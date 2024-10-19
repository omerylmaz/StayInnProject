namespace Reservation.API.Reservation.StoreReservation;

public record StoreReservationRequest(ReservationModel Reservation);

public record StoreReservationResponse(string UserId);

public class StoreReservationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("reservation", async (StoreReservationRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreReservationCommand>();
            var response = (await sender.Send(command))
                .Adapt<StoreReservationResponse>();

            return Results.Created($"reservation/{response.UserId}" ,response);
        })
            .WithName("CreateReservation")
            .Produces<StoreReservationResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Reservation")
            .WithDescription("Create Reservation"); ;
    }
}
