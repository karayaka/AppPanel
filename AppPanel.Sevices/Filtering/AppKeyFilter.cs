using AppPanel.DAL.DataContext;
using AppPanel.Sevices.Models.BaseModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppPanel.Sevices.Filtering
{
    public class AppKeyFilter : ResultFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var db = context.HttpContext.RequestServices.GetRequiredService<ServiceContext>();
            //var key=context.HttpContext.Request.Headers["AppKey"].ToString();
            //var retVal = db.PanelApps.Any(t => t.AppKey == key);
            //if(!retVal)
            //    context.Result= new HttpResponseMessage()
            //{
            //    StatusCode = HttpStatusCode.Unauthorized,
            //    Content = new StringContent("Unauthorized User")
            //};

        }
    }
}
