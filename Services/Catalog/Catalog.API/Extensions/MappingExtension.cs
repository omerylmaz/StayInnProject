using Catalog.API.Helpers;
using Catalog.API.Rooms.GetRoomById;
using Catalog.API.Rooms.GetRooms;

namespace Catalog.API.Extensions
{
    public static class MappingExtension
    {
        public static void RegisterMappings()
        {
            var config = TypeAdapterConfig.GlobalSettings;

            //config.NewConfig<Room, GetRoomResult>()
            //    .Map(dest => dest.RoomType, src => src.RoomType.GetDisplayName());

            //config.NewConfig<Room, GetRoomByIdResult>()
            //    .Map(dest => dest.RoomType, src => src.RoomType.GetDisplayName());
        }
    }
}
