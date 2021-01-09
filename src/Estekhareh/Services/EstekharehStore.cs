using Estekhareh.DatabaseModels;
using Estekhareh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Estekhareh.Services
{
    public class EstekharehStore : IDataStore
    {
        IEstekharehDatabase DataBaseStore => DependencyService.Get<IEstekharehDatabase>();

        public async Task<Item> GetItemAsync(int index)
        {
            return (await GetItemsByIdList(index)).FirstOrDefault();
        }

        public Task<IEnumerable<Item>> GetItemsAsync(int startIndex, int ayaCount)
        {
            if (startIndex < 2 || startIndex > 6236)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "must between 2 and 6236");
            }

            if (ayaCount < 1 || ayaCount > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(ayaCount), ayaCount, "must between 1 and 20");
            }

            var ayasIndex = GetAyasIndexArray(startIndex, ayaCount);

            return GetItemsByIdList(ayasIndex);

        }

        private async Task<IEnumerable<Item>> GetItemsByIdList(params int[] ayasIndex)
        {
            if (ayasIndex is null || ayasIndex.Length == 0)
            {
                throw new ArgumentNullException(nameof(ayasIndex));
            }

            var ayaList = await DataBaseStore.GetAyas(ayasIndex);
            var suras = await DataBaseStore.GetSuras(ayaList.Select(a => a.sura).Distinct().ToArray());

            var setting = await DataBaseStore.GetSetting();
            var translateList = setting.enable_trans == 1
                ? await DataBaseStore.GetTranslates(setting.translator_index, ayasIndex)
                : new List<QuranTranslate>();

            return ayaList.Select((aya) => new Item
            {
                Id = aya.index,
                Text = aya.text,
                Description = translateList.FirstOrDefault(i => i.index == aya.index)?.text,
                AyaDescription = $"{suras.FirstOrDefault(s => s.id == aya.sura)?.sura} {aya.aya}"
            });
        }

        private static int[] GetAyasIndexArray(int startIndex, int ayaCount)
        {
            var randomAyas = new List<int>(ayaCount);
            randomAyas.Add(startIndex);
            var temp = startIndex;
            for (int i = 1; i < ayaCount; i++)
            {
                temp = temp + 1;
                if (temp > 6236)
                {
                    break;
                }
                randomAyas.Add(temp);
            }

            return randomAyas.ToArray();
        }

        public async Task SaveLastIndex(int index)
        {
            var setting = await DataBaseStore.GetSetting();
            setting.last_index = index;
            await DataBaseStore.SetSetting(setting);
        }

        public async Task<int> GetLastIndex()
        {
            var setting = await DataBaseStore.GetSetting();
            return setting.last_index;
        }


        public async Task SaveSetting(SettingModel settingModel)
        {
            var setting = await DataBaseStore.GetSetting();

            setting.enable_trans = settingModel.EnableTranslation ? 1 : 0;
            setting.translator_index = settingModel.TranslatorIndex;
            await DataBaseStore.SetSetting(setting);
        }

        public async Task<SettingModel> GetSetting()
        {
            var setting = await DataBaseStore.GetSetting();

            return new SettingModel
            {
                EnableTranslation = setting.enable_trans == 1,
                TranslatorIndex = setting.translator_index
            };
        }

        public async Task<List<TranslatorModel>> GetTranslates()
        {
            var translators = await DataBaseStore.GetTranslators();
            return translators.Select(t => new TranslatorModel
            {
                Index = t.id,
                Name = t.name
            }).ToList();
        }
    }
}