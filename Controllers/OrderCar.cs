using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectBE.Repository.interfaces;

namespace ProjectBE.Controllers
{
    [Route("api/[controller]")]
    public class OrderCar : Controller
    {
        private readonly ILogger<OrderCar> _logger;

        private readonly IOrderCarRepository _orderCarRepository;

        public OrderCar(IOrderCarRepository orderCarRepository, ILogger<OrderCar> logger)
        {
            _logger = logger;
            _orderCarRepository = orderCarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ordercars = await _orderCarRepository.GetAllAsync();
            return Ok(ordercars);
        }
    }
}