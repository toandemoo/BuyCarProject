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
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService, ILogger<BrandController> logger)
        {
            _logger = logger;
            _brandService = brandService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10)
        {
            var res = await _brandService.GetAll(page, pageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var res = await _brandService.GetById(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BrandRequest dto)
        {
            var res = await _brandService.Create(dto);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _brandService.Delete(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BrandRequest dto)
        {
            var res = await _brandService.Update(dto, id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

    }
}