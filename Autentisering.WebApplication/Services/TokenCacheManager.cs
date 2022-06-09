﻿using Autentisering.WebBFFApplication.AppServices.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Autentisering.WebBFFApplication.Services
{
    public class TokenCacheManager
    {
        const string TokenKey = "AccessToken";
        private readonly ILogger<TokenCacheManager> logger;
        private readonly IMemoryCache memoryCache;
        private readonly IIdentityAndAccessApiService identityService;
        MemoryCacheEntryOptions memoryCacheEntryOptions;


        private string Key(string username) => $"{TokenKey}:{username}";
        private string RefreshKey(string username) => $"Refresh:{username}";


        public TokenCacheManager(ILogger<TokenCacheManager> logger, IMemoryCache memoryCache)
        {
            this.logger = logger;
            this.memoryCache = memoryCache;
            memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5));

        }

        public void SetToken(string username, string accessToken, string refreshToken)
        {

            if (accessToken != null)
            {
                logger.LogInformation("Add token to memoryCache");
                memoryCache.Set(Key, accessToken, memoryCacheEntryOptions);
            }

            if (refreshToken != null)
            {
                logger.LogInformation("Add refreshToken to memoryCache");
                memoryCache.Set(RefreshKey, refreshToken, memoryCacheEntryOptions);
            }


        }

        public async Task<(string, string)> GetToken(string username)
        {
            string accessToken;
            string refreshToken;
            if (memoryCache.TryGetValue(Key, out accessToken))
            {
            }
            if (memoryCache.TryGetValue(RefreshKey, out refreshToken))
            {
            }


            return (accessToken, refreshToken);
        }


    }
}
