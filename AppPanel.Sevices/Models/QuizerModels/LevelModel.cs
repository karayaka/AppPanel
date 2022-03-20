using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.QuizerModels
{
    public class LevelModel
    {
        public int ID { get; set; }

        public string LevelName { get; set; }

        public string LevelDesc { get; set; }

        public int TopicCount { get; set; }

        public int TestCount { get; set; }

    }
}
