using AppPanel.BLL.DTO.QuizAppDTO;
using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.EnglishQuizerClasses;
using AppPanel.DAL.DataContext;
using AppPanel.DAL.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.BLL.Bussenes
{
    public class QuizerRepository : AppBaseRepository, IQuizerRepository
    {
        private readonly ServiceContext context;

        private int UserID;

        public QuizerRepository(ServiceContext _context, IHttpContextAccessor httpContextAccessor):base(_context, httpContextAccessor)
        {
            context = _context;
            var val = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (val != null)
                UserID = Convert.ToInt32(val.Value);
        }

        public async Task<Question> AddQuestion(Question question)
        {
            try
            {
                question.QuestionNumber = await GetQuestionNumber(question.TestID);
                await Add(question);
                return question;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        public async Task<int> GetQuestionNumber(int TestID)
        {
            try
            {
                return context.Questions.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted&&t.Status==Status.Active&&t.TestID==TestID)+1;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<IQueryable<Level>> GetReadyPaginatedLevels(int PageID)
        {
            throw new NotImplementedException();
        }


        public string TestStatusEnumToStr(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.Ready:
                    return "Yayında";
                    break;
                case TestStatus.GettingReady:
                    return "Hazırlanıyor";
                    break;
                default:
                    return "";
                    break;
            }
        }


        public string AdStatusEnumToStr(AdsStatus status)
        {
            switch (status)
            {
                case AdsStatus.NoAds:
                    return "Reklam Yok";
                    break;
                case AdsStatus.InterstitialAd:
                    return "Geçiş Reklamı";
                    break;
                case AdsStatus.RewardedAd:
                    return "Ödüllü Reklam";
                    break;
                default:
                    return "";
                    break;
            }

        }


        public string CorrectAnsverToStr(CorrectAnswer ansver)
        {
            switch (ansver)
            {
                case CorrectAnswer.AnsvwerA:
                    return "Cevap A";                    
                case CorrectAnswer.AnsvwerB:
                    return "Cevap B";                
                case CorrectAnswer.AnsvwerC:
                    return "Cevap C";
                case CorrectAnswer.AnsvwerD:
                    return "Cevap D";
                default:
                    return "";
            }
        }

        public async Task<MobilDashbordDTO> GetMobilDashbord()
        {
            try
            {
                var model = new MobilDashbordDTO();

                model.LevelCount = await CountNonDeletedAndActive<Level>(t=>true);
                model.TopicCount = await CountNonDeletedAndActive<Topic>(t=>true);
                model.TestCount = await CountNonDeletedAndActive<Test>(t=>true);
                model.QuetionCount = await CountNonDeletedAndActive<Question>(t=>true);

                return model;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}