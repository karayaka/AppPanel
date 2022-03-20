using AppPanel.DAL.Classes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.AppsClasses
{
    public class PanelApp:BaseObject
    {
        public string AppName { get; set; }

        public string AppSubDesc { get; set; }

        public string AppDesc { get; set; }


        public string AppUrl { get; set; }

        public string GitHubUrl { get; set; }

        public string AppKey { get; set; }

        public ICollection<AppImages> AppImages { get; set; }
        public ICollection<AppCardColor> AppCardColors { get; set; }
    }
}
