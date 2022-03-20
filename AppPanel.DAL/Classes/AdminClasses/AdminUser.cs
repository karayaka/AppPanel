using AppPanel.DAL.Classes.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.Classes.AdminClasses
{
    public class AdminUser:BaseObject
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }


        public string UserImage { get; set; }
    }
}
