using Estekhareh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Estekhareh.Services
{
    public class EstekharehStore : IDataStore<Item>
    {
        IEstekharehDatabase DataStore => DependencyService.Get<IEstekharehDatabase>();
        readonly List<Item> items;

        public EstekharehStore()
        {
            items = new List<Item>()
            {
                new Item { Id = 1, Text = "First item", Description="This is an item description." },
                new Item { Id = 2, Text = "Second item", Description="This is an item description." },
                new Item { Id = 3, Text = "Third item", Description="This is an item description." },
                new Item { Id = 4, Text = "Fourth item", Description="This is an item description." },
                new Item { Id = 5, Text = "Fifth item", Description="This is an item description." },
                new Item { Id = 6, Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return (await GetItemsByIdList(id)).FirstOrDefault();
        }

        public Task<IEnumerable<Item>> GetItemsAsync(int ayaCount)
        {
            if (ayaCount < 1 || ayaCount > 20)
            {
                throw new ArgumentOutOfRangeException(nameof(ayaCount), ayaCount, "must between 1 and 20");
            }

            var randomAyas = GetRandomAyaStart(ayaCount);

            return GetItemsByIdList(randomAyas);

        }

        private async Task<IEnumerable<Item>> GetItemsByIdList(params int[] ayasIndex)
        {
            if(ayasIndex is null || ayasIndex.Length == 0)
            {
                throw new ArgumentNullException(nameof(ayasIndex));
            }

            var ayaList = await DataStore.GetAyas(ayasIndex);
            var translateList = await DataStore.GetTranslates(EnmTranslateBy.fa_makarem, ayasIndex);
            var suras = await DataStore.GetSuras(ayaList.Select(a => a.sura).Distinct().ToArray());

            return ayaList.Select((aya, i) => new Item
            {
                Id = aya.index,
                Text = aya.text,
                Description = translateList[i].text,
                AyaDescription = $"{suras.FirstOrDefault(s => s.id == aya.sura)?.sura}-{aya.aya}"
            });
        }

        private static int[] GetRandomAyaStart(int ayaCount)
        {
            Random random = new Random();
            var randomAyas = new List<int>(ayaCount);
            var rand = random.Next(2, 6236);
            randomAyas.Add(rand);

            for (int i = 1; i < ayaCount; i++)
            {
                rand = rand + 1;
                if (rand > 6236)
                {
                    break;
                }
                randomAyas.Add(rand);
            }

            return randomAyas.ToArray();
        }
    }
}