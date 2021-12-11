using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DeveloperTest.Controllers
{
    [ApiController, Route("[controller]")]
    public class EngineerController : ControllerBase
    {
        private static readonly string[] Engineers = { "Ashley", "Dave", "Kalina" };

        private readonly ILogger<EngineerController> _logger;

        public EngineerController(ILogger<EngineerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Engineers;
        }
    }
}
