using Estekhareh.DatabaseModels;
using Estekhareh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estekhareh.Services
{
    public interface IEstekharehDatabase
    {
        Task<List<QuranText>> GetAyas(params int[] ayaIndexs);
        Task<List<QuranTranslate>> GetTranslates(int translaorIndex, params int[] ayaIndexs);
        Task<List<QuranSura>> GetSuras(int[] suraIndexs);
        Task<EstekharehSetting> GetSetting(bool forceRefresh = false);
        Task SetSetting(EstekharehSetting appSetting);
        Task<List<QuranTranslator>> GetTranslators();        
    }
}