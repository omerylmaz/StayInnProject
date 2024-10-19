namespace Reservation.API.Reservation.GetReservation;

public record GetReservationResponse(ReservationModel ReservationCart);

public class GetReservationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("reservation/{userId}", async (Guid userId, ISender sender) =>
        {

            var response = (await sender.Send(new GetReservationQuery(userId)))
                .Adapt<GetReservationResponse>();

            return Results.Ok(response);
        })
            .WithName("GetReservation")
            .Produces<GetReservationResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Reservation")
            .WithDescription("Get Reservation"); ;
    }
}