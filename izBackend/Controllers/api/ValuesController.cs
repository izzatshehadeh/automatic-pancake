using izBackend.Controllers.Interfaces;
using izBackend.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace izBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : IzController
    {
        public ValuesController(IAuthenticateService authService)
        {
            InitAuth(authService);
        }

        // GET api/values

        [Authorize]
        [HttpGet]
        public ActionResult<object> Get()
        {
            return AuthService().GetUserID(User);
        }
    }
}