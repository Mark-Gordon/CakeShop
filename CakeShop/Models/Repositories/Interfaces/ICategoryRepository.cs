using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Repositories.Interfaces
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> AllCategories { get; }
    }
}
