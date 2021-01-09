using SQLite;

namespace Estekhareh.DatabaseModels
{
    [Table("tbl_setting")]
    public class EstekharehSetting
    {
        [PrimaryKey]
        public int id { get; set; }
        public int enable_trans { get; set; }
        public int last_index { get; set; }
        public int translator_index { get; set; }
    }
}
