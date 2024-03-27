using CoronaAPI.Data;
using CoronaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoronaAPI.DAL
{
    public class ManufactorManager
    {
        public static async Task<IEnumerable<Manufactor>> GetAllManufactorsAsync(CoronaAPIContext context)
        {
            return await context.Manufactors.ToListAsync();
        }
    }
}
