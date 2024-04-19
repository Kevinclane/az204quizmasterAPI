using Microsoft.AspNetCore.Mvc;

namespace az204quizmasterAPI.Controllers
{
    [ApiController]
    [Route("Ping")]
    public class PingController : ControllerBase
    {
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "Get")]
        public string Get()
        {
            return "Pong";
        }
    }
}
