using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Entities;
using Project.Repository;

namespace ProjectBE.Repository.interfaces
{
    public interface IOrderCarRepository : IRepository<OrderCars>
    {
        Task<List<OrderCars>> GetOrderCarsByOrderId(int orderid);
    }
}