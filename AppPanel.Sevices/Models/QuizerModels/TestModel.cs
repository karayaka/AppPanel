using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Models.QuizerModels
{
    public class TestModel
    {
        public int ID { get; set; }

        public int TopicID { get; set; }

        public string TestName { get; set; }

        public string TestDesc { get; set; }

        public bool ShowTestStartDesc { get; set; }

        public string TestStartDesc { get; set; }

        public AdsStatus AdsStatus { get; set; }

        public TestStatus TestStatus { get; set; }
    }
}
