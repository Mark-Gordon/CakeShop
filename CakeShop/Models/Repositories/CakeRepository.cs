using CakeShop.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Repositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly AppDbContext _appDbContext;

        public CakeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Cake GetCakeById(int id)
        {
            return _appDbContext.Cakes.FirstOrDefault(c => c.CakeId == id);
        }

        public IEnumerable<Cake> AllCakes
        {
            get
            {
                return _appDbContext.Cakes.Include(c => c.Category);
            }
        }


    }
}
