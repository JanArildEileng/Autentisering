﻿namespace Autentisering.RefitApi.Services
{
    public interface IIdentityService
    {
        Task<string> Login(string userName = "TestUSer", string password = "Password");

        Task<string> GetAuthorizationCode(string client_id, string userName, string password);

        Task<string> GetIdToken(string authorizationCode);
        Task<string> GetAccessToken(string authorizationCode);
        Task<string> GetUserinfo(string AccessToken);

    }
}