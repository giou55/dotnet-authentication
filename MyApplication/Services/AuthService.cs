using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using MyApplication.Interfaces;

namespace MyApplication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public AuthService(
            IHttpContextAccessor httpContextAccessor,
            IDataProtectionProvider dpp)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtectionProvider = dpp;
        }

        public string SignIn()
        {
            var protector = _dataProtectionProvider.CreateProtector("auth-cookie");
            _httpContextAccessor.HttpContext.Response.Headers["set-cookie"] = 
                $"auth={protector.Protect("user:george")}";
            return "ok";
        } 
    }
}
