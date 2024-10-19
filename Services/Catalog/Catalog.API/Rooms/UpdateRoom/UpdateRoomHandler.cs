using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;

namespace Catalog.API.Rooms.UpdateRoom;

public record UpdateRoomCommand(
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
        List<string> ImageFiles) : ICommand<UpdateRoomResult>;

public record UpdateRoomResult(bool IsSuccess);

public class UpdateRoomHandler(IDocumentSession session) : ICommandHandler<UpdateRoomCommand, UpdateRoomResult>
{
    public async Task<UpdateRoomResult> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await session.LoadAsync<Room>(request.Id, cancellationToken);
        if (room == null)
            return new UpdateRoomResult(false);

        room.RoomType = request.RoomType;
        room.Capacity = request.Capacity;
        room.PricePerNight = request.PricePerNight;
        room.IsAvailable = request.IsAvailable;
        room.ImageFiles = request.ImageFiles;

        session.Update(room);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateRoomResult(true);
    }
}

