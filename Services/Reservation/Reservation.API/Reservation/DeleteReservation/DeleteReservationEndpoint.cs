
namespace Reservation.API.Reservation.DeleteReservation;

public record DeleteReservationResponse(bool IsSuccess);

public class DeleteReservationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("reservation/{userId}", async (Guid userId, ISender sender) =>
        {
            var response = (await sender.Send(new DeleteReservationCommand(userId)))
                .Adapt<DeleteReservationResponse>();

            return Results.Ok(response);
        })
            .WithName("DeleteReservation")
            .Produces<DeleteReservationResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Reservation")
            .WithDescription("Delete Reservation"); ;
    }
}