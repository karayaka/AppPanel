using AppPanel.DAL.Classes.BaseClasses;
using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.EnglishQuizerClasses
{
    /// <summary>
    /// test için genel soru yazılacak ve dolu ise teste başlamadan 
    /// gösterilecek
    /// </summary>
    public class Test:BaseObject
    {
        public int TopicID { get; set; }
        public Topic Topic { get; set; }

        public string TestName { get; set; }

        public string TestDesc { get; set; }

        public bool ShowTestStartDesc { get; set; }

        public string TestStartDesc { get; set; }

        public AdsStatus AdsStatus { get; set; }

        public TestStatus TestStatus { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
