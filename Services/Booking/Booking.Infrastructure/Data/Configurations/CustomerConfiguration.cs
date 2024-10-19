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
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasConversion(customerId => customerId.Value, contextId => CustomerId.Of(contextId));

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(100);

            builder.HasIndex(c => c.Email);

            //builder.HasData(
            //        Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "omer", "omer@gmail.com"),
            //        Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "john", "john@gmail.com")
            //    );
        }
    }
}
