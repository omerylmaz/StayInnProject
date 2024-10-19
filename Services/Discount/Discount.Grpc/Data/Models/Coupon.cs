namespace Discount.Grpc.Data.Models;

public class Coupon
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; } = default!;
    public int Amount { get; set; }
    public CouponType Type { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public bool IsActive { get; set; }
}

