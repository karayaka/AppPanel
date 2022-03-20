using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.QuizerModels
{
    public class TestListModel
    {
        public int ID { get; set; }

        public string TopicName { get; set; }

        public int TopicID { get; set; }

        public int QuestionCount { get; set; }

        public string TestName { get; set; }

        public string TestDesc { get; set; }

        public string TestStartDesc { get; set; }

        public int AdsStatus { get; set; }

        public string AdsStatusStr { get; set; }

        public int TestStatus { get; set; }

        public bool ShowTestStartDesc { get; set; }
    }
}
