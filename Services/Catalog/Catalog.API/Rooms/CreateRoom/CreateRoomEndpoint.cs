using Catalog.API.Data.Enums;

namespace Catalog.API.Rooms.CreateRoom
{
    public record CreateRoomRequest(
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
    public record CreateRoomResponse(Guid RoomId);
    public class CreateRoomEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("rooms", async (CreateRoomRequest createRoomRequest, ISender sender) =>
            {
                var command = createRoomRequest.Adapt<CreateRoomCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRoomResponse>();

                return Results.Created($"rooms/{response.RoomId}", response);
            })
            .WithName("CreateRoom")
            .Produces<CreateRoomResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Room")
            .WithDescription("Create Room")
            .RequireAuthorization(/*policy => policy.RequireRole("Host")*/);
        }
    }

}
