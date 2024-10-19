using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Rooms.GetRoomByAmenities;

public record GetRoomsByTypeQuery(RoomTypes RoomType) : IQuery<GetRoomListByTypeResult>;


public record GetRoomListByTypeResult(IEnumerable<GetRoomByTypeResult> RoomListByType);


public record GetRoomByTypeResult(
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


internal sealed class GetRoomByTypeHandler(IQuerySession session) : IQueryHandler<GetRoomsByTypeQuery, GetRoomListByTypeResult>
{
    public async Task<GetRoomListByTypeResult> Handle(GetRoomsByTypeQuery request, CancellationToken cancellationToken)
    {
        var rooms = await session.Query<Room>()
                                 .Where(x => x.RoomType == request.RoomType)
                                 .ToListAsync(cancellationToken);

        var result = rooms
            .Adapt<IEnumerable<GetRoomByTypeResult>>();

        return new GetRoomListByTypeResult(result);
    }
}