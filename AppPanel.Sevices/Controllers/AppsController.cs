using AppPanel.BLL.DTO.AppsDTO;
using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.AppsClasses;
using AppPanel.DAL.Enums;
using AppPanel.Sevices.Models.BaseModels;
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
    public class AppsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public AppsController(IUnitOfWork _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }
        [HttpGet("GetMainListApps")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMainListApps()
        {
            try
            {
                var models = await uow.AppsRepository.GetAppMainPageList();
                return Ok(new ResultModel<List<AppMainList>>(_Data: models, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception e)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: e.Message));
            }
        }
        [HttpGet("GetApps")]
        public async Task<IActionResult> GetApps()
        {
            try
            {
                var models = await (await uow.BaseRepositoriy.GetNonDeletedAndActive<PanelApp>(t => true))
                    .Select(s=>new AppPanelFormModel() 
                    {
                        ID=s.ID,
                        AppDesc=s.AppDesc,
                        AppKey=s.AppKey,
                        AppName=s.AppName,
                        AppSubDesc=s.AppSubDesc,
                        GitHubUrl=s.GitHubUrl,
                        AppUrl=s.AppUrl,
                        AppCardColors=s.AppCardColors
                        .Where(i => i.ObjectStatus == ObjectStatus.NonDeleted)
                        .Select(c=>c.Color).ToList(),
                        FilesUrls=s.AppImages.Where(i=>i.ObjectStatus==ObjectStatus.NonDeleted)
                        .Select(si=> "https://cagnaz.com/service/Images/appPanel/appImages/" + (si.ImageUr).ToString()).ToList(),//local host yerine host yazılacak
                    }).ToListAsync();

                return Ok(new ResultModel<List<AppPanelFormModel>>(_Data: models, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }
        [HttpPost("CreateApp")]
        public async Task<IActionResult> CreateApp([FromForm]AppPanelFormModel model)
        {
            //color strin olarak geliyo vvirgül ile ayrılıyor
            //https://stackoverflow.com/questions/54519998/upload-multiple-file-with-vue-js-and-axios
            try
            {
                //var form = Request.Form.Files;
                await uow.AppsRepository.CreatePanelApp(model);
                return Ok(new ResultModel<int>(_Data: 1,  _Type: ResuldStatus.succes,_Message:"İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpPut("UpdateApp")]
        public async Task<IActionResult> UpdateApp(AppPanelFormModel model)
        {
            try
            {
                await uow.AppsRepository.UpdatePanelApp(model);
                return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }

        [HttpDelete("DeleteApp/{ID}")]
        public async Task<IActionResult> DeleteApp(int ID)
        {
            try
            {
                await uow.AppsRepository.PanelAppDelete(ID);
                return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "İşlem Başarılı"));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "İşlem Sırasında bir hata olulştu"));
            }
        }




    }
}
