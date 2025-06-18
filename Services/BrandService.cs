using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Repository.interfaces;
using Project.Request;
using Project.Response;
using Project.Services.interfaces;

namespace Project.Services
{
    public class BrandService : IBrandService
    {
        private readonly ILogger<BrandService> _logger;
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository, ILogger<BrandService> logger)
        {
            _logger = logger;
            _brandRepository = brandRepository;
        }

        public async Task<BrandResponse<object>> Create(BrandRequest dto)
        {
            try
            {
                var brand = new Brands
                {
                    Name = dto.Name
                };

                await _brandRepository.AddAsync(brand);

                return new BrandResponse<object> { Message = "List Brands", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Error Create new Brand: {Message}", e.Message);
                return new BrandResponse<object> { Message = $"Error Create new Brand", Success = false };
            }
        }

        public async Task<BrandResponse<object>> Delete(int id)
        {
            try
            {
                await _brandRepository.DeleteAsync(id);

                return new BrandResponse<object> { Message = $"Delete Brand with ID ({id}) successfully", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found Brand with ID ({id}): {Message}", id, e.Message);
                return new BrandResponse<object> { Message = $"Not Found Brand with ID ({id})", Success = false };
            }
        }

        public async Task<BrandResponse<Pagination<Brands>>> GetAll(int page, int pageSize)
        {
            try
            {
                var brands = await _brandRepository.GetAllAsync();
                var totalItems = brands.Count();
                var itemPerPage = brands.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                Pagination<Brands> data = new Pagination<Brands>(page, pageSize, totalItems, itemPerPage);
                // var brandsdto = brands.Select(c => new BrandDTO
                // {
                //     Id = c.Id,
                //     Name = c.Name
                // });
                return new BrandResponse<Pagination<Brands>> { Message = "List Brands", Success = true, Data = data };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found Brand: {Message}", e.Message);
                return new BrandResponse<Pagination<Brands>> { Message = "Not Found Brand", Success = false };
            }
        }

        public async Task<BrandResponse<BrandDTO>> GetById(int id)
        {
            try
            {
                var brand = await _brandRepository.GetByIdAsync(id);
                var brandsdto = new BrandDTO
                {
                    Id = brand.Id,
                    Name = brand.Name
                };

                return new BrandResponse<BrandDTO> { Message = "List Brands", Success = true, Data = brandsdto};
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found Brand with ID ({id}): {Message}", id, e.Message);
                return new BrandResponse<BrandDTO> { Message = $"Not Found Brand with ID ({id})", Success = false };
            }
        }

        public async Task<BrandResponse<object>> Update(BrandRequest dto, int id)
        {
            try
            {
                var oldbrand = await _brandRepository.GetByIdAsync(id);

                var newbrand = new Brands
                {
                    Name = dto.Name
                };

                await _brandRepository.UpdateAsync(newbrand, id);

                return new BrandResponse<object> { Message = $"Update Brand with ID ({id}) successfully", Success = true};
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found Brand with ID ({id}): {Message}", id, e.Message);
                return new BrandResponse<object> { Message = $"Not Found Brand with ID ({id})", Success = false };
            }
        }
    }
}