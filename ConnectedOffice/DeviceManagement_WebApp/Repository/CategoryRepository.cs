using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using System;
using System.Linq;

namespace DeviceManagement_WebApp.Repository
{
    //This inherits the ICategoryRepository and then implement the interface
    //Here interface definitions of additional methods are added
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ConnectedOfficeContext context) : base(context)
        {
        }

        //Creats a new id for a new Category
        public Category CreateID(Category category)
        {
            category.CategoryId = Guid.NewGuid();
            return category;
        }

        //Checks if a category exists by looking at its id
        public bool CategoryExists(Guid? id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}

