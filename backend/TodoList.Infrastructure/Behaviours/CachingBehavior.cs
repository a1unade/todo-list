using System.Reflection;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TodoList.Application.Common.Attributes;

namespace TodoList.Infrastructure.Behaviours;

public class CachingBehavior<TRequest, TResponse>(IMemoryCache cache) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull 
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");

        var cacheableAttribute = request.GetType().GetCustomAttribute<CacheableRequestAttribute>();

        if (cacheableAttribute != null)
        {
            string cacheKey = request.GetType().FullName!;
            
            if (cache.TryGetValue(cacheKey, out TResponse? cachedResponse) && cachedResponse is not null)
                return cachedResponse;
            
            var response = await next(cancellationToken);
            cache.Set(cacheKey, response, TimeSpan.FromSeconds(cacheableAttribute.ExpirationInSeconds));
            return response;
        }
        
        return await next(cancellationToken);
    }
}
