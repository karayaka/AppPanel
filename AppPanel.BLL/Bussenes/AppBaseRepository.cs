using AppPanel.BLL.DTO.BaseModels;
using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.AppsClasses;
using AppPanel.DAL.Classes.BaseClasses;
using AppPanel.DAL.DataContext;
using AppPanel.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Bussenes
{
    public class AppBaseRepository : IAppBaseRepository
    {
        private readonly ServiceContext context;

        private int UserID;

        public AppBaseRepository(ServiceContext _context, IHttpContextAccessor httpContextAccessor)
        {
            //Kullanıcı Giriş Yöntemleri karşışatıtran bir yapı kurulacak
            context = _context;
            var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (val != null)
                UserID = Convert.ToInt32(val.Value);
        }
        /// <summary>
        /// Generik Olarak Ekleme Metodu
        /// Eklenen Objeyi döndürüyor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entitiy"></param>
        /// <returns></returns>
        public async Task<T> Add<T>(T Entitiy) where T : BaseObject
        {
            try
            {
                Entitiy.CreatedBy = UserID;
                Entitiy.LastModifiedBy = UserID;
                Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
                Entitiy.Status = Status.Active;
                await context.AddAsync(Entitiy);
                return Entitiy;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public async Task<IEnumerable<T>> AddRange<T>(IEnumerable<T> models) where T : BaseObject
        {
            try
            {
                foreach (var item in models)
                {
                    item.CreatedBy = UserID;
                    item.LastModifiedBy = UserID;
                    item.ObjectStatus = ObjectStatus.NonDeleted;
                    item.Status = Status.Active;
                }
                await context.AddRangeAsync(models);

                return models;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IQueryable<T>> AddRange<T>(IQueryable<T> models) where T : BaseObject
        {
            try
            {
                foreach (var item in models)
                {
                    item.CreatedBy = UserID;
                    item.LastModifiedBy = UserID;
                }
                await context.AddRangeAsync(models);

                return models;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// veri silen fonksiyon
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<int> Delete<T>(int ID) where T : BaseObject
        {
            try
            {
                var model = context.Set<T>().FirstOrDefault(t => t.ID == ID);
                return await Delete(model);
                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<int> Delete<T>(T model) where T : BaseObject
        {
            try
            {
                model.LastModifiedBy = UserID;
                model.LastModifiedDate = DateTime.Now;
                model.ObjectStatus = ObjectStatus.Deleted;
                model.Status = Status.Pasive;
                context.Update(model);
                return model.ID;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<T>> DeleteRange<T>(IEnumerable<T> models) where T : BaseObject
        {
            try
            {
                foreach (var model in models)
                {
                    model.ObjectStatus = ObjectStatus.Deleted;
                    model.LastModifiedBy = UserID;
                    model.Status = Status.Pasive;
                    context.Update(model);
                }
                return models;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IQueryable<T>> Get<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return context.Set<T>().Where(expression);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<T> GetByID<T>(int ID) where T : BaseObject
        {
            try
            {
                return await context.Set<T>().FirstOrDefaultAsync(t => t.ID == ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SelectListModel> GetEnumSelectList<T>()
        {
            try
            {
                return (Enum.GetValues(typeof(T)).Cast<T>().Select(
                enu => new SelectListModel() { Text = enu.ToString(), Value = enu.ToString() })).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IQueryable<T> GetIQueryableByID<T>(int ID) where T : BaseObject
        {
            try
            {
                return context.Set<T>().Where(t => t.ID == ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IQueryable<T>> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                IQueryable<T> models;
                if (expression != null)
                    models = context.Set<T>().Where(expression);
                else
                    models = context.Set<T>();

                return models.Where(t => t.ObjectStatus == ObjectStatus.NonDeleted);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<IQueryable<T>> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                var model = await GetNonDeleted<T>(t => t.Status == Status.Active);
                return model.Where(expression).OrderByDescending(o=>o.ID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<IQueryable<T>> GetNonDeletedAndPaginate<T>(int pageID, int PageSize) where T : BaseObject
        {
            try
            {
                //Lokayona göre filitrelenecek metod düşünülecek
                pageID--;
                return (await GetNonDeletedAndActive<T>(t => true)).Skip(pageID * PageSize).Take(PageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<IQueryable<T>> GetNonDeletedAndPaginate<T>(Expression<Func<T, bool>> expression, int pageID, int PageSize) where T : BaseObject
        {
            pageID--;
            return (await GetNonDeletedAndActive<T>(expression)).OrderBy(o=>o.ID).Skip(pageID * PageSize).Take(PageSize);
        }


        public int GetPageCount<T>(Expression<Func<T, bool>> expression, int pageSize) where T : BaseObject
        {
            try
            {
                var count = context.Set<T>().Count(expression);
                return (count / pageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetPageCount<T> (int pageSize) where T : BaseObject
        {
            try
            {
                return GetPageCount<T>(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active, pageSize);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int GetPageCount<T>(IQueryable<T> models, int pageSize) where T : BaseObject
        {
            try
            {
                var count = models.Count();
                return (count / pageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<IQueryable<T>> GetPaginate<T>(IQueryable<T> models, int pageCount, int pageSize) where T : BaseObject
        {
            try
            {
                pageCount--;
                return models.Skip<T>(pageCount * pageSize).Take(pageSize);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IQueryable<T>> GetQueryableByID<T>(int ID) where T : BaseObject
        {
            try
            {
                return await GetNonDeletedAndActive<T>(t => t.ID == ID);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<int> Remove<T>(int ID) where T : BaseObject
        {
            try
            {
                var model = await GetByID<T>(ID);
                context.Remove(model);
                return ID;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void RemoveRange<T>(IEnumerable<T> models) where T : BaseObject
        {
            try
            {
                context.RemoveRange(models);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<T> Update<T>(T Entitiy) where T : BaseObject
        {
            try
            {
                Entitiy.LastModifiedBy = UserID;
                Entitiy.ObjectStatus = ObjectStatus.NonDeleted;
                Entitiy.Status = Status.Active;
                context.Update(Entitiy);
                return Entitiy;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<T>> UpdateRange<T>(IEnumerable<T> models) where T : BaseObject
        {
            try
            {
                foreach (var item in models)
                {
                    item.LastModifiedBy = UserID;
                    item.Status = Status.Active;
                }
                context.UpdateRange(models);
                return models;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string SaveImage(IFormFile file, string folderPath, string fileName)
        {
            try
            {
                var x = Directory.GetCurrentDirectory();
                var pathToSave = x+"\\wwwroot"+folderPath;//TEstamaçlıYAzıldı
                string fullPath = "";
                if (file.Length > 0)
                {
                    var fileKey = Guid.NewGuid();
                    var ex = Path.GetExtension(file.FileName);
                    fileName = fileName + "-" + fileKey + ex;
                    fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return fileName;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Count<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return context.Set<T>().Count(expression);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<int> CountNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return (await GetNonDeleted<T> (expression)).Count();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<int> CountNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return (await GetNonDeletedAndActive<T>(expression)).Count();

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool Any<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
           
                return context.Set<T>().Any(expression);
            }
            catch (Exception a)
            {

                throw a;
            }
        }

        public async Task<bool> AnyNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return (await GetNonDeleted<T>(t => true)).Any(expression);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> AnyNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject
        {
            try
            {
                return (await GetNonDeletedAndActive<T>(t => true)).Any(expression);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> CheckAppKey(string Key)
        {
            try
            {
                return await AnyNonDeletedAndActive<PanelApp>(t => t.AppKey == Key);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
