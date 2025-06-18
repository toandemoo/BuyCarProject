using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using Project.Request;
using ProjectBE.Dtos;
using StackExchange.Redis;

namespace Project.Repository.interfaces
{
   public interface IOrderRepository : IRepository<Orders>
   {
      Task<List<Orders>> GetAllOrdersByUserID(int userid);
      Task<Orders> GetDetailOrder(int userid, int orderid);
      Task UpdateOrder(OrderStatusEnum orderStatusEnum, int userid);
   }
}