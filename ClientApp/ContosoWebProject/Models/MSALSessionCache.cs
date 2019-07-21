using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoWebProject.Models
{
    public class MSALSessionCache
    {
        internal HttpContext httpContext;

        string UserId = string.Empty;
        string CacheId = string.Empty;
        ITokenCache cache;

        public MSALSessionCache(string userId, HttpContext context)
        {
            // not object, we want the SUB
            UserId = userId;
            CacheId = UserId + "_TokenCache";
            this.httpContext = context;
        }

        public void EnablePersistence(ITokenCache cache)
        {
            this.cache = cache;
            cache.SetBeforeAccessAsync(BeforeAccessNotificationAsync);
            cache.SetAfterAccessAsync(AfterAccessNotificationAsync);
            cache.SetBeforeWriteAsync(BeforeAccessNotificationAsync);
        }

        private async Task AfterAccessNotificationAsync(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                // Reflect changes in the persistent store
                byte[] blob = args.TokenCache.SerializeMsalV3();
                this.httpContext.Session.Set(CacheId, blob);
                this.httpContext.Session.CommitAsync().Wait();
                this.httpContext.Session.Set(CacheId, blob);
                await this.httpContext.Session.CommitAsync();
            }
        }

        private async Task BeforeAccessNotificationAsync(TokenCacheNotificationArgs arg)
        {
            this.httpContext.Session.LoadAsync().Wait();
            await this.httpContext.Session.LoadAsync();
        }
    }
}
