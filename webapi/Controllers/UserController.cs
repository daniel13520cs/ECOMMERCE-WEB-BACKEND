using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet(Name = "User")]
        public ActionResult<int> Get()
        {
            // Store a value in the session
            //_httpContextAccessor.HttpContext?.Session.SetInt32("UserId", 1);

            // Retrieve a value from the session
            int userId = _httpContextAccessor.HttpContext?.Session.GetInt32("UserId") ?? -1;

            var user = new
            {
                UserId = userId
            };

            return Ok(user);
        }
    }
}
