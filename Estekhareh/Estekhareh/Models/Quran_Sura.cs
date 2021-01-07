using SQLite;

namespace Estekhareh.Models
{
    [Table("quranNameList")]
    public class Quran_Sura
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string sura { get; set; }
        public int page { get; set; }
    }
}


