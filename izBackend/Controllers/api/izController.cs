using izBackend.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace izBackend.Controllers.Interfaces
{
    public abstract class IzController : ControllerBase
    {
   

        private IAuthenticateService _authService;
        private IUserManagementService _userManagementService;


        [NonAction]
        public IAuthenticateService AuthService()
        {
            return _authService;
        }
        [NonAction]
        public void InitAuth(IAuthenticateService authenticateService)
        {
            _authService = authenticateService;
        }
        [NonAction]
        public IUserManagementService UserService()
        {
            return _userManagementService;
        }
        [NonAction]
        public void InitUserManagement(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

     

    }
}