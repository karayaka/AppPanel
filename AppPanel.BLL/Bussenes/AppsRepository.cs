using AppPanel.BLL.DTO.AppsDTO;
using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.AppsClasses;
using AppPanel.DAL.DataContext;
using AppPanel.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Bussenes
{
    public class AppsRepository:AppBaseRepository,IAppsRepository
    {
        private readonly ServiceContext context;

        private int UserID;
        public AppsRepository(ServiceContext _context, IHttpContextAccessor httpContextAccessor) :base(_context, httpContextAccessor)
        {
            context = _context;
            var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (val != null)
                UserID = Convert.ToInt32(val.Value);
        }
        


        public async Task<PanelApp> CreatePanelApp(AppPanelFormModel model)
        {
            try
            {
                var appKey = Guid.NewGuid() + "-/-" + Guid.NewGuid();
                var app = new PanelApp()
                {
                    AppDesc = model.AppDesc,
                    AppName = model.AppName,
                    GitHubUrl = model.GitHubUrl,
                    AppSubDesc=model.AppSubDesc,
                    AppKey = appKey,
                    AppUrl=model.AppUrl,                    
                };
                await Add(app);

                await PanelAppImageAdd(app, model.Files);

                await PanelAppColors(app, model.AppCardColors);

                await context.SaveChangesAsync();
                return app;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<AppMainList>> GetAppMainPageList()
        {
            try
            {
                var model = await (await GetNonDeletedAndActive<PanelApp>(t => true)).Select(s => new AppMainList() 
                {
                    AppName=s.AppName,
                    Url=s.AppUrl,
                    AppDesc=s.AppDesc,
                    GitHubUrl=s.GitHubUrl,
                    AppSubDesc=s.AppSubDesc,
                    AppCardColors= s.AppCardColors.Select(s=>s.Color).ToList(),
                    AppImages= s.AppImages.Where(s=>s.ObjectStatus==ObjectStatus.NonDeleted).Select(t=> "https://cagnaz.com/service/Images/appPanel/appImages/" + (t.ImageUr).ToString()).ToList()

                }).ToListAsync();

                foreach (var item in model)
                {
                    item.Colors = CardColorPrePare(item.AppCardColors);
                }

                return model;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> PanelAppColors(PanelApp app, List<string> model)
        {
            try
            {
                var colors = new List<AppCardColor>();
                foreach (var item in model)
                {
                    var color = new AppCardColor()
                    {
                        Color = item,
                        PanelApp = app,
                        PanelAppID = app.ID,
                        Desc = "renk",
                    };
                    colors.Add(color);
                }
                await AddRange(colors);

                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> PanelAppDelete(int ID)
        {
            try
            {
                var app = await GetIQueryableByID<PanelApp>(ID).Include(i => i.AppImages).Include(t => t.AppCardColors).FirstOrDefaultAsync();
                await DeleteRange(app.AppCardColors);
                await DeleteRange(app.AppImages);

                await Delete(app);

                return await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> PanelAppImageAdd(PanelApp app, List<IFormFile> Files)
        {
            try
            {
                var files = new List<AppImages>();
                foreach (var item in Files)
                {
                    var image = new AppImages();
                    image.ImageUr = SaveImage(item, "\\Images\\appPanel\\appImages", "app");
                    image.ImageName = item.FileName;
                    image.PanelAppID = app.ID;
                    image.PanelApp = app;
                    files.Add(image);
                }
                await AddRange(files);
                return 1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<PanelApp> UpdatePanelApp(AppPanelFormModel model)
        {
            try
            {
                var app = await GetIQueryableByID<PanelApp>(model.ID).Include(i => i.AppImages)
                    .Include(t => t.AppCardColors).FirstOrDefaultAsync();
                if(model.AppCardColors.Count()>0)
                    await DeleteRange(app.AppCardColors);
                if (model.Files.Count > 0)
                    await DeleteRange(app.AppImages);

                app.AppName = model.AppName;
                app.AppDesc = model.AppDesc;
                app.AppKey = model.AppKey;
                app.AppUrl = model.AppUrl;

                await Update(app);

                await PanelAppImageAdd(app, model.Files);

                await PanelAppColors(app, model.AppCardColors);

                await context.SaveChangesAsync();

                return app;

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string CardColorPrePare(List<string> colors)
        {
            var retVal = "linear-gradient(";

            for (int i = 0; i < colors.Count; i++)
            {
                retVal += colors[i];
                if (i != (colors.Count-1))
                    retVal += ",";
            }

       
            retVal += ")";
            return retVal;
        }
    }
}
