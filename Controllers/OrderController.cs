using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities;
using Project.Request;
using Project.Services.interfaces;
using ProjectBE.Dtos.Request;
using StackExchange.Redis;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _OrderService;

        public OrderController(IOrderService OrderService, ILogger<OrderController> logger)
        {
            _logger = logger;
            _OrderService = OrderService;
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize)
        {
            var res = await _OrderService.GetAllOrders(page, pageSize);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByUserId()
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var res = await _OrderService.GetAllOrdersByUserID(userid);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpGet("{orderid}")]
        [Authorize]
        public async Task<IActionResult> GetByUserId([FromRoute] int orderid)
        {
            var userid = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var res = await _OrderService.GetDetailOrder(userid, orderid);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var res = await _OrderService.Delete(id);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] OrderRequest dto)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
            var res = await _OrderService.Create(dto, userId);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }

        [HttpPatch("{orderid}")]
        public async Task<IActionResult> Update([FromBody] OrderUpdateRequest order, [FromRoute] int orderid)
        {
            var res = await _OrderService.Update(order.orderStatusEnum, orderid);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}