using AutoFixture;
using Booking.Application.Bookings.Queries.GetBookingsByRoom;
using Booking.Application.Dtos;
using Booking.Tests.Domain.Customizations;
using FluentAssertions;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Tests.Application.Booking;

public class GetBookingsByRoomQueryTests : TestBase
{
    public GetBookingsByRoomQueryTests()
    {
        //_fixture.Customizations.Add(new BookingSpecimenBuilder());
    }

    [Fact]
    public async Task GetBookingsQueryByName_Should_Return_Bookings()
    {
        // Arrange
        //var roomName = _fixture.Create<string>();
        var bookings = _fixture.CreateMany<BookingModel>().ToList();
        var bookingName = bookings.First().BookingName;
        var query = new GetBookingsByNameQuery(bookingName);
        //_dbContextMock.Setup(x => x.Bookings).Returns();
        _dbContextMock.Object.Bookings.AddRange(bookings);
        // Act
        var handler = new GetBookingsByNameHandler(_dbContextMock.Object);
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        //result.BookingDto.Should().NotBeEmpty();
        //result.BookingDto.Should().BeEquivalentTo(bookings);
    }
}
