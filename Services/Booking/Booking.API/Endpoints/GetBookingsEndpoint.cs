using Booking.Application.Bookings.Commands.CreateBooking;
using Booking.Application.Bookings.Queries.GetBookings;
using Booking.Application.Dtos;
using BuildingBlocks.Pagination;
using Carter;
using Mapster;
using MediatR;

namespace Booking.API.Endpoints
{
    public class GetBookingsEndpoint : ICarterModule
    {
        private record GetBookingsResult(PaginatedResult<BookingDto> PaginatedResult);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/bookings", async ([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
            {
                var result = await sender.Send(new GetBookingsQuery(paginationRequest));
                var response = result.Adapt<GetBookingsResult>();

                return Results.Ok(response);
            })
        .WithName("GetBookings")
        .Produces<CreateBookingResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Bookings")
        .WithDescription("Get Bookings");
        }
    }
}
