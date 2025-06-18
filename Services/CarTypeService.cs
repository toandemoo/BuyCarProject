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
    public class CarTypeService : ICarTypeService
    {
        private readonly ILogger<CarTypeService> _logger;
        private readonly ICarTypeRepository _carTypeRepository;
        public CarTypeService(ICarTypeRepository carTypeRepository, ILogger<CarTypeService> logger)
        {
            _logger = logger;
            _carTypeRepository = carTypeRepository;
        }

        public async Task<CarTypeResponse<object>> Create(CarTypeRequest dto)
        {
            try
            {
                var cartype = new CarTypes
                {
                    Name = dto.Name
                };

                await _carTypeRepository.AddAsync(cartype);

                return new CarTypeResponse<object> { Message = "Create new CarType successfully", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Error Create new CarType: {Message}", e.Message);
                return new CarTypeResponse<object> { Message = $"Error Create new CarType", Success = false };
            }
        }

        public async Task<CarTypeResponse<object>> Delete(int id)
        {
            try
            {
                await _carTypeRepository.DeleteAsync(id);

                return new CarTypeResponse<object> { Message = $"Delete CarType with ID ({id}) successfully", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found CarType with ID ({id}): {Message}", id, e.Message);
                return new CarTypeResponse<object> { Message = $"Not Found CarType with ID ({id})", Success = false };
            }
        }

        public async Task<CarTypeResponse<Pagination<CarTypes>>> GetAll(int page, int pageSize)
        {
            try
            {
                var carTypes = await _carTypeRepository.GetAllAsync();
                var totalItems = carTypes.Count();
                var itemPerPage = carTypes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                Pagination<CarTypes> data = new Pagination<CarTypes>(page, pageSize, totalItems, itemPerPage);

                // var cartypesdto = carTypes.Select(c => new CarTypeDTO
                // {
                //     Id = c.Id,
                //     Name = c.Name
                // });

                return new CarTypeResponse<Pagination<CarTypes>> { Message = "List CarTypes", Success = true, Data = data };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found CarType: {Message}", e.Message);
                return new CarTypeResponse<Pagination<CarTypes>> { Message = "Not Found CarType", Success = false };
            }
        }

        public async Task<CarTypeResponse<CarTypeDTO>> GetById(int id)
        {
            try
            {
                var cartype = await _carTypeRepository.GetByIdAsync(id);
                var cartypedto = new CarTypeDTO
                {
                    Id = cartype.Id,
                    Name = cartype.Name
                };

                return new CarTypeResponse<CarTypeDTO> { Message = "Detail CarType", Success = true, Data = cartypedto };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found CarType with ID ({id}): {Message}", id, e.Message);
                return new CarTypeResponse<CarTypeDTO> { Message = $"Not Found CarType with ID ({id})", Success = false };
            }
        }

        public async Task<CarTypeResponse<object>> Update(CarTypeRequest dto, int id)
        {
            try
            {
                var oldcartype = await _carTypeRepository.GetByIdAsync(id);

                var newcartype = new CarTypes
                {
                    Name = dto.Name
                };

                await _carTypeRepository.UpdateAsync(newcartype, id);

                return new CarTypeResponse<object> { Message = $"Update CarType with ID ({id}) successfully", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Not Found CarType with ID ({id}): {Message}", id, e.Message);
                return new CarTypeResponse<object> { Message = $"Not Found CarType with ID ({id})", Success = false };
            }
        }
    }
}