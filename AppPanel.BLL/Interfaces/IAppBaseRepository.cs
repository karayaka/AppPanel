using AppPanel.BLL.DTO.BaseModels;
using AppPanel.DAL.Classes.BaseClasses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Interfaces
{
    public interface IAppBaseRepository
    {
        /// <summary>
        /// Generik Olarak Ekleme Metodu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<T> Add<T>(T model) where T : BaseObject;

        /// <summary>
        /// Liste OLarak Ekele
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> AddRange<T>(IEnumerable<T> models) where T : BaseObject;
        Task<IQueryable<T>> AddRange<T>(IQueryable<T> models) where T : BaseObject;

        /// <summary>
        /// Generik Olarak Güncelleme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entitiy"></param>
        /// <returns></returns>
        Task<T> Update<T>(T Entitiy) where T : BaseObject;

        /// <summary>
        /// Liste Olarak Günceleme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Entitiy"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> UpdateRange<T>(IEnumerable<T> models) where T : BaseObject;

        /// <summary>
        /// Generik Olarak Silme Metodu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        Task<int> Delete<T>(int ID) where T : BaseObject;

        /// <summary>
        /// Generik Olarak Silme Metodu
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        Task<int> Delete<T>(T model) where T : BaseObject;

        /// <summary>
        /// Liste Halinde Silme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        Task<IEnumerable<T>> DeleteRange<T>(IEnumerable<T> models) where T : BaseObject;
        

        /// <summary>
        /// ID ile DbSilme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        Task<int> Remove<T>(int ID) where T : BaseObject;

        /// <summary>
        /// Liste Silme
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        void RemoveRange<T>(IEnumerable<T> models) where T : BaseObject;

        /// <summary>
        /// Get Queryable Generik
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IQueryable<T>> Get<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        /// <summary>
        /// Get Queryable NonDeleted
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        /// <summary>
        /// Get Queryable NonDeleted ve Aktif Kayıtlar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject;

        /// <summary>
        /// Get Queryable Silinmemiş ve aktif kayıtları 
        /// 20 li kayılar ile bir sayfa
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageID"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetNonDeletedAndPaginate<T>(int pageID, int PageSize) where T : BaseObject;
        Task<IQueryable<T>> GetNonDeletedAndPaginate<T>(Expression<Func<T, bool>> expression, int pageID, int PageSize) where T : BaseObject;

        /// <summary>
        /// ID ile getirilmniş onje kayıtları
        /// ınculude yapılamaz!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<T> GetByID<T>(int ID) where T : BaseObject;

        /// <summary>
        /// Sorgulanabilir Nesne ID İle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetQueryableByID<T>(int ID) where T : BaseObject;

        /// <summary>
        /// Sayfalama işleminde önce sayfa sayısı
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        int GetPageCount<T>(Expression<Func<T, bool>> expression, int pageSize) where T : BaseObject;

        /// <summary>
        /// Sorgu Veilmez ise silinmemiş aktif kayıtların sayfa sayısı getiriyor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int GetPageCount<T>(int pageSize) where T : BaseObject;

        /// <summary>
        /// Quarible nesneyi ni saydfa sayısını veran kod
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        int GetPageCount<T>(IQueryable<T> models, int pageSize) where T : BaseObject;


        /// <summary>
        /// Qurable nesneyi sayfalayıp dönen fonsiyon
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models">Nesneler</param>
        /// <param name="pageCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetPaginate<T>(IQueryable<T> models, int pageCount,int pageSize) where T : BaseObject;


        /// <summary>
        /// Dosya Kaydetme İşlemi Dosya Adı Dönüyor
        /// </summary>
        /// <param name="files"></param>
        /// <param name="folderName"></param>
        /// <param name="path">KayıtOLacak Dosya Yolu</param>
        /// <returns></returns>
        //string SaveFile(IFormFile file, string folderName, string path);

        /// <summary>
        /// Resim Defauld Boyutlama ile resim yükleme
        /// </summary>
        /// <param name="files"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        //ImageResultModel SaveImage(IFormFile file, string folderName, string path);

        List<SelectListModel> GetEnumSelectList<T>();
        /// <summary>
        /// Generic dosya yükleme kodu
        /// </summary>
        /// <param name="file">Form File</param>
        /// <param name="folderPath">Dosyanın kayıt Yolu</param>
        /// <param name="fileName">Dosya Adı</param>
        /// <returns></returns>
        string SaveImage(IFormFile file,string folderPath,string fileName);

        /// <summary>
        /// Sayı Getit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// silinmemiş kayıtları getir
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// Silinmemiş ve aktif kayıtları getir
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<int> CountNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// Generick Any COde
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool Any<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// silinmemiş any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyNonDeleted<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// Sİlinmemiş ve Aktif Ant Code
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> AnyNonDeletedAndActive<T>(Expression<Func<T, bool>> expression) where T : BaseObject;
        /// <summary>
        /// Aplication Ket Control
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        Task<bool> CheckAppKey(string Key);
        
    }
}
