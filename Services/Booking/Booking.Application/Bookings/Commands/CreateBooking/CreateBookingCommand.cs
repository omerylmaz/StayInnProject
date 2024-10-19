using BuildingBlocks.CQRS;
using FluentValidation;
using Booking.Application.Dtos;

namespace Booking.Application.Bookings.Commands.CreateBooking;

public record CreateBookingCommand(BookingDto Booking) : ICommand<CreateBookingResult>;

public record CreateBookingResult(Guid Id);


public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.Booking.BookingName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Booking.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}