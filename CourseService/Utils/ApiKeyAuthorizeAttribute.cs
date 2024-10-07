using CourseService.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace CourseService.Utils
{
    public class ApiKeyAuthorizeAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IOptions<ApiSetting> apiSetting = context.HttpContext.RequestServices.GetService<IOptions<ApiSetting>>();
            string validApiKey = apiSetting.Value.Key;
            string queryParams = apiSetting.Value.QueryKey;

            if (!context.HttpContext.Request.Query.TryGetValue(queryParams, out var potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!validApiKey.Equals(potentialApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}