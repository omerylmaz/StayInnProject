using BuildingBlocks.CQRS;
using FluentValidation;
using System.Windows.Input;

namespace Booking.Application.Bookings.Commands.DeleteBooking;

public record DeleteBookingCommand(Guid BookingId) : ICommand<DeleteBookingResult>;

public record DeleteBookingResult(bool IsSuccess);

internal class DeleteBookingCommandValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingCommandValidator()
    {
        RuleFor(x => x.BookingId).NotEmpty().WithMessage("BookingId is required");
    }
}