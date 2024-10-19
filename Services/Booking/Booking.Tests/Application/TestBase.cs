using AutoFixture;
using Booking.Application.Data;
using Booking.Infrastructure.Data;
using Booking.Tests.Domain.Customizations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Booking.Tests.Application;
public class TestBase
{
    public readonly IFixture _fixture;
    protected readonly Mock<IBookingDbContext> _dbContextMock;
    public TestBase()
    {
        _fixture = new Fixture();
        _dbContextMock = new Mock<IBookingDbContext>();
        var options = new DbContextOptionsBuilder<BookingDbContext>()
                            .UseInMemoryDatabase(databaseName: "SomeDatabaseInMemory")
                            .Options;
        _dbContextMock.Setup(db => db.Bookings).Returns(new BookingDbContext(options).Bookings);
        //_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
        //    .ForEach(b => _fixture.Behaviors.Remove(b));
        //_fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        //_fixture.Customize(new BookingDtoCustomization());
    }
}
