using Booking.Application.Dtos;
using Booking.Domain.Models;
using Booking.Domain.ValueObjects;
using System.Linq;

namespace Booking.Application.Extensions;

internal static class CustomMapperExtension
{
    // Burayı daha sonra mapper aracı ile düzeltmeye çalışacam
    internal static BookingModel ConvertToBooking(this BookingDto bookingDto, Guid id = default)
    {
        var billingAddress = Address.Of(bookingDto.BillingAddress.FirstName, bookingDto.BillingAddress.LastName, bookingDto.BillingAddress.EmailAddress, bookingDto.BillingAddress.AddressLine, bookingDto.BillingAddress.Country, bookingDto.BillingAddress.State, bookingDto.BillingAddress.ZipCode);

        var booking = BookingModel.Create(
            id: id == default ? BookingId.Of(Guid.NewGuid()) : BookingId.Of(id),
            customerId: CustomerId.Of(bookingDto.CustomerId),
            bookingName: bookingDto.BookingName,
            billingAddress: billingAddress,
            payment: Payment.Of(bookingDto.Payment.Amount, bookingDto.Payment.Method),
            roomId: RoomId.Of(bookingDto.RoomId),
            nightQuantity: bookingDto.NightQuantity,
            bookingDate: bookingDto.BookingDate
            );

        return booking;
    }

    internal static BookingDto ConvertToBookingDto(this BookingModel booking)
    {
        Guid customerId = booking.CustomerId.Value;
        string bookingName = booking.BookingName;
        AddressDto billingAddress = new AddressDto(
            booking.BillingAddress.FirstName, 
            booking.BillingAddress.LastName, 
            booking.BillingAddress.EmailAddress, 
            booking.BillingAddress.AddressLine, 
            booking.BillingAddress.Country, 
            booking.BillingAddress.State, 
            booking.BillingAddress.ZipCode);
        PaymentDto paymentDto = new PaymentDto(
            booking.Payment.Amount, 
            booking.Payment.PaymentDate, 
            booking.Payment.Status, 
            booking.Payment.Method);
        return new BookingDto(customerId, bookingName, billingAddress, paymentDto, booking.Status, booking.RoomId.Value, booking.NightQuantity, booking.TotalPrice, booking.BookingDate);
    }
}
