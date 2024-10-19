namespace BuildingBlocks.Messaging.Events;
public record ConfirmReservationEvent : IntegrationEvent
{
    public Guid ReservationId { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    //public Guid HotelId { get; set; } = default!;
    public Guid RoomId { get; set; } = default!;
    public DateTime CheckInDate { get; set; } = default!;
    public DateTime CheckOutDate { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
    public int NightQuantity { get; set; } = default!;
    public Payment Payment { get; set; } = default!;
    public Address Address { get; set; } = default!;
}

public class Address
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
}

public class Payment
{
    public string CardName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string Expiration { get; set; } = default!;
    public string CVV { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
}