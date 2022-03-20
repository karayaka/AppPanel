using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.DTO.AppsDTO
{
    public class AppCardColorModel
    {
        public int ID { get; set; }
        
        public int PanelAppID { get; set; }

        public string Color { get; set; }

        public string Desc { get; set; }
    }
}
