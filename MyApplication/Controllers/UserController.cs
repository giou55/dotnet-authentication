using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public UserController(
            IHttpContextAccessor httpContextAccessor,
            IDataProtectionProvider dpp)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataProtectionProvider = dpp;
        }


        [HttpGet(Name = "GetUser")]
        public string Get()
        {
            //var protector = _dataProtectionProvider.CreateProtector("auth-cookie");
            //var authCookie = _httpContextAccessor.HttpContext.Request
            //    .Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth="));
            //var protectedPayload = authCookie.Split("=").Last(); 
            //var payload = protector.Unprotect(protectedPayload);
            //var parts = payload.Split(":");
            //var keys = parts[0];
            //var value = parts[1];
            //return value ?? "no value found";

            if(_httpContextAccessor.HttpContext.User is not null) 
            return _httpContextAccessor.HttpContext.User.FindFirst("user").Value;

            return "No user found";

            //{
            //    "claims":[],
            //    "identities":[{
            //        "authenticationType":null,
            //        "isAuthenticated":false,
            //        "actor":null,
            //        "bootstrapContext":null,
            //        "claims":[],
            //        "label":null,
            //        "name":null,
            //        "nameClaimType":"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
            //        "roleClaimType":"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
            //    }],
            //    "identity":{
            //        "name":null,
            //        "authenticationType":null,
            //        "isAuthenticated":false
            //    }
            //}
        }
    }
}
