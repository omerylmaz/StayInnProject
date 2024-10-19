using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Enums;
using Booking.Domain.Models;
using BookingModel = Booking.Domain.Models.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.ValueObjects;

namespace Booking.Infrastructure.Data.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<BookingModel>
    {
        public void Configure(EntityTypeBuilder<BookingModel> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasConversion(
                            bookingId => bookingId.Value,
                            dbId => BookingId.Of(dbId));

            builder.HasOne<Customer>()
              .WithMany()
              .HasForeignKey(o => o.CustomerId)
              .IsRequired();

            //builder.HasMany(o => o.BookingItems)
            //    .WithOne()
            //    .HasForeignKey(oi => oi.BookingId);

            builder.ComplexProperty(
                o => o.RoomId, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(BookingModel.RoomId))
                        .IsRequired();
                });

            //builder.ComplexProperty(
            //   o => o.ShippingAddress, addressBuilder =>
            //   {
            //       addressBuilder.Property(a => a.FirstName)
            //           .HasMaxLength(50)
            //           .IsRequired();

            //       addressBuilder.Property(a => a.LastName)
            //            .HasMaxLength(50)
            //            .IsRequired();

            //       addressBuilder.Property(a => a.EmailAddress)
            //           .HasMaxLength(50);

            //       addressBuilder.Property(a => a.AddressLine)
            //           .HasMaxLength(180)
            //           .IsRequired();

            //       addressBuilder.Property(a => a.Country)
            //           .HasMaxLength(50);

            //       addressBuilder.Property(a => a.State)
            //           .HasMaxLength(50);

            //       addressBuilder.Property(a => a.ZipCode)
            //           .HasMaxLength(5)
            //           .IsRequired();
            //   });

            builder.ComplexProperty(
              o => o.BillingAddress, addressBuilder =>
              {
                  addressBuilder.Property(a => a.FirstName)
                       .HasMaxLength(50)
                       .IsRequired();

                  addressBuilder.Property(a => a.LastName)
                       .HasMaxLength(50)
                       .IsRequired();

                  addressBuilder.Property(a => a.EmailAddress)
                      .HasMaxLength(50);

                  addressBuilder.Property(a => a.AddressLine)
                      .HasMaxLength(180)
                      .IsRequired();

                  addressBuilder.Property(a => a.Country)
                      .HasMaxLength(50);

                  addressBuilder.Property(a => a.State)
                      .HasMaxLength(50);

                  addressBuilder.Property(a => a.ZipCode)
                      .HasMaxLength(5)
                      .IsRequired();
              });

            builder.ComplexProperty(
                   o => o.Payment, paymentBuilder =>
                   {
                       paymentBuilder.Property(p => p.CardName)
                           .HasMaxLength(50);

                       paymentBuilder.Property(p => p.CardNumber)
                           .HasMaxLength(24)
                           .IsRequired();

                       paymentBuilder.Property(p => p.Expiration)
                           .HasMaxLength(10);

                       paymentBuilder.Property(p => p.CVV)
                           .HasMaxLength(3);

                       paymentBuilder.Property(p => p.PaymentMethod);
                   });

            builder.Property(o => o.Status)
                .HasDefaultValue(BookingStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (BookingStatus)Enum.Parse(typeof(BookingStatus), dbStatus));

            builder.Property(o => o.TotalPrice);

            //builder.HasData(GetSeedBookings());
        }

        //private static List<BookingModel> GetSeedBookings()
        //{
        //    var address1 = Address.Of("omer", "yilmaz", "omer@gmail.com", "namık kemal", "Turkey", "Istanbul", "38050");
        //    var address2 = Address.Of("john", "doe", "john@gmail.com", "Broadway No:1", "England", "Nottingham", "08050");

        //    var payment1 = Payment.Of("omer", "5555555555554444", "04/25", "543", 1);
        //    var payment2 = Payment.Of("john", "8885555555554444", "12/47", "435", 2);

        //    var booking1 = BookingModel.Create(
        //                    BookingId.Of(Guid.Parse("2d2cbcb1-86c2-44d4-ac28-eae156361adf")),
        //                    CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
        //                    BookingName.Of("ORD_1"),
        //                    shippingAddress: address1,
        //                    billingAddress: address1,
        //                    payment1);

        //    var booking2 = BookingModel.Create(
        //                    BookingId.Of(Guid.Parse("59a8475f-da52-41e8-8e1d-3f59fa877c93")),
        //                    CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
        //                    BookingName.Of("ORD_2"),
        //                    shippingAddress: address2,
        //                    billingAddress: address2,
        //                    payment2);

        //    return new List<BookingModel> { booking1, booking2 };
        //}
    }
}
