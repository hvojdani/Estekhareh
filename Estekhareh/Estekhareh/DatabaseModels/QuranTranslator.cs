using SQLite;

namespace Estekhareh.DatabaseModels
{
    [Table("tbl_trans")]
    public class QuranTranslator
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public string table { get; set; }
    }
}