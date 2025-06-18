using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Repository.interfaces;
using Project.Request;
using Project.Response;
using Project.Services.interfaces;
using ProjectBE.Dtos;
using ProjectBE.Repository.interfaces;
using StackExchange.Redis;

namespace Project.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;

        private readonly IOrderCarRepository _orderCarRepository;
        public OrderService(IOrderRepository orderRepository, IOrderCarRepository orderCarRepository, ICarRepository carRepository, ILogger<OrderService> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _orderCarRepository = orderCarRepository;
            _carRepository = carRepository;
        }

        public async Task<OrderResponse<OrderDTO>> Create(OrderRequest dto, int userId)
        {
            var order = new Orders
            {
                UserId = userId,
                Status = dto.Status,
            };

            await _orderRepository.AddAsync(order);

            foreach (var orderCars in dto.orderCarRequest)
            {
                var car = await _carRepository.GetByIdAsync(orderCars.CarId);
                // _dbContext.Cars.Attach(car);
                if (car is null)
                {
                    throw new ArgumentNullException(nameof(car));
                }
                var ordercar = new OrderCars
                {
                    CarId = orderCars.CarId,
                    OrderId = order.Id,
                    Quantity = orderCars.Quantity,
                    Car = car
                };

                await _orderCarRepository.AddAsync(ordercar);
            }

            return new OrderResponse<OrderDTO> { Message = $"Create New Order successfully", Success = true };
        }

        public async Task<OrderResponse<OrderDTO>> Delete(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return new OrderResponse<OrderDTO> { Message = $"Delete Order with ID ({id}) successfully", Success = false };
        }

        public async Task<OrderResponse<Pagination<OrderDTO>>> GetAllOrders(int page, int pageSize)
        {

            var rentals = await _orderRepository.GetAllAsync();
            var totalItems = rentals.Count();
            var itemPerPage = rentals.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination<OrderDTO> data = new Pagination<OrderDTO>(
                page,
                pageSize,
                totalItems,
                itemPerPage.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    Status = o.Status,
                }).ToList()
            );

            return new OrderResponse<Pagination<OrderDTO>> { Message = "List Orders", Success = true, Data = data };
        }

        public async Task<OrderResponse<List<OrderDTO>>> GetAllOrdersByUserID(int userid)
        {
            var orders = await _orderRepository.GetAllOrdersByUserID(userid);
            var orderdtos = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                UserId = o.UserId,
                Status = o.Status,
            }).ToList();
            return new OrderResponse<List<OrderDTO>> { Message = "List Orders", Success = true, Data = orderdtos };
        }

        public async Task<OrderResponse<DetailOrderDTO>> GetDetailOrder(int userid, int orderid)
        {
            var orders = await _orderRepository.GetDetailOrder(userid, orderid);

            List<OrderCarsDTO> orderCarsDTOs = new List<OrderCarsDTO>();

            foreach (var c in orders.OrderCars)
            {
                var car = await _carRepository.GetByIdAsync(c.CarId);
                orderCarsDTOs.Add(new OrderCarsDTO
                {
                    OrderId = c.OrderId,
                    CarId = c.CarId,
                    Quantity = c.Quantity,
                    Car = new CarDTO(car)
                });
            }

            var orderdto = new DetailOrderDTO
            {
                Id = orders.Id,
                UserId = orders.UserId,
                CreateAt = orders.CreatedAt,
                Status = orders.Status,
                Orders = orderCarsDTOs

            };

            return new OrderResponse<DetailOrderDTO> { Message = "List Orders", Success = true, Data = orderdto };
        }

        public async Task<OrderResponse<OrderDTO>> GetOrderById(int id)
        {

            var rental = await _orderRepository.GetByIdAsync(id);

            var rentaldto = new OrderDTO
            {
                Id = rental.Id,
                UserId = rental.UserId,
                Status = rental.Status,
            };
            return new OrderResponse<OrderDTO> { Message = $"Detail Order with ID ({id})", Success = true, Data = rentaldto };
        }

        public async Task<OrderResponse<OrderDTO>> Update(OrderStatusEnum orderStatusEnum, int orderid)
        {
            await _orderRepository.UpdateOrder(orderStatusEnum, orderid);
            return new OrderResponse<OrderDTO> { Message = $"Update Order with ID ({orderid}) successfully", Success = true };
        }
    }
}