using System;
using System.Threading.Tasks;
using Discount.Grpc.Data;
using Discount.Grpc.Data.Models;
using Discount.Grpc.Services;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Discount.Grpc.Tests.Services
{
    public class DiscountServiceTests
    {
        private readonly DiscountContext _dbContext;

        public DiscountServiceTests()
        {
            var options = new DbContextOptionsBuilder<DiscountContext>()
                .UseInMemoryDatabase(databaseName: "DiscountTestDb")
                .Options;

            _dbContext = new DiscountContext(options);

            _dbContext.Coupons.Add(new Coupon { Id = Guid.NewGuid(), Name = "TestCoupon", Amount = 10 });
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetDiscount_ShouldReturnCouponModel_WhenCouponExists()
        {
            var service = new DiscountService(_dbContext);
            var request = new GetDiscountRequest { Name = "TestCoupon" };

            var result = await service.GetDiscount(request, null);

            Assert.Equal("TestCoupon", result.Name);
            Assert.Equal(10, result.Amount);
        }

        [Fact]
        public async Task CreateDiscount_ShouldAddCoupon_WhenRequestIsValid()
        {
            var service = new DiscountService(_dbContext);
            var request = new CreateDiscountRequest
            {
                Coupon = new CouponModel
                {
                    Amount = -5,
                    Description = "deneme",
                    IsActive = true,
                    Name = "Summer10",
                    Type = CouponType.Percentage,
                    ValidFrom = DateTime.Now.AddDays(5).ToString(),
                    ValidTo = DateTime.Now.AddDays(10).ToString()
                }
            };


            var result = await service.CreateDiscount(request, null);

            Assert.NotNull(result);
        }
    }
}
