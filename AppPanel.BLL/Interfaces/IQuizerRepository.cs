using AppPanel.BLL.DTO.QuizAppDTO;
using AppPanel.DAL.Classes.EnglishQuizerClasses;
using AppPanel.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Interfaces
{
    public interface IQuizerRepository:IAppBaseRepository
    {
        /// <summary>
        /// HazırOlan Levelleri Getr
        /// </summary>
        /// <param name="PageID"></param>
        /// <returns></returns>
        Task<IQueryable<Level>> GetReadyPaginatedLevels(int PageID);

        /// <summary>
        /// Soru Ekle
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        Task<Question> AddQuestion(Question question);

        /// <summary>
        /// Soru Numarasına Göre soru getir
        /// </summary>
        /// <returns></returns>
        Task<int> GetQuestionNumber(int TestID);
        /// <summary>
        /// Test DUrumunu Stribg OLarak Dönen Metod
        /// </summary>
        /// <returns></returns>
        string TestStatusEnumToStr(TestStatus status);
        /// <summary>
        /// AdStatus Strin Metodu
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        string AdStatusEnumToStr(AdsStatus status);
        /// <summary>
        /// Dosru cevabın türkçe tabloda görünümü için yapımaış bir metosd
        /// </summary>
        /// <param name="ansver"></param>
        /// <returns></returns>
        string CorrectAnsverToStr(CorrectAnswer ansver);

        /// <summary>
        /// Mobil dashbor verileri
        /// </summary>
        /// <returns></returns>
        Task<MobilDashbordDTO> GetMobilDashbord(); 


    }
}
