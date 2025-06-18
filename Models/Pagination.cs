using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Pagination
{
    public class Pagination<T>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
        public Pagination(int pageNumber, int pageSize, int totalItems, List<T> items)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((decimal)totalItems / PageSize);
            TotalItems = totalItems;
            Items = items;
        }
    }
}