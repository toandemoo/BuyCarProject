using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Request;
using Project.Services.interfaces;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarTypeController : ControllerBase
    {
        private readonly ILogger<CarTypeController> _logger;
        private readonly ICarTypeService _carTypeService;
        public CarTypeController(ICarTypeService carTypeService, ILogger<CarTypeController> logger)
        {
            _logger = logger;
            _carTypeService = carTypeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize)
        {
            var res = await _carTypeService.GetAll(page, pageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var res = await _carTypeService.GetById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CarTypeRequest dto)
        {
            var res = await _carTypeService.Create(dto);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _carTypeService.Delete(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CarTypeRequest dto)
        {
            var res = await _carTypeService.Update(dto, id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}