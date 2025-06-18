using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Request;
using Project.Response;

namespace Project.Services.interfaces
{
    public interface ICarTypeService
    {
        Task<CarTypeResponse<Pagination<CarTypes>>> GetAll(int page, int pageSize);
        Task<CarTypeResponse<CarTypeDTO>> GetById(int id);
        Task<CarTypeResponse<object>> Create(CarTypeRequest dto);
        Task<CarTypeResponse<object>> Update(CarTypeRequest dto, int id);
        Task<CarTypeResponse<object>> Delete(int id);
    }
}