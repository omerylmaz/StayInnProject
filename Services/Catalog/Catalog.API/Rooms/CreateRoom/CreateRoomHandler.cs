using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;

namespace Catalog.API.Rooms.CreateRoom;

public record CreateRoomCommand(
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
        List<string> ImageFiles)
    : ICommand<CreateRoomResult>;
public record CreateRoomResult(Guid RoomId);
internal sealed class CreateRoomHandler(IDocumentSession session) : ICommandHandler<CreateRoomCommand, CreateRoomResult>
{
    public async Task<CreateRoomResult> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = request.Adapt<Room>();

        session.Store(room);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateRoomResult(room.Id);
    }
}
