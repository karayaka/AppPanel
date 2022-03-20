using AppPanel.BLL.Interfaces;
using AppPanel.DAL.Classes.AdminClasses;
using AppPanel.DAL.Enums;
using AppPanel.Sevices.Models.BaseModels;
using AppPanel.Sevices.Models.SecurityModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private IConfiguration configuration;

        public SecurityController(IUnitOfWork _uow, IMapper _mapper, IConfiguration _configuration)
        {
            uow = _uow;
            mapper = _mapper;
            configuration = _configuration;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm]LoginModel model)
        {
            try
            {
                var user = await uow.SecurityRepository.Login(model.UserName, model.Password);
                if (user == null)
                    return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Kullanıcı Adı veya Şifre Hatalı"));

                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);

                var tokenDecriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Surname, user.Surname),
                        new Claim(ClaimTypes.Email, user.Email),
                    }),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDecriptor);
                var tokenstring = tokenHandler.WriteToken(token);

                var resultModel = new LoginResultModel()
                {
         
                    Email = user.Email,
                    ID = user.ID,
                    Name = user.Name,
                    SurName = user.Surname,
                    Token = tokenstring,
                };
                //return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Giriş İşleminde Bir Hata Oluştu"));
                return Ok(new ResultModel<LoginResultModel>(_Data: resultModel));
            }
            catch (Exception)
            {
                return Ok(new ResultModel<int>(_Data: -1, _Type: ResuldStatus.danger, _Message: "Giriş İşleminde Bir Hata Oluştu"));
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register()
        {
            try
            {
                var admin = new AdminUser()
                {
                    Email = "cagri.karayaka@gmail.com",
                    Name = "Çağrı",
                    Surname = "Karayaka",
                    Password = "55315531",
                    UserName = "cagri",
                };
                await uow.SecurityRepository.AddUser(admin);

                await uow.SaveChangesAsync();

                return Ok(new ResultModel<int>(_Data: 1, _Type: ResuldStatus.succes, _Message: "Kayıt"));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }
      
        
    }
}
