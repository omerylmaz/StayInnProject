
using Catalog.API.Data.Models;

namespace Catalog.API.Rooms.DeleteRoom;

public record DeleteRoomCommand(Guid Id) : ICommand<DeleteRoomResult>;

public record DeleteRoomResult(bool IsSuccess);

internal class DeleteRoomHandler(IDocumentSession session) : ICommandHandler<DeleteRoomCommand, DeleteRoomResult>
{
    public async Task<DeleteRoomResult> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        session.Delete<Room>(request.Id);

        await session.SaveChangesAsync(cancellationToken);
        return new DeleteRoomResult(true);
    }
}
