using Catalog.API.Data.Enums;
using Catalog.API.Rooms.CreateRoom;

namespace Catalog.API.Rooms.GetRoomByAmenities;

public record GetRoomListByTypeResponse(IEnumerable<GetRoomByTypeResult> RoomListByType);


public class GetRoomByTypeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("rooms/type", async (HttpContext http, ISender sender) =>
        {
            var roomTypeQuery = http.Request.Query["roomType"].ToString();

            if (Enum.TryParse<RoomTypes>(roomTypeQuery, out var roomType))
            {
                GetRoomsByTypeQuery getRoomsByTypeQuery = new(roomType);

                var rooms = (await sender.Send(getRoomsByTypeQuery))
                            .Adapt<GetRoomListByTypeResponse>();

                return Results.Ok(rooms);
            }

            return Results.BadRequest("Invalid Room Type.");
        })
        .WithName("GetRoomByType")
        .Produces<GetRoomListByTypeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Room By Type")
        .WithDescription("Get Room By Type");
    }
}
