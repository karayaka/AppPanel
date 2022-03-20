using AppPanel.DAL.Classes.AdminClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Interfaces
{
    public interface ISecurityRepository
    {
        Task<AdminUser> AddUser(AdminUser user);

        Task<AdminUser> Login(string UserName, string Password);

        bool VerifyEmail(string email);

        bool VerifyUserName(string userName);
    }
}
