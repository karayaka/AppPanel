using AppPanel.DAL.Classes.AdminClasses;
using AppPanel.DAL.Classes.AppsClasses;
using AppPanel.DAL.Classes.EnglishQuizerClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.DAL.DataContext
{
    public class ServiceContext:DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options) : base(options)
        {

        }

        //quizerApp
        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<Question> Questions { get; set; }

        //Apps Tables

        public DbSet<PanelApp> PanelApps { get; set; }
        public DbSet<AppImages> AppImages { get; set; }
        public DbSet<AppCardColor> AppCardColors { get; set; }

    }
}
