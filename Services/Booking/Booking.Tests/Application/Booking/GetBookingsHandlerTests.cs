using AutoFixture;
using Booking.Application.Bookings.Queries.GetBookings;
using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Moq;
using BookingModel = Booking.Domain.Models.Booking;

namespace Booking.Tests.Application.Booking;

public class GetBookingsHandlerTests : TestBase
{
    private readonly GetBookingsHandler _handler;

    public GetBookingsHandlerTests()
    {
        _handler = new GetBookingsHandler(_dbContextMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectPaginatedResult()
    {
        // Arrange
        var bookings = _fixture.CreateMany<BookingModel>(10);
        // daha fazla sahte veri ekleyin

        var mockSet = new Mock<DbSet<BookingModel>>();
        //mockSet.As<IQueryable<BookingModel>>().Setup(m => m.Provider).Returns(bookings.Provider);
        //mockSet.As<IQueryable<BookingModel>>().Setup(m => m.Expression).Returns(bookings.Expression);
        //mockSet.As<IQueryable<BookingModel>>().Setup(m => m.ElementType).Returns(bookings.ElementType);
        mockSet.As<IQueryable<BookingModel>>().Setup(m => m.GetEnumerator()).Returns(bookings.GetEnumerator());

        _dbContextMock.Setup(x => x.Bookings).Returns(mockSet.Object);
        _dbContextMock.Setup(x => x.Bookings.CountAsync(It.IsAny<CancellationToken>())).ReturnsAsync(bookings.Count());

        var request = new GetBookingsQuery(new PaginationRequest { PageIndex = 0, PageSize = 10 });


        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.PaginationRequest.PageIndex, result.PaginatedResult.PageIndex);
        Assert.Equal(request.PaginationRequest.PageSize, result.PaginatedResult.PageSize);
        Assert.Equal(bookings.Count(), result.PaginatedResult.Count);
        Assert.Equal(bookings.Count(), result.PaginatedResult.Items.Count());
    }
}
