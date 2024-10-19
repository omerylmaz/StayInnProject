//using AutoFixture;
//using AutoFixture.Kernel;
//using Booking.Application.Dtos;
//using Booking.Domain.Models;
//using Booking.Domain.ValueObjects;
//using BookingModel = Booking.Domain.Models.Booking;

//namespace Booking.Tests.Domain.Customizations;

//public class BookingSpecimenBuilder : ISpecimenBuilder
//{
//    //public object Create(object request, ISpecimenContext context)
//    //{
//    //    if (request is Type type && type == typeof(BookingModel))
//    //    {
//    //        var customerId = context.Create<Guid>();
//    //        var bookingName = context.Create<string>();
//    //        var shippingAddress = Address.Of("asdf","asdf","asdf","asdf","asdf","134", "234");//context.Create<Address>();
//    //        var billingAddress = Address.Of("asdf", "asdf", "asdf", "asdf", "asdf", "134", "234");//context.Create<Address>();

//    //        #region Payment
//    //        var cardName = context.Create<string>();
//    //        var cardNumber = context.Create<string>();
//    //        var expiration = context.Create<string>();

//    //        var paymentMethod = context.Create<int>();

//    //        var payment = Payment.Of(cardName, cardNumber, expiration, "105", paymentMethod);
//    //        #endregion

//    //        var booking = BookingModel.Create(
//    //            BookingId.Of(Guid.NewGuid()),
//    //            CustomerId.Of(customerId),
//    //            bookingName,
//    //            shippingAddress,
//    //            billingAddress,
//    //            payment);
//    //        for(int i = 0; i < 3; i++)
//    //        {
//    //            var roomId = context.Create<Guid>();
//    //            var quantity = context.Create<int>();
//    //            var price = context.Create<decimal>();
//    //            booking.AddBookingItem(RoomId.Of(roomId), quantity, price);
//    //        }
//    //        return booking;
//    //    }
//    //    return new NoSpecimen();
//    //}
//}
