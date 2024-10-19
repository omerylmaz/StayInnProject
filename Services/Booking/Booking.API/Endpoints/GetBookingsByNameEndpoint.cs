using Booking.Application.Bookings.Commands.CreateBooking;
using Booking.Application.Bookings.Queries.GetBookingsByRoom;
using Booking.Application.Dtos;
using Carter;
using Mapster;
using MediatR;
using System.Collections.Generic;

namespace Booking.API.Endpoints;

public class GetBookingsByNameEndpoint : ICarterModule
{
    private record GetBookingsByNameResponse(HashSet<BookingDto> Booking);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/bookings/{bookingName}", async (string bookingName, ISender sender) =>
        {
            var result = await sender.Send(new GetBookingsByNameQuery(bookingName));
            var response = result.Adapt<GetBookingsByNameResponse>();
            return Results.Ok(response);
        })
        .WithName("GetBookingsByName")
        .Produces<CreateBookingResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Bookings By Name")
        .WithDescription("Get Bookings By Name");
    }
}
