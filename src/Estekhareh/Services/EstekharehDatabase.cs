﻿using Estekhareh.Models;
using Estekhareh.DatabaseModels;
using SheardResources;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System;

namespace Estekhareh.Services
{
    public class EstekharehDatabase : IEstekharehDatabase
    {
        static SQLiteAsyncConnection _database;
        private SQLiteAsyncConnection Database 
        {
            get
            {
                lock (_lock)
                {
                    if (_database is null)
                    {
                        CopyFromResourceToPhone();
                        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
                    }
                }
                return _database;
            }
        }

        private object _lock = new object();

        private EstekharehSetting _cachedSetting;
        private List<QuranTranslator> _cachedTranslators;

        public static void CopyFromResourceToPhone()
        {
            if (File.Exists(Constants.DatabasePath))
            {
                //File.Delete(Constants.DatabasePath);
                return;
            }

            var manager = new ResourceManager("SheardResources.Properties.Resources", typeof(SheardResClass).Assembly);
            using (var stream = new MemoryStream(manager.GetObject("QuranAlkarim") as byte[]))
            {
                using (var reader = new System.IO.BinaryReader(stream))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(Constants.DatabasePath, FileMode.CreateNew)))
                    {
                        byte[] buffer = new byte[1024];
                        int len = 0;
                        while ((len = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
        }

        public EstekharehDatabase()
        {
        }

        public Task<List<QuranText>> GetAyas(params int[] ayaIndexs)
        {
            return Database.QueryAsync<QuranText>($"select * from quran_text where [index] in ({string.Join(", ", ayaIndexs)})");
        }

        public async Task<List<QuranTranslate>> GetTranslates(int translatorIndex, params int[] ayaIndexs)
        {
            var translators = await GetTranslators();
            var activeTranslator = translators.First(t => t.id == translatorIndex);
            return await Database.QueryAsync<QuranTranslate>($"select * from {activeTranslator.table} where [index] in ({string.Join(", ", ayaIndexs)})");
        }

        public Task<List<QuranSura>> GetSuras(int[] suraIndexs)
        {
            return Database.QueryAsync<QuranSura>($"select * from quranNameList where id in ({string.Join(", ", suraIndexs)})");
        }

        public async Task<EstekharehSetting> GetSetting(bool forceRefresh = false)
        {
            if (_cachedSetting is null || forceRefresh)
            {
                var settings = await Database.QueryAsync<EstekharehSetting>("select * from tbl_setting where id=1");
                _cachedSetting = settings.FirstOrDefault();
            }
            return _cachedSetting;
        }

        public Task SetSetting(EstekharehSetting appSetting)
        {
            _cachedSetting = appSetting;
            return Database.ExecuteAsync("update tbl_setting set enable_trans = ?, last_index=?, translator_index=? where id=1", appSetting.enable_trans, appSetting.last_index, appSetting.translator_index);
        }

        public async Task<List<QuranTranslator>> GetTranslators()
        {
            if (_cachedTranslators is null)
            {
                _cachedTranslators = await Database.QueryAsync<QuranTranslator>($"select * from tbl_trans");
            }
            return _cachedTranslators;
        }

    }
}


