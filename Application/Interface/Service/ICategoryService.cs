using Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface ICategoryService
    {
        public Category GetCategory(int id);

        public List<Category> GetCategories();

        public bool CreateCategory(Category category);

        public bool UpdateCategory(Category category);
    }
}
