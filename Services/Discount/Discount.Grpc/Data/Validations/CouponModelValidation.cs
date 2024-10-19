using FluentValidation;
using System.Security.Cryptography.Xml;

namespace Discount.Grpc.Data.Validations;

public class CouponModelValidation : AbstractValidator<CreateDiscountRequest>
{
    public CouponModelValidation()
    {
        RuleFor(x => DateTime.Parse(x.Coupon.ValidFrom))
            .LessThan(x => DateTime.Parse(x.Coupon.ValidTo))
            .WithMessage("Valid From should not greater than or equal to Valid To");

        RuleFor(x => x.Coupon.Amount)
            .GreaterThan(0)
            .WithMessage("Amount should be greater 0");
    }
}
