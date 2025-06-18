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
    public interface IBrandService
    {
        Task<BrandResponse<Pagination<Brands>>> GetAll(int page, int pageSize);
        Task<BrandResponse<BrandDTO>> GetById(int id);
        Task<BrandResponse<object>> Create(BrandRequest dto);
        Task<BrandResponse<object>> Update(BrandRequest dto, int id);
        Task<BrandResponse<object>> Delete(int id);
    }
}