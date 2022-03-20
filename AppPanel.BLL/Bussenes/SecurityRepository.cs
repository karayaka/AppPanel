using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.AdminClasses;
using AppPanel.DAL.DataContext;
using AppPanel.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Bussenes
{
    public class SecurityRepository : ISecurityRepository
    {
        private readonly ServiceContext context;
        public SecurityRepository(ServiceContext _context)
        {
            context = _context;
        }
        /// <summary>
        /// Oturumsuz Kullanıcı ekleme İşlemleri İçin
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<AdminUser> AddUser(AdminUser user)
        {
            try
            {
                user.CreatedBy = 0;
                user.LastModifiedBy = 0;
                await context.AdminUsers.AddAsync(user);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<AdminUser> Login(string UserName, string Password)
        {
            try
            {
                return await context.AdminUsers.FirstOrDefaultAsync(t => (t.UserName == UserName || t.Email == UserName)
            && t.Password == Password && t.ObjectStatus == ObjectStatus.NonDeleted);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public bool VerifyEmail(string email)
        {
            try
            {
                return context.AdminUsers.Any(t => t.Email == email.Trim());
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public bool VerifyUserName(string userName)
        {
            try
            {
                return context.AdminUsers.Any(t => t.UserName == userName.Trim());
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
