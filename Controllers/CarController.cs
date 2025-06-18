using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.Services.interfaces;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICarService _carService;
        public CarController(ICarService carService, ILogger<UserController> logger)
        {
            _logger = logger;
            _carService = carService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCar([FromQuery] int page, int pageSize)
        {
            var cars = await _carService.GetAllCar(page, pageSize);
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById([FromRoute] int id)
        {
            var car = await _carService.GetCarById(id);
            if (car == null)
                return BadRequest(car);
            return Ok(car);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CarRequest dto)
        {
            var res = await _carService.Create(dto);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _carService.Delete(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] CarRequest dto, [FromRoute] int id)
        {
            var res = await _carService.Update(dto, id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

    }
}