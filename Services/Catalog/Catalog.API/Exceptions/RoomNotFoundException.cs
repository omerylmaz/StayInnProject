using BuildingBlocks.Exceptions;
using System.Runtime.Serialization;

namespace Catalog.API.Exceptions
{
    internal class RoomNotFoundException : NotFoundException
    {
        public RoomNotFoundException(Guid Id) : base("Room", Id)
        {
        }

        public RoomNotFoundException(string? message) : base(message)
        {
        }

        public RoomNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}