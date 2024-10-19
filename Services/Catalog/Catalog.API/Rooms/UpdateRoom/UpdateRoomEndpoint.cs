using Catalog.API.Data.Enums;
using Catalog.API.Rooms.CreateRoom;

namespace Catalog.API.Rooms.UpdateRoom;

public record UpdateRoomRequest(
        RoomTypes RoomType,
        string Title,
        string Description,
        string Address,
        string City,
        string Country,
        decimal PricePerNight,
        Guid OwnerId,
        int RoomNumber,
        short Beds,
        int Capacity,
        bool IsAvailable,
        List<string> ImageFiles);


public class UpdateRoomEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("rooms/{id}", async (Guid id, UpdateRoomRequest request, ISender sender) =>
        {
            var command = new UpdateRoomCommand(id, request.RoomType, request.Title, request.Description, request.Address, request.City, request.Country, request.PricePerNight, request.OwnerId, request.RoomNumber, request.Beds, request.Capacity, request.IsAvailable, request.ImageFiles);

            var result = await sender.Send(command);

            if (!result.IsSuccess)
            {
                return Results.NotFound();
            }

            return Results.Ok(result.IsSuccess);
        })
        .WithName("UpdateRoom")
        .Produces<UpdateRoomResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Room")
        .WithDescription("Update Room");
    }
}
