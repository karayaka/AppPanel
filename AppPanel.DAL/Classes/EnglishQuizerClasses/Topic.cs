using AppPanel.DAL.Classes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.EnglishQuizerClasses
{
    public class Topic:BaseObject
    {
        public int LevelID { get; set; }
        public Level Level { get; set; }

        public string TopicName { get; set; }

        public string TopicDesc { get; set; }

        public ICollection<Test> Tests { get; set; }

    }
}
