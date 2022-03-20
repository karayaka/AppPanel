using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.QuizerModels
{
    public class TopicListModel
    {
        public int ID { get; set; }

        public string LevelName { get; set; }

        public int LevelID { get; set; }

        public string TopicName { get; set; }

        public string TopicDesc { get; set; }

        public int TestCount { get; set; }
    }
}
