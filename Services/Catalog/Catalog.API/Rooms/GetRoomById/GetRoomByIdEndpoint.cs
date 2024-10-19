
using Catalog.API.Data.Enums;
using Catalog.API.Rooms.CreateRoom;

namespace Catalog.API.Rooms.GetRoomById;

public record GetRoomByIdResponse(
        Guid Id, 
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

public class GetRoomByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("rooms/{id}", async (Guid id, ISender sender) =>
        {
            var room = await sender.Send(new GetRoomByIdQuery(id));

            var response = room.Adapt<GetRoomByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetRoomById")
        .Produces<GetRoomByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Room By Id")
        .WithDescription("Get Room By Id");
    }
}
