using BuildingBlocks.CQRS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Booking.Application.Dtos;

namespace Booking.Application.Bookings.Commands.UpdateBooking;

public record UpdateBookingCommand(Guid Id, BookingDto Booking) : ICommand<UpdateBookingResult>;

public record UpdateBookingResult(bool isSuccess);

public class UpdateBookingCommandValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Booking.BookingName).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Booking.CustomerId).NotNull().WithMessage("CustomerId is required");
    }
}