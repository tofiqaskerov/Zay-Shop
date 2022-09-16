using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitieFramework
{
    public class CategoryDal : EfEntityRepositoryBase<Category, AppDbContext>, ICategoryDal
    {
        public Category GetByName(string name)
        {
            using var _context = new AppDbContext();
            var category = _context.Categories.FirstOrDefault(c => c.Name == name);
            return category;
        }
    }
}
