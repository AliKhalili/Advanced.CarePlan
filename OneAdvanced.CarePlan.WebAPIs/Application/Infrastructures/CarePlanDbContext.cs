using Microsoft.EntityFrameworkCore;

namespace OneAdvanced.CarePlan.WebAPIs.Application.Infrastructures
{
    public class CarePlanDbContext : DbContext
    {
        public CarePlanDbContext(DbContextOptions<CarePlanDbContext> options) : base(options)
        {
        }
        public virtual DbSet<CarePlanEntity> CarePlans { get; set; }
    }
}