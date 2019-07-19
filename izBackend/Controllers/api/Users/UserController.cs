using izBackend.Controllers.Interfaces;
using izBackend.Models.Requests;
using izBackend.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace izBackend.Controllers.api.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : IzController
    {
        private readonly IStringLocalizer<UserStrings> _localizer;
        public UserController(IAuthenticateService authService , IStringLocalizer<UserStrings> localizer)
        {
            InitAuth(authService);
            _localizer = localizer;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<object> Get()
        {
            return AuthService().GetUserID(User);
        }

        
    }
}