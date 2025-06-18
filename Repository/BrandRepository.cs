using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Data;
using Project.Entities;
using Project.Repository.interfaces;

namespace Project.Repository
{
   public class BrandRepository : Repository<Brands>, IBrandRepository
   {
      public BrandRepository(AppDbContext context) : base(context)
      {
      }
   }
}