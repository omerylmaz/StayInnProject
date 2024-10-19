
using Catalog.API.Data.Enums;
using Catalog.API.Rooms.CreateRoom;
using Catalog.API.Rooms.GetRooms;

public record GetRoomsRequest(int PageNumber, int PageSize);

public record GetRoomListResponse(IEnumerable<GetRoomResponse> Rooms);
public record GetRoomResponse(
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

public class GetRoomsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("rooms", async ([AsParameters] GetRoomsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetRoomsQuery>();

            var rooms = await sender.Send(query);

            var response = rooms.Adapt<GetRoomListResponse>();

            return Results.Ok(response);
        })
        .WithName("GetRooms")
        .Produces<GetRoomListResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Rooms")
        .WithDescription("Get Rooms");
    }
}
