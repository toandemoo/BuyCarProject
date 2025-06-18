using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Response;

namespace Project.Services.interfaces
{
    public interface ICarService
    {
        Task<CarResponse<Pagination<CarDTO>>> GetAllCar(int page, int pageSize);
        Task<CarResponse<CarDTO>> GetCarById(int id);
        Task<CarResponse<object>> Create(CarRequest dto);
        Task<CarResponse<object>> Update(CarRequest dto, int id);
        Task<CarResponse<object>> Delete(int id);
    }
}