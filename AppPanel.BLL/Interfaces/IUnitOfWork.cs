using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IAppBaseRepository BaseRepositoriy { get; }

        ISecurityRepository SecurityRepository { get; }

        IQuizerRepository QuizerRepository { get; }

        IAppsRepository AppsRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
