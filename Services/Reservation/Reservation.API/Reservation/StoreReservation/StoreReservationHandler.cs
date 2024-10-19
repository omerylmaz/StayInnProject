using Reservation.API.Data;
using Discount.Grpc;
using MediatR;

namespace Reservation.API.Reservation.StoreReservation;

public record StoreReservationCommand(ReservationModel Reservation) : ICommand<StoreReservationResult>;

public record StoreReservationResult(string UserId);

public class StoreReservationHandler(IReservationRepository reservationRepository, 
    DiscountProtoService.DiscountProtoServiceClient discountService) 
    : ICommandHandler<StoreReservationCommand, StoreReservationResult>
{
    public async Task<StoreReservationResult> Handle(StoreReservationCommand request, CancellationToken cancellationToken)
    {
        await DeductDiscount(request.Reservation);

        var response = await reservationRepository.StoreReservation(request.Reservation, cancellationToken);

        return new(response.RoomId.ToString());
    }

    private async Task DeductDiscount(ReservationModel reservationCart)
    {
        var coupon = await discountService.GetDiscountAsync(new() { Name = reservationCart.CouponName });

        reservationCart.TotalPrice = coupon.Type switch
        {
            CouponType.Percentage => reservationCart.TotalPrice * (100 - coupon.Amount) / 100,
            CouponType.FixedAmount => reservationCart.TotalPrice - coupon.Amount
        };
    }
}
