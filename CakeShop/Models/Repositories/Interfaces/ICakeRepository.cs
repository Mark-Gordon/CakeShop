using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.Repositories
{
    public interface ICakeRepository
    {
        IEnumerable<Cake> AllCakes { get; }

        Cake GetCakeById(int id);


    }
}
