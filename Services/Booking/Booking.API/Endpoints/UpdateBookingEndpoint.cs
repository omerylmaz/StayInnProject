using Booking.Application.Bookings.Commands.UpdateBooking;
using Booking.Application.Bookings.Commands.UpdateBooking;
using Booking.Application.Dtos;
using Carter;
using Mapster;
using MediatR;

namespace Booking.API.Endpoints;

public class UpdateBookingEndpoint : ICarterModule
{
    private record UpdateBookingRequest(BookingDto Booking);
    private record UpdateBookingResponse(Guid Id);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/bookings/{id}", async (Guid id, UpdateBookingRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateBookingCommand>();
            var updatedCommand = command with { Id = id };
            var result = await sender.Send(updatedCommand);
            var response = result.Adapt<UpdateBookingResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateBooking")
        .Produces<UpdateBookingResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Booking")
        .WithDescription("Update Booking");
    }
}
