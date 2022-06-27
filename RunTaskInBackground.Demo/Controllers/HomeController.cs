using Microsoft.AspNetCore.Mvc;

namespace RunTaskInBackground.Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CannonService _cannonService;
        private readonly SomeService _someService;
        public HomeController(ILogger<HomeController> logger, CannonService cannonService, SomeService someService)
        {
            _logger = logger;
            _cannonService = cannonService;
            _someService = someService;
            _someService.From = nameof(HomeController);
        }

        [HttpGet]
        [Route("Async")]
        public IActionResult RunAsync(int timeout = 1000)
        {
            _cannonService.Fire(async (SomeService someService) =>
            {
                someService.From = nameof(CannonService);
                await Task.Delay(timeout);
                someService.DoSomething();
            });
            return Ok();
        }

        [HttpGet]
        [Route("AsyncBoom")]
        public IActionResult RunAsyncBoom(int timeout = 1000)
        {
            _cannonService.Fire(async () =>
            {
                await Task.Delay(timeout);
                _someService.DoSomething();
            });
            return Ok();
        }

        [HttpGet]
        [Route("Sync")]
        public IActionResult RunSync()
        {
            _cannonService.Fire((SomeService someService) =>
            {
                someService.From = nameof(CannonService);
                someService.DoSomething();
            });
            return Ok();
        }
    }
}