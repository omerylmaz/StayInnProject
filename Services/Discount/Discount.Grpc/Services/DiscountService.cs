using Discount.Grpc.Data;
using Discount.Grpc.Data.Models;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.Where(x => x.Name == request.Name).FirstOrDefaultAsync();

            if (coupon is null)
            {
                return new CouponModel { Name = "No Discount", Amount = 0, Description = "" };
            }

            var response = coupon.Adapt<CouponModel>();
            return response;

        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var discount = new Coupon()
            {
                Id = Guid.NewGuid(),
                Name = request.Coupon.Name,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description ?? string.Empty,
                IsActive = request.Coupon.IsActive,
                Type = request.Coupon.Type,
                ValidFrom = DateTime.Parse(request.Coupon.ValidFrom),
                ValidTo = DateTime.Parse(request.Coupon.ValidTo)
            };

            if (discount is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            var response = await dbContext.AddAsync(discount);

            await dbContext.SaveChangesAsync();

            var couponModel = response.Entity.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = new Coupon()
            {
                Id = Guid.Parse(request.Coupon.Id),
                Name = request.Coupon.Name,
                Amount = request.Coupon.Amount,
                Description = request.Coupon.Description,
                IsActive = request.Coupon.IsActive,
                Type = request.Coupon.Type,
                ValidFrom = DateTime.Parse(request.Coupon.ValidFrom),
                ValidTo = DateTime.Parse(request.Coupon.ValidTo)
            };

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            var response = dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            return response.Entity.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var response = await dbContext.Coupons.FirstOrDefaultAsync(x => x.Name == request.RoomName);

            if (response is null)
                throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found!!!"));
            dbContext.Coupons.Remove(response);
            await dbContext.SaveChangesAsync();
            var state = dbContext.Entry(response).State == EntityState.Detached;
            return new DeleteDiscountResponse { Success = state };
        }
    }
}
