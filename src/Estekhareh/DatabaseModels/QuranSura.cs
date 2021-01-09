using SQLite;

namespace Estekhareh.DatabaseModels
{
    [Table("quranNameList")]
    public class QuranSura
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string sura { get; set; }
        public int page { get; set; }
    }
}


