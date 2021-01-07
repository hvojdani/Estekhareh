﻿using SQLite;

namespace Estekhareh.Models
{
    public class QuranTranslate
    {
        [PrimaryKey, AutoIncrement]
        public int index { get; set; }
        public int sura { get; set; }
        public int aya { get; set; }
        public string text { get; set; }
    }
}


