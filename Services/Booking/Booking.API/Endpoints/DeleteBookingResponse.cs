using Booking.Application.Bookings.Commands.DeleteBooking;
using Carter;
using Mapster;
using MediatR;

namespace Booking.API.Endpoints;

public class DeleteBookingEndpoint : ICarterModule
{
    //private record DeleteBookingRequest(Guid Id);
    private record DeleteBookingResponse(bool IsSuccess);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/bookings/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBookingCommand(id));
            var response = result.Adapt<DeleteBookingResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteBooking")
        .Produces<DeleteBookingResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Booking")
        .WithDescription("Delete Booking");
    }
}
