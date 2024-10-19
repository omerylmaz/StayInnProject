using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Booking.Application.Data;
using Booking.Application.Bookings.Commands.CreateBooking;
using Booking.Infrastructure.Data;
using Booking.Application.Dtos;
using Booking.Tests.Domain.Customizations;

namespace Booking.Tests.Application.Booking;

public class CreateBookingCommandTests : TestBase
{

    public CreateBookingCommandTests()
    {
        _fixture.Customizations.Add(new BookingDtoSpecimenBuilder());
    }

    [Fact]
    public async Task CreateBookingCommand_Should_Create_Booking()
    {
        var bookingDto = _fixture
            .Create<BookingDto>();

        //var bookingDtoUpdated = bookingDto with { Payment = bookingDto.Payment with { Cvv = "105" } };

        var handler = new CreateBookingHandler(_dbContextMock.Object);

        var result = await handler.Handle(new CreateBookingCommand(bookingDto), CancellationToken.None);

        result.Should().NotBeNull();
    }
}
