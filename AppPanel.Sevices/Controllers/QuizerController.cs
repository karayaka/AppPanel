using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.EnglishQuizerClasses;
using AppPanel.DAL.Enums;
using AppPanel.Sevices.Models.BaseModels;
using AppPanel.Sevices.Models.QuizerModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class QuizerController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public QuizerController(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }

        [HttpGet("GetLevel/{search}/{pageID}")]
        public async Task<IActionResult> GetLevel(string search = "all", int pageID = 1)//string olarak serach alnacak
        {
            try
            {

                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Level>(t => true);
                if (search != "all")
                {
                    models = models.Where(t => t.LevelName.ToLower().Contains(search.ToLower())
                     || t.LevelDesc.ToLower().Contains(search.ToLower()));
                }

                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 10) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models, pageID, 10);
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

        [HttpPost("CreateLevel")]
        public async Task<IActionResult> CreateLevel([FromForm] LevelModel model)
        {
            try
            {
                var level = mapper.Map<Level>(model);
                await uow.BaseRepositoriy.Add(level);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<Level>(_Data: level, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("UpdateLevel")]
        public async Task<IActionResult> UpdateLevel([FromForm] LevelModel model)
        {
            try
            {
                var level = await uow.BaseRepositoriy.GetByID<Level>(model.ID);
                level.LevelName = model.LevelName;
                level.LevelDesc = model.LevelDesc;
                await uow.BaseRepositoriy.Update(level);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<Level>(_Data: level, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("DeleteLevel/{ID}")]
        public async Task<IActionResult> DeleteLevel(int ID)
        {
            try
            {
                await uow.BaseRepositoriy.Delete<Level>(ID);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetTopic/{search}/{pageID}/{levelID?}")]
        public async Task<IActionResult> GetTopic(string search, int pageID, int levelID)
        {
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
                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 10) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models, pageID, 10);
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

        [HttpGet("GetLevelSelectList")]
        public async Task<IActionResult> GetLevelSelectList()
        {
            try
            {
                var selects = await (await uow.BaseRepositoriy.GetNonDeletedAndActive<Level>(t => true))
                    .Select(s => new SelectModel()
                    {
                        Text = s.LevelName,
                        Value = s.ID,
                    }).ToListAsync();
                return Ok(new ResultModel<List<SelectModel>>(_Data: selects, _Type: ResuldStatus.succes));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("CreateTopic")]
        public async Task<IActionResult> CreateTopic([FromForm] TopicModel model)
        {
            try
            {
                var topic = mapper.Map<Topic>(model);
                await uow.BaseRepositoriy.Add(topic);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<Topic>(_Data: topic, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("UpdateTopic")]
        public async Task<IActionResult> UpdateTopic([FromForm] TopicModel model)
        {
            try
            {
                var topic = await uow.BaseRepositoriy.GetByID<Topic>(model.ID);
                topic.TopicName = model.TopicName;
                topic.TopicDesc = model.TopicDesc;
                topic.LevelID = model.LevelID;
                await uow.BaseRepositoriy.Update(topic);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<Topic>(_Data: topic, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("DeleteTopic/{ID}")]
        public async Task<IActionResult> DeleteTopic(int ID)
        {
            try
            {
                await uow.BaseRepositoriy.Delete<Topic>(ID);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }


        [HttpGet("GetTest/{search}/{pageID}/{topiclID?}")]
        public async Task<IActionResult> GetTest(string search, int topiclID, int pageID)
        {
            try
            {
                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Test>(t => true);
                if (topiclID != 0)
                {
                    models = models.Where(t => t.TopicID == topiclID);
                }
                if (search != "all")
                {
                    models = models.Where(t => t.TestName.ToLower().Contains(search.ToLower())
                     || t.TestDesc.ToLower().Contains(search.ToLower()));
                }
                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 10) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models, pageID, 10);
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

        [HttpGet("GetTopicSelectList")]
        public async Task<IActionResult> GetTopicSelectList()
        {
            try
            {
                var selects = await (await uow.BaseRepositoriy.GetNonDeletedAndActive<Topic>(t => true))
                    .Select(s => new SelectModel()
                    {
                        Text = $"{s.Level.LevelName} / {s.TopicName}",
                        Value = s.ID,
                    }).ToListAsync();
                return Ok(new ResultModel<List<SelectModel>>(_Data: selects, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("CreateTest")]
        public async Task<IActionResult> CreateTest([FromForm] TestModel model)
        {
            try
            {
                var test = mapper.Map<Test>(model);
                await uow.BaseRepositoriy.Add(test);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("UpdateTest")]
        public async Task<IActionResult> UpdateTest([FromForm] TestModel model)
        {
            try
            {
                var test = await uow.BaseRepositoriy.GetByID<Test>(model.ID);
                test.TestName = model.TestName;
                test.TestDesc = model.TestDesc;
                test.TopicID = model.TopicID;
                test.ShowTestStartDesc = model.ShowTestStartDesc;
                test.TestStartDesc = model.TestStartDesc;
                test.AdsStatus = model.AdsStatus;

                await uow.BaseRepositoriy.Update(test);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("DeleteTest/{ID}")]
        public async Task<IActionResult> DeleteTest(int ID)
        {
            try
            {
                await uow.BaseRepositoriy.Delete<Test>(ID);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("ChangeTestStatus")]
        public async Task<IActionResult> ChangeTestStatus([FromForm] CahngeTestStatusModel model)
        {
            try
            {
                var testq = await uow.BaseRepositoriy.GetQueryableByID<Test>(model.ID);
                testq = testq.Include(i => i.Questions);
                var test = await testq.FirstOrDefaultAsync();
                if (test.Questions.Count <= 0)
                    return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.warning, _Message: "Soru Olamaya Bir Testi Yayınlayamazsınız!"));

                test.TestStatus = model.TestStatus;
                await uow.BaseRepositoriy.Update(test);
                var retVal = await uow.SaveChangesAsync();
                if (retVal > 0)
                    return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetTestSelectList")]
        public async Task<IActionResult> GetTestSelectList()
        {
            try
            {
                var selects = await (await uow.BaseRepositoriy.GetNonDeletedAndActive<Test>(t => true))
                    .Select(s => new SelectModel()
                    {
                        Text = s.TestName,
                        Value = s.ID,
                    }).ToListAsync();
                return Ok(new ResultModel<List<SelectModel>>(_Data: selects, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetQuestions/{search}/{pageID}/{testID?}")]
        public async Task<IActionResult> GetQuestions(string search, int pageID, int testID)
        {
            try
            {
                var models = await uow.BaseRepositoriy.GetNonDeletedAndActive<Question>(t => true);
                if (testID != 0)
                {
                    models = models.Where(t => t.TestID == testID);
                }
                if (search != "all")
                {
                    models = models.Where(t => t.QuestionDesc.ToLower().Contains(search.ToLower())
                     || t.QuestionNumber.ToString().ToLower().Contains(search.ToLower()));
                }
                var pageCount = uow.BaseRepositoriy.GetPageCount(models, 10) + 1;
                models = await uow.BaseRepositoriy.GetPaginate(models, pageID, 10);
                var retVal = await models.Select(s => new QuestionListModel()
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
                }).ToListAsync();
                return Ok(new ResultModel<List<QuestionListModel>>(_Data: retVal, _PageCount: pageCount, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("GetQuestion/{testID}/{questionNo}")]
        public async Task<IActionResult> GetQuestion(int testID, int questionNo)
        {
            try
            {
                var Question = await (await uow.BaseRepositoriy.GetNonDeletedAndActive<Question>(t => t.QuestionNumber == questionNo &&
                 t.TestID == testID)).Select(s => new QuestionListModel()
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

                return Ok(new ResultModel<QuestionListModel>(_Data: Question, _PageCount: questionNo, _Type: ResuldStatus.succes));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion([FromForm]QuestionModel model)
        {
            try
            {
                //bu bölüm konrol edilecek
                var question = mapper.Map<Question>(model);

                await uow.QuizerRepository.AddQuestion(question);
                await uow.SaveChangesAsync();
                return Ok(new ResultModel<Question>(_Data: question, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion([FromForm] QuestionModel model)
        {
            try
            {
                var question = await uow.BaseRepositoriy.GetByID<Question>(model.ID);

                question.AnsverA = model.AnsverA;
                question.AnsverB = model.AnsverB;
                question.AnsverC = model.AnsverC;
                question.AnsverD = model.AnsverD;
                question.AnswerDesc = model.AnswerDesc;
                question.CorrectAnswer = model.CorrectAnswer;
                question.QuestionDesc = model.QuestionDesc;
                //question.TestID = model.TestID;

                await uow.BaseRepositoriy.Update(question);
                await uow.SaveChangesAsync();

                return Ok(new ResultModel<Question>(_Data: question, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpGet("DeleteQuestion/{ID}")]
        public async Task<IActionResult> DeleteQuestion(int ID)
        {
            try
            {
                await uow.BaseRepositoriy.Delete<Question>(ID);
                await uow.SaveChangesAsync();
                return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }
       

    }
}
