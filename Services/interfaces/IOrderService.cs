using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Request;
using Project.Response;
using ProjectBE.Dtos;

namespace Project.Services.interfaces
{
  public interface IOrderService
  {
    Task<OrderResponse<Pagination<OrderDTO>>> GetAllOrders(int page, int pageSize);
    Task<OrderResponse<OrderDTO>> GetOrderById(int id);
    Task<OrderResponse<OrderDTO>> Create(OrderRequest dto, int userId);
    Task<OrderResponse<OrderDTO>> Update(OrderStatusEnum orderStatusEnum, int orderid);
    Task<OrderResponse<OrderDTO>> Delete(int id);
    Task<OrderResponse<List<OrderDTO>>> GetAllOrdersByUserID(int userid);
    Task<OrderResponse<DetailOrderDTO>> GetDetailOrder(int userid, int orderid);
  }
}