using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Models;
using Booking.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasConversion(
                roomId => roomId.Value,
                contextId => RoomId.Of(contextId));

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            //builder.HasData(
            //    Room.Create(RoomId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 500),
            //    Room.Create(RoomId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 400),
            //    Room.Create(RoomId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 650),
            //    Room.Create(RoomId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", 450));
        }
    }
}
