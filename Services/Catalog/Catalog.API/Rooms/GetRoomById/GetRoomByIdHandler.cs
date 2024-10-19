

using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;

namespace Catalog.API.Rooms.GetRoomById;

public record GetRoomByIdQuery(Guid Id) : IQuery<GetRoomByIdResult>;
public record GetRoomByIdResult(
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


public class GetRoomByIdHandler(IQuerySession session) : IQueryHandler<GetRoomByIdQuery, GetRoomByIdResult>
{
    public async Task<GetRoomByIdResult> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await session.LoadAsync<Room>(request.Id, cancellationToken);

        if (room is null)
        {
            throw new RoomNotFoundException(request.Id);
        }

        var result = room.Adapt<GetRoomByIdResult>();
        return result;
    }
}
