using AppPanel.DAL.Classes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.EnglishQuizerClasses
{
    public class Level:BaseObject
    {
        public string LevelName { get; set; }

        public string LevelDesc { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
