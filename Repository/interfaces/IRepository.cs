using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();  // Lấy tất cả các bản ghi
        Task<T> GetByIdAsync(int id);        // Lấy bản ghi theo Id
        Task AddAsync(T entity);             // Thêm mới một bản ghi
        Task UpdateAsync(T entity, int id);          // Cập nhật bản ghi
        Task DeleteAsync(int id);            // Xóa bản ghi
    }
}