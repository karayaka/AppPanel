using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.DTO.AppsDTO
{
    public class AppMainList
    {
        public string AppName { get; set; }

        public string AppDesc { get; set; }

        public string AppSubDesc { get; set; }

        public string GitHubUrl { get; set; }

        public List<string> AppImages { get; set; }

        public string Url { get; set; }

        public List<string> AppCardColors { get; set; }
        public string Colors { get; set; }
    }
}
