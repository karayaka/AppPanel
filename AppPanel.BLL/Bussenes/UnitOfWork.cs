using AppPanel.BLL.Interfaces;
using AppPanel.DAL.DataContext;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Bussenes
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ServiceContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UnitOfWork(ServiceContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            context = _context ?? throw new ArgumentNullException("context can not be null");
            httpContextAccessor = _httpContextAccessor;
        }

        private IAppBaseRepository _BaseRepositoriy;

        public IAppBaseRepository BaseRepositoriy
        {
            get => _BaseRepositoriy ?? (_BaseRepositoriy = new AppBaseRepository(context, httpContextAccessor));
        }


        private ISecurityRepository _SecurityRepository;
        public ISecurityRepository SecurityRepository
        {
            get => _SecurityRepository ?? (_SecurityRepository = new SecurityRepository(context));
        }


        private IQuizerRepository _QuizerRepository;
        public IQuizerRepository QuizerRepository
        {
            get => _QuizerRepository ?? (_QuizerRepository = new QuizerRepository(context, httpContextAccessor));
        }
        private IAppsRepository _AppsRepository;
        public IAppsRepository AppsRepository
        {
            get => _AppsRepository ?? (_AppsRepository = new AppsRepository(context, httpContextAccessor));
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
