using Microsoft.Extensions.Caching.Memory;

namespace TempRel.Middlewares
{
    public class RequestThrottlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMemoryCache cache;

        public RequestThrottlingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            this.next = next;
            this.cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            int maxRequests = 100;
            TimeSpan period = TimeSpan.FromMinutes(1);

            String clientIp = context.Connection.RemoteIpAddress.ToString();
            string cacheKey = $"RequestThrottler_{clientIp}";

            if(!cache.TryGetValue(cacheKey, out int requestCount))
            {
                requestCount = 0;
            }

            if (requestCount >= maxRequests) 
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. PLease try again later. :)");
                return;
            }

            cache.Set(cacheKey, requestCount + 1, period);

            await next(context);
        }
    }

    public static class RequestThrottlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestThrottling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestThrottlingMiddleware>();
        }
    }
}
