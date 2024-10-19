using Booking.Domain.Abstractions;
using Booking.Domain.DomainEvents;
using Booking.Domain.Enums;
using Booking.Domain.ValueObjects;

namespace Booking.Domain.Models;

public class Booking : Aggregate<BookingId>
{
    private Booking()
    {
        
    }

    public CustomerId CustomerId { get; private set; } = default!;
    public string BookingName { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public int NightQuantity { get; private set; } = default!;
    public RoomId RoomId { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public decimal TotalPrice { get; private set; } = default!;
    public DateTime BookingDate { get; set; } = default!;
    public BookingStatus Status { get; private set; } = BookingStatus.Pending;
    //public decimal TotalPrice
    //{
    //    get => Price * NightQuantity;
    //    private set { }
    //}

    public static Booking Create(BookingId id, CustomerId customerId, string bookingName, Address billingAddress, Payment payment, RoomId roomId, int nightQuantity, DateTime bookingDate)
    {
        var booking = new Booking
        {
            Id = id,
            CustomerId = customerId,
            BookingName = bookingName,
            BookingDate = bookingDate,
            BillingAddress = billingAddress,
            RoomId = roomId,
            NightQuantity = nightQuantity,
            Payment = payment,
            Status = BookingStatus.Pending
        };

        booking.AddDomainEvent(new BookingCreatedEvent(booking));

        return booking;
    }
    //public void AddBookingItem(RoomId roomId, int quantity, decimal price)
    //{
    //    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
    //    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

    //    var bookingItem = new BookingItem(Id, roomId, quantity, price);
    //    _bookingItems.Add(bookingItem);
    //}

    public void Update(string bookingName, Address billingAddress, Payment payment, RoomId roomId, int nightQuantity, BookingStatus status)
    {
        BookingName = bookingName;
        BillingAddress = billingAddress;
        Payment = payment;
        RoomId = roomId;
        NightQuantity = nightQuantity;
        Status = status;

        AddDomainEvent(new BookingUpdatedEvent(this));
    }


    //public void RemoveBookingItem(RoomId roomId)
    //{
    //    var bookingItem = _bookingItems.FirstOrDefault(x => x.RoomId == roomId);
    //    if (bookingItem is not null)
    //    {
    //        _bookingItems.Remove(bookingItem);
    //    }
    //}
}