using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.DTO.AppsDTO
{
    public class AppPanelFormModel
    {
        public int ID { get; set; }

        public string AppName { get; set; }

        public string AppDesc { get; set; }

        public string AppSubDesc { get; set; }

        public string GitHubUrl { get; set; }

        public string AppUrl { get; set; }

        public string AppKey { get; set; }

        public List<IFormFile> Files { get; set; }

        //public IFormCollection files { get; set; }

        public List<string> FilesUrls { get; set; }

        public List<string> AppCardColors { get; set; }
    }
}
