﻿using SQLite;

namespace SpeechPathology.Models
{
    [Table("ArticulationTests")]
    public class ArticulationTest : ModelBase
    {
        public string Sound { get; set; }
        public string Text { get; set; }
        [Indexed]
        public string SoundPosition { get; set; }
        public string Image { get; set; }
        [Indexed]
        public string LanguageCode { get; set; }
        [Indexed]
        public int AgeY { get; set; }
        [Indexed]
        public int AgeM { get; set; }
    }
}
