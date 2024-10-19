using FluentValidation;

namespace Reservation.API.Reservation.StoreReservation
{
    public class StoreReservationValidator : AbstractValidator<StoreReservationCommand>
    {
        public StoreReservationValidator()
        {
            RuleFor(x => x.Reservation).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Reservation.GuestId).NotEmpty().WithMessage("GuestId is required");
            RuleFor(x => x.Reservation.CheckInDate)
                .LessThan(x => x.Reservation.CheckOutDate)
                .WithMessage("Check-in date should not greater than check-out date");
        }
    }
}
