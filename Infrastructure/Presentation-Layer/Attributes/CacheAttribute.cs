using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Service_Abstraction;
using System.Text;

namespace Presentation_Layer.Attributes
{
    public class CacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInSec;

        public CacheAttribute(int durationInSec = 90)
        {
            _durationInSec = durationInSec;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            string cacheKey = CreateCacheKey(context.HttpContext.Request);

            try
            {
                var cacheValue = await cacheService.GetAsync(cacheKey);

                if (cacheValue is not null)
                {
                    context.Result = new ContentResult
                    {
                        Content = cacheValue,
                        ContentType = "application/json",
                        StatusCode = StatusCodes.Status200OK
                    };
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Redis] Cache GET failed: {ex.Message}");
            }

            var executedContext = await next.Invoke();

            if (executedContext.Result is OkObjectResult result)
            {
                try
                {
                    await cacheService.SetAsync(cacheKey, result.Value, TimeSpan.FromSeconds(_durationInSec));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Redis] Cache SET failed: {ex.Message}");
                }
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append($"{request.Path}?");

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }

            return key.ToString().TrimEnd('&');
        }
    }
}
