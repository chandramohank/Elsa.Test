using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa.Activities.Workflows.Extensions;
using Elsa.Models;
using Elsa.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;
        private readonly IWorkflowInvoker invoker;

        public TestController(ILogger<TestController> logger, IWorkflowInvoker invoker)
        {
            _logger = logger;
            this.invoker = invoker;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await this.invoker.TriggerSignalAsync("RegisterUser", Variables.Empty);
            return Ok();
        }
    }
}
