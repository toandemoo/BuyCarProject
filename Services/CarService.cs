using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.DTOs;
using Project.Entities;
using Project.Models.Pagination;
using Project.Repository.interfaces;
using Project.Response;
using Project.Services.interfaces;

namespace Project.Services
{
    public class CarService : ICarService
    {
        private readonly ILogger<CarService> _logger;
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository, ILogger<CarService> logger)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        public async Task<CarResponse<object>> Create(CarRequest dto)
        {
            try
            {
                var car = new Cars
                {
                    Name = dto.Name,
                    LicensePlate = dto.LicensePlate,
                    CarTypeId = dto.CarTypeId,
                    BrandId = dto.BrandId,
                    PricePerDay = dto.PricePerDay,
                    ImageUrl = dto.ImageUrl,
                    Status = dto.Status,
                };

                await _carRepository.AddAsync(car);

                return new CarResponse<object> { Message = "Tạo Car thành công", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi xóa Car: {Message}", e.Message);
                return new CarResponse<object> { Message = "Lỗi tạo Car", Success = false };
            }
        }

        public async Task<CarResponse<object>> Delete(int id)
        {
            try
            {
                await _carRepository.DeleteAsync(id);
                return new CarResponse<object> { Message = "Xóa Car thành công", Success = true };
            }
            catch (Exception e)
            {
                _logger.LogError("Lỗi xóa Car: {Message}", e.Message);
                return new CarResponse<object> { Message = "Lỗi xóa Car", Success = false };
            }
        }

        public async Task<CarResponse<Pagination<CarDTO>>> GetAllCar(int page, int pageSize)
        {
            var cars = await _carRepository.GetAllAsync();
            var totalItems = cars.Count();
            var itemPerPage = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination<CarDTO> data = new Pagination<CarDTO>(page, pageSize, totalItems, itemPerPage.Select(c => new CarDTO(c)).ToList());

            return new CarResponse<Pagination<CarDTO>> { Message = "Danh sách Car", Success = true, Data = data };
        }

        public async Task<CarResponse<CarDTO>> GetCarById(int id)
        {
            try
            {
                var car = await _carRepository.GetByIdAsync(id);

                var cardto = new CarDTO(car);

                return new CarResponse<CarDTO> { Message = "Detail Car", Success = true, Data = cardto };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error get Car với ID {Id}: {Message}", id, e.Message);
                return new CarResponse<CarDTO> { Message = $"Not Found Car with ID: {id}", Success = false };
            }
        }

        public async Task<CarResponse<object>> Update(CarRequest dto, int id)
        {
            try
            {
                var oldcar = await _carRepository.GetByIdAsync(id);

                var newcar = new Cars
                {
                    Name = dto.Name,
                    LicensePlate = dto.LicensePlate,
                    CarTypeId = dto.CarTypeId,
                    BrandId = dto.BrandId,
                    PricePerDay = dto.PricePerDay,
                    ImageUrl = dto.ImageUrl,
                    Status = dto.Status
                };
                await _carRepository.UpdateAsync(newcar, id);

                return new CarResponse<object> { Message = "cập nhật Car thành công", Success = true };

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Lỗi Update Car với ID {Id}: {Message}", id, e.Message);
                return new CarResponse<object> { Message = "Lỗi Update Car", Success = true };
            }
        }
    }
}