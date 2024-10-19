using Booking.Application.Bookings.Commands.CreateBooking;
using Booking.Application.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Booking.API.Endpoints;


public class CreateBookingEndpoint : ICarterModule
{
    private record CreateBookingRequest(BookingDto Booking);
    private record CreateBookingResponse(Guid Id);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("bookings", async (CreateBookingRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateBookingCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateBookingResult>();
            return Results.Created($"bookings/{response.Id}", response);
        })        
        .WithName("CreateBooking")
        .Produces<CreateBookingResult>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Booking")
        .WithDescription("Create Booking");
    }
}
