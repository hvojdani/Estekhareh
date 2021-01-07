using SQLite;

namespace Estekhareh.DatabaseModels
{
    [Table("quran_text")]
    public class QuranText
    {
        [PrimaryKey, AutoIncrement]
        public int index { get; set; }
        public int joz { get; set; }
        public int pageNo { get; set; }
        public int sura { get; set; }
        public int aya { get; set; }
        public string text { get; set; }
        public string text_clean { get; set; }
    }
}


