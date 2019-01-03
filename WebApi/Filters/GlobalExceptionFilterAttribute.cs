using GrainInterfaces.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Filters
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is BaseException ex)
            {
                context.Result = new OkObjectResult(new ApiResult
                {
                    Code = ex.Code,
                    Data = context.Exception.GetType().Name
                });
            }
            //else
            //{
            //    context.Result = new OkObjectResult(new ApiResult
            //    {
            //        Code = -1,
            //        Data = context.Exception.Message
            //    });
            //}
            return base.OnExceptionAsync(context);
        }
    }
}
