using Discount.Grpc.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
    {
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
    new Coupon
    {
        Id = Guid.NewGuid(),
        Name = "Summer10",
        Description = "Summer discount",
        Amount = 150,
        Type = CouponType.FixedAmount,
        ValidFrom = new DateTime(2024, 1, 1),
        ValidTo = new DateTime(2024, 12, 31),
        IsActive = true
    },
    new Coupon
    {
        Id = Guid.NewGuid(),
        Description = "Spring discount",
        Name = "Spring10",
        Amount = 100,
        Type = CouponType.Percentage,
        ValidFrom = new DateTime(2024, 1, 1),
        ValidTo = new DateTime(2024, 6, 30),
        IsActive = true
    }
);

        }
    }
}
