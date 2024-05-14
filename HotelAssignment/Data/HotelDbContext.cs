using HotelAssignment.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelAssignment.Data
{
    public class HotelDbContext:DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
            
        }
        public DbSet<HotelData> HotelData { get; set; }
    }
}
