using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.DTO.BaseModels
{
    public class AppImageResultModel
    {
        public int ID { get; set; }

        public string ImageName { get; set; }


        public string ImageUrlOrg { get; set; }


        public string ImageUrlSmall { get; set; }


        public string ImageUrlMiddle { get; set; }


        public string ImageUrlLarge { get; set; }

    }
}
