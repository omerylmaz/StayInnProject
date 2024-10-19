using AutoFixture;
using AutoFixture.Kernel;
using Booking.Application.Dtos;

namespace Booking.Tests.Domain.Customizations;

public class BookingDtoSpecimenBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        //if (request is Type type && type == typeof(BookingDto))
        //{
        //    var customerId = context.Create<Guid>();
        //    var bookingName = context.Create<string>();
        //    var shippingAddressDto = context.Create<AddressDto>();
        //    var billingAddressDto = context.Create<AddressDto>();
        //    var bookingItemsDto = context.CreateMany<BookingItemDto>().ToHashSet();

        //    #region PaymentDto
        //    var cardName = context.Create<string>();
        //    var cardNumber = context.Create<string>();
        //    var expiration = context.Create<string>();

        //    //var cvv = context.Create<int>() % 1000;
        //    //var cvvString = cvv.ToString("D3");

        //    var paymentMethod = context.Create<int>();

        //    var paymentDto = new PaymentDto(cardName, cardNumber, expiration, "105", paymentMethod);
        //    #endregion

        //    var bookingDto = new BookingDto(
        //        customerId,
        //        bookingName,
        //        shippingAddressDto,
        //        billingAddressDto,
        //        paymentDto,
        //        Booking.Domain.Enums.BookingStatus.Pending,
        //        bookingItemsDto);

        //    return bookingDto;
        //}

        // Eğer istenilen tür BookingDto değilse, talebin gerçekleştirilmediğini belirtmek için NoSpecimen döndürülür.
        return new NoSpecimen();
    }
}
