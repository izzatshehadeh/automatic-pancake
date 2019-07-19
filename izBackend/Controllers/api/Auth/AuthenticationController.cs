using izBackend.Controllers.Interfaces;
using izBackend.Models.Common;
using izBackend.Models.Common.Auth;
using izBackend.Models.Requests;
using izBackend.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace izBackend.Controllers.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : IzController
    {
        private readonly IStringLocalizer<UserStrings> _localizer;

        public AuthenticationController(IUserManagementService userservice, IAuthenticateService authService, IStringLocalizer<UserStrings> localizer)
        {
            InitUserManagement(userservice);
            InitAuth(authService);
            _localizer = localizer;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("request")]
        public IActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AuthService().IsAuthenticated(request, out AuthResponse res))
            {
                APIResponse<AuthResponse> result = new APIResponse<AuthResponse>(res);
                return Ok(result);
            }
            return Ok(new APIResponseError("Failed"));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!UserService().CreateUser(request, out string res))
            {
                return Ok(new APIResponseError(res));
            }

            return Ok(new APIResponseSuccess(_localizer["UserCreatedSuccess"].Value));
        }

        [HttpPost("refresh")]
        public IActionResult RefreshtToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (AuthService().GetUserID(User) == request.UserID)
            {
                if (AuthService().RenewToken(request, out AuthResponse res))
                {
                    APIResponse<AuthResponse> result = new APIResponse<AuthResponse>(res);
                    return Ok(result);
                }
                return Ok(new APIResponseError( _localizer["Invalid User"].Value));
            }
            return Ok(new APIResponseError(_localizer["Invalid User"].Value));
        }
    }
}