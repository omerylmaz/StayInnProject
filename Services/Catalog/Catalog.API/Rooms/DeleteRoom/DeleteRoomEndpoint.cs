namespace Catalog.API.Rooms.DeleteRoom;

public record DeleteRoomResponse(bool IsSuccess);
public class DeleteRoomEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("rooms/{id}", async (Guid id, ISender sender) =>
        {
            var result = (await sender.Send(new DeleteRoomCommand(id)))
                .Adapt<DeleteRoomResponse>();

            return Results.Ok(result);
        })
            .WithName("DeleteRoom")
            .Produces<DeleteRoomResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Room")
            .WithDescription("Delete Room");
    }
}
