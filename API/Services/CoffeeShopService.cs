using DataAccess.Data;
using DataAccess.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _Context;
        public CoffeeShopService(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<List<CoffeeShop>> GetList()
        {
            return await _Context.Set<CoffeeShop>().ToListAsync();
        }
    }
}
