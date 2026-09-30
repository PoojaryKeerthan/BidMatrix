using API.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IAppLogger _log;
        public TestController(IAppLogger appLogger)
        {
            _log = appLogger;
            _log.LogClassEntry();
        }
        [HttpGet]
        public IActionResult TestLogging()
        {
            _log.LogMethodEntry("TestLogging Method started");
            _log.Info("Testing the Logging");
            _log.LogMethodExit("Test completed successFully!");
            return Ok("Logging test completed");

        }
    }
}
