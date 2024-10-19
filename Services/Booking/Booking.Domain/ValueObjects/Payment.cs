namespace Booking.Domain.ValueObjects;
public record Payment
{
    public decimal Amount { get; init; } = default!;
    public DateTime PaymentDate { get; init; } = DateTime.UtcNow;
    public PaymentStatus Status { get; init; } = PaymentStatus.Pending;
    public PaymentMethod Method { get; init; } = default!;

    protected Payment() { }

    private Payment(decimal amount, DateTime paymentDate, PaymentStatus status, PaymentMethod paymentMethod)
    {
        Amount = amount;
        Method = paymentMethod;
        PaymentDate = DateTime.UtcNow;
        Status = status;
    }

    public static Payment Of(decimal amount, DateTime paymentDate, PaymentStatus status, PaymentMethod paymentMethod)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        return new Payment(amount, paymentDate, status, paymentMethod);
    }
}
public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Cancelled
}
public enum PaymentMethod
{
    CreditCard,
    PayPal,
    BankTransfer,
    Cash
}
