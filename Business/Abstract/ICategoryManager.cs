using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryManager
    {
        List<Category> GetAll();
        Category Get(int id);
        Category GetByName(string name);
        void Add(Category category);
        void Delete(Category category);
        void Update(Category category); 
    }
}
