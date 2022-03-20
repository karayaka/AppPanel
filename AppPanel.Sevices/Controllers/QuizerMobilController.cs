using AppPanel.BLL.DTO.QuizAppDTO;
using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.EnglishQuizerClasses;
using AppPanel.DAL.Enums;
using AppPanel.Sevices.Models.BaseModels;
using AppPanel.Sevices.Models.QuizerModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizerMobilController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public QuizerMobilController(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        [HttpGet("GetLevel/{search}/{pageID}")]
        public async Task<IActionResult> GetLevel(string search = "all", int pageID = 1)//string olarak serach alnacak
        {
            var appKey = HttpContext.Request.Headers["AppKey"].ToString();
            var result= await uow.BaseRepositoriy.CheckAppKey(appKey);
            if (!result)
                return Unauthorized(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Uygulama Erişim Keyi Hatalı"));
            try
            {

                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Level>(t => true);
                if (search != "all")
                {
                    models = models.Where(t => t.LevelName.ToLower().Contains(search.ToLower())
                     || t.LevelDesc.ToLower().Contains(search.ToLower()));
                }

                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 20) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models.OrderBy(o=>o.ID), pageID, 20);
                var retVAl = await models.Select(s =>
                 new LevelModel()
                 {
                     ID = s.ID,
                     LevelDesc = s.LevelDesc,
                     LevelName = s.LevelName,
                     TopicCount = s.Topics.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active),
                 }).ToListAsync();

                return Ok(new ResultModel<List<LevelModel>>(_Data: retVAl, _PageCount: pageCount, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetTopic/{search}/{pageID}/{levelID?}")]
        public async Task<IActionResult> GetTopic(string search="all", int pageID=1, int levelID=0)
        {
            var appKey = HttpContext.Request.Headers["AppKey"].ToString();
            var result = await uow.BaseRepositoriy.CheckAppKey(appKey);
            if (!result)
                return Unauthorized(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Uygulama Erişim Keyi Hatalı"));
            try
            {
                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Topic>(t => true);
                if (levelID != 0)
                {
                    models = models.Where(t => t.LevelID == levelID);
                }
                if (search != "all")
                {
                    models = models.Where(t => t.TopicName.ToLower().Contains(search.ToLower())
                     || t.TopicDesc.ToLower().Contains(search.ToLower()));
                }
                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 20) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models.OrderBy(o => o.ID), pageID, 20);
                var retVal = await models.Select(s => new TopicListModel()
                {
                    ID = s.ID,
                    TopicName = s.TopicName,
                    LevelName = s.Level.LevelName,
                    LevelID = s.LevelID,
                    TopicDesc = s.TopicDesc,
                    TestCount = s.Tests.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted && t.Status == Status.Active)
                }).ToListAsync();

                return Ok(new ResultModel<List<TopicListModel>>(_Data: retVal, _PageCount: pageCount, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetTest/{search}/{pageID}/{topiclID?}")]
        public async Task<IActionResult> GetTest(string search="all", int topiclID=0, int pageID=1)
        {
            var appKey = HttpContext.Request.Headers["AppKey"].ToString();
            var result = await uow.BaseRepositoriy.CheckAppKey(appKey);
            if (!result)
                return Unauthorized(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Uygulama Erişim Keyi Hatalı"));
            try
            {
                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Test>(t => t.TestStatus==TestStatus.Ready);
                if (topiclID != 0)
                {
                    models = models.Where(t => t.TopicID == topiclID);
                }
                if (search != "all")
                {
                    models = models.Where(t => t.TestName.ToLower().Contains(search.ToLower())
                     || t.TestDesc.ToLower().Contains(search.ToLower()));
                }
                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 20) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models.OrderBy(o => o.ID), pageID, 20);
                var retVal = await models.Select(s => new TestListModel()
                {
                    ID = s.ID,
                    TopicID = s.TopicID,
                    TopicName = s.Topic.TopicName,
                    TestName = s.TestName,
                    TestDesc = s.TestDesc,
                    QuestionCount = s.Questions.Count(t => t.ObjectStatus == ObjectStatus.NonDeleted),
                    AdsStatus = Convert.ToInt32(s.AdsStatus),
                    AdsStatusStr = uow.QuizerRepository.AdStatusEnumToStr(s.AdsStatus),
                    TestStartDesc = s.TestStartDesc,
                    TestStatus = Convert.ToInt32(s.TestStatus),
                    ShowTestStartDesc = s.ShowTestStartDesc
                }).ToListAsync();

                return Ok(new ResultModel<List<TestListModel>>(_Data: retVal, _PageCount: pageCount, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetQuestion/{testID}/{questionNo}")]
        public async Task<IActionResult> GetQuestion(int testID, int questionNo)
        {
            var appKey = HttpContext.Request.Headers["AppKey"].ToString();
            var result = await uow.BaseRepositoriy.CheckAppKey(appKey);
            if (!result)
                return Unauthorized(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Uygulama Erişim Keyi Hatalı"));
            try
            {
                var Questions = await uow.BaseRepositoriy.GetNonDeletedAndActive<Question>(t => t.TestID == testID);
                var question= await  Questions.Where(t=>t.QuestionNumber==questionNo).Select(s => new QuestionListModel()
                 {
                     ID = s.ID,
                     QuestionNumber = s.QuestionNumber,
                     QuestionDesc = s.QuestionDesc,
                     Test = s.Test.TestName,
                     AnsverA = s.AnsverA,
                     AnsverB = s.AnsverB,
                     AnsverC = s.AnsverC,
                     AnsverD = s.AnsverD,
                     AnswerDesc = s.AnswerDesc,
                     CorrectAnswer = Convert.ToInt32(s.CorrectAnswer),
                     CorrectAnswerStr = uow.QuizerRepository.CorrectAnsverToStr(s.CorrectAnswer)

                 }).FirstOrDefaultAsync();

                return Ok(new ResultModel<QuestionListModel>(_Data: question, _PageCount: Questions.Count(), _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("MobilDasbord")]
        public async Task<IActionResult> MobilDasbord()
        {
            var appKey = HttpContext.Request.Headers["AppKey"].ToString();
            var result = await uow.BaseRepositoriy.CheckAppKey(appKey);
            if (!result)
                return Unauthorized(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Uygulama Erişim Keyi Hatalı"));
            try
            {
                var model = await uow.QuizerRepository.GetMobilDashbord();
                return Ok(new ResultModel<MobilDashbordDTO>(_Data: model, _Type: ResuldStatus.succes));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

    }
}
