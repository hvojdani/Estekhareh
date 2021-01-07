using Estekhareh.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Estekhareh.Services
{
    public interface IEstekharehDatabase
    {
        Task<List<QuranText>> GetAyas(params int[] ayaIndexs);
        Task<List<QuranTranslate>> GetTranslates(EnmTranslateBy translateBy, params int[] ayaIndexs);
        Task<List<Quran_Sura>> GetSuras(int[] suraIndexs);
    }
}