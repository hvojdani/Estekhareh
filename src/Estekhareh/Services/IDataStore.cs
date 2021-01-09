using Estekhareh.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estekhareh.Services
{
    public interface IDataStore
    {
        Task<Item> GetItemAsync(int index);
        Task<IEnumerable<Item>> GetItemsAsync(int startIndex, int ayaCount);
        Task SaveLastIndex(int index);
        Task<int> GetLastIndex();
        Task SaveSetting(SettingModel setting);
        Task<SettingModel> GetSetting();
        Task<List<TranslatorModel>> GetTranslates();

    }
}
