using DataAccess.Data.Models;

namespace API.Services
{
    public interface ICoffeeShopService
    {
        Task<List<CoffeeShop>> GetList();
    }
}
