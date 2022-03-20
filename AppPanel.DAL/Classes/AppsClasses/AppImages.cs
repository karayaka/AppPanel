using AppPanel.DAL.Classes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.AppsClasses
{
    public class AppImages:BaseObject
    {
        public int PanelAppID { get; set; }
        public PanelApp PanelApp { get; set; }

        public string ImageName { get; set; }


        public string ImageUr { get; set; }


    }
}
