using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.DTO.AppsDTO
{
    public class AppImage
    {
        public int ID { get; set; }

        public int PanelAppID { get; set; }

        public List<IFormFile> Files { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }


    }
}
