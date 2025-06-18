using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ProjectBE.Controllers
{
    public class ExceptionController : Controller
    {
        private readonly ILogger<ExceptionController> _logger;

        public ExceptionController(ILogger<ExceptionController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        [HttpGet]
        public IActionResult Index()
        {
            throw new UnauthorizedAccessException("Unauthorized access exception occurred.");
        }
    }
}