using FluentValidation;

namespace Reservation.API.Reservation.CheckoutReservation;

public class ConfirmReservationDtoValidator : AbstractValidator<ConfirmReservationCommand>
{
    public ConfirmReservationDtoValidator()
    {
        RuleFor(x => x.ConfirmReservation.ReservationId)
            .NotEmpty().WithMessage("Reservation ID is required.");

        RuleFor(x => x.ConfirmReservation.GuestId)
            .NotEmpty().WithMessage("Guest ID is required.");

        //RuleFor(x => x.ConfirmReservation.HotelId)
        //    .NotEmpty().WithMessage("Hotel ID is required.");

        RuleFor(x => x.ConfirmReservation.RoomId)
            .NotEmpty().WithMessage("Room ID is required.");

        RuleFor(x => x.ConfirmReservation.CheckInDate)
            .NotEmpty().WithMessage(errorMessage: "Check-in date is required.")
            .LessThan(x => x.ConfirmReservation.CheckOutDate).WithMessage("Check-in date must be before the check-out date.");

        RuleFor(x => x.ConfirmReservation.CheckOutDate)
            .NotEmpty().WithMessage("Check-out date is required.")
            .WithMessage("Check-out date must be after the check-in date.");

        RuleFor(x => x.ConfirmReservation.TotalPrice)
            .GreaterThan(0).WithMessage("Total price must be greater than zero.");

        RuleFor(x => x.ConfirmReservation.NightQuantity)
            .GreaterThan(0).WithMessage("Night quantity must be greater than zero.");

        RuleFor(x => x.ConfirmReservation.Payment.CardName)
            .NotEmpty().WithMessage("Card name is required.")
            .MinimumLength(2).WithMessage("Card name must be at least 2 characters long.");

        RuleFor(x => x.ConfirmReservation.Payment.CardNumber)
            .NotEmpty().WithMessage("Card number is required.")
            .CreditCard().WithMessage("Invalid credit card number.");

        RuleFor(x => x.ConfirmReservation.Payment.Expiration)
            .NotEmpty().WithMessage("Expiration date is required.")
            .Matches(@"^(0[1-9]|1[0-2])\/([0-9]{2})$").WithMessage("Expiration date must be in MM/YY format.");

        RuleFor(x => x.ConfirmReservation.Payment.CVV)
            .NotEmpty().WithMessage("CVV is required.")
            .Length(3, 4).WithMessage("CVV must be 3 or 4 digits long.");

        RuleFor(x => x.ConfirmReservation.Payment.PaymentMethod)
            .InclusiveBetween(1, 3).WithMessage("Payment method is invalid.");
    }

}
