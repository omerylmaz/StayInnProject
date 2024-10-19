using BuildingBlocks.CQRS;
using Catalog.API.Data.Enums;
using Catalog.API.Data.Models;
using Marten.Pagination;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Catalog.API.Rooms.GetRooms;

public record GetRoomsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetRoomsResult>;

public record GetRoomsResult(IEnumerable<GetRoomResult> Rooms);


public record GetRoomResult(
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


internal sealed class GetRoomsHandler(IQuerySession session) : IQueryHandler<GetRoomsQuery, GetRoomsResult>
{
    public async Task<GetRoomsResult> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await session.Query<Room>()
                                 .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        var result = rooms.Adapt<IEnumerable<GetRoomResult>>().ToList();

        GetRoomsResult roomsResult = new(result);

        return roomsResult;
    }
}
