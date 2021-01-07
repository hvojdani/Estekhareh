using Estekhareh.Models;
using SheardResources;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Threading.Tasks;

namespace Estekhareh.Services
{
    public class EstekharehDatabase : IEstekharehDatabase
    {
        static SQLiteAsyncConnection Database = null;

        public void CopyFromResourceToPhone()
        {
            if (File.Exists(Constants.DatabasePath))
            {
                //File.Delete(Constants.DatabasePath);
                return;
            }

            var manager = new ResourceManager("SheardResources.Properties.Resources", typeof(SheardResClass).Assembly);
            var stream = new MemoryStream(manager.GetObject("QuranAlkarim") as byte[]);
         
            using (var reader = new System.IO.BinaryReader(stream))
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(Constants.DatabasePath, FileMode.CreateNew)))
                {
                    byte[] buffer = new byte[2048];
                    int len = 0;
                    while ((len = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, len);
                    }
                }
            }

        }

        public EstekharehDatabase()
        {
            if (Database is null)
            {
                CopyFromResourceToPhone();
                Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            }
        }

        public Task<List<QuranText>> GetAyas(params int[] ayaIndexs)
        {
            return Database.QueryAsync<QuranText>($"select * from quran_text where [index] in ({string.Join(", ", ayaIndexs)})");
        }

        public Task<List<QuranTranslate>> GetTranslates(EnmTranslateBy translateBy, params int[] ayaIndexs)
        {
            var tableName = translateBy.ToString().ToLower();
            return Database.QueryAsync<QuranTranslate>($"select * from {tableName} where [index] in ({string.Join(", ", ayaIndexs)})");
        }

        public Task<List<Quran_Sura>> GetSuras(int[] suraIndexs)
        {
            return Database.QueryAsync<Quran_Sura>($"select * from quranNameList where id in ({string.Join(", ", suraIndexs)})");
        }
    }
}


