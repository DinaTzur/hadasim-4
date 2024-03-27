using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoronaAPI.DAL
{
    public class CityManager
    {
        public static async Task<IEnumerable<City>> GetAllCitiesAsync(CoronaAPIContext context)
        {
            return await context.Cities.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
