namespace Reservation.API.Data.Models;

public class Cancellation
{
    public Guid CancellationId { get; set; }
    public Guid BookingId { get; set; }
    public DateTime CancellationDate { get; set; }
    public decimal RefundAmount { get; set; }
}