using AppPanel.BLL.DTO.AppsDTO;
using AppPanel.DAL.Classes.AppsClasses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Interfaces
{
    public interface IAppsRepository:IAppBaseRepository
    {
        /// <summary>
        /// Uygulamaların ana Sayfada getiren metod
        /// </summary>
        /// <returns></returns>
        Task<List<AppMainList>> GetAppMainPageList();

        /// <summary>
        /// App Oluşturma Fonl-ksiyonu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PanelApp> CreatePanelApp(AppPanelFormModel model);
        /// <summary>
        /// Ugulama Güncelleme metodu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PanelApp> UpdatePanelApp(AppPanelFormModel model);

        /// <summary>
        /// Uygulamaya resim ekleme metodu
        /// </summary>
        /// <param name="app"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        Task<int> PanelAppImageAdd(PanelApp app, List<IFormFile> Files);

        /// <summary>
        /// Uygulamaya rekn ekleme metodu
        /// </summary>
        /// <param name="app"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> PanelAppColors(PanelApp app, List<string> model);

        /// <summary>
        /// Uygumala kendisi resimleri ve renkleri silme metodu
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<int> PanelAppDelete(int ID);
    }
}
