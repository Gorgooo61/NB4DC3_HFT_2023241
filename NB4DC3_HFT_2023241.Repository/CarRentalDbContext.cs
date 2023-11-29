using Microsoft.EntityFrameworkCore;
using NB4DC23_HFT_2023241.Models;

namespace NB4DC3_HFT_2023241.Repository
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext()
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                //string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\CarRental.mdf;Integrated Security=True;MultipleActiveResultSets=True";
                builder.UseLazyLoadingProxies().UseInMemoryDatabase("UwU");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasOne(c => c.Brand).WithMany(brand => brand.Cars).HasForeignKey(c => c.BrandID).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().HasOne(o => o.Car).WithMany(car => car.Orders).HasForeignKey(o => o.CarID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brand>().HasData(new Brand[]
            {
                new Brand("1#Ferrari#Italy"),
                new Brand("2#Mercedes#Germany"),
                new Brand("3#McLaren#English"),
                new Brand("4#Suzuki#Japan")
            });

            modelBuilder.Entity<Car>().HasData(new Car[]
            {
                new Car("4#1#499PM"),
                new Car("3#2#AMG_GT_63"),
                new Car("2#3#720S"),
                new Car("1#4#Swift")
            });

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order("4#1#2"),
                new Order("3#2#3"),
                new Order("2#3#1"),
                new Order("1#4#7")
            });
        }
    }
}
