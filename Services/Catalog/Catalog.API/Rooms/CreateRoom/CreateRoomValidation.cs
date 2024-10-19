using FluentValidation;

namespace Catalog.API.Rooms.CreateRoom
{
    public class CreateRoomValidation : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomValidation()
        {
            //RuleFor(x => x.HotelId)
            //    .NotEmpty()
            //    .NotNull()
            //    .WithMessage("HotelId cannot be empty");

            RuleFor(x => x.RoomType)
                .IsInEnum()
                .WithMessage("Invalid Room Type");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than 0");

            RuleFor(x => x.PricePerNight)
                .GreaterThan(0)
                .WithMessage("PricePerNight must be greater than 0");
        }
    }
}
