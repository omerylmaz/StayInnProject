using Reservation.API.DTOs;

namespace Reservation.API.Reservation.CheckoutReservation;


public record ConfirmReservationRequest(ConfirmReservationDto ConfirmReservation);
public record ConfirmReservationResponse(bool IsSuccess);
public class ConfirmReservationEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reservation/confirmation", async (ConfirmReservationRequest confirmReservation, ISender sender) =>
        {
            var confirmationCommand = confirmReservation.Adapt<ConfirmReservationCommand>();

            var result = await sender.Send(confirmationCommand);

            var response = result.Adapt<ConfirmReservationResponse>();

            return Results.Ok(response);
        })
        .WithName("ConfirmReservation")
        .Produces<ConfirmReservationResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Confirm Reservation")
        .WithDescription("Confirm Reservation"); ;
    }
}
