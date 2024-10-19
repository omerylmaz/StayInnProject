using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Data.Enums;


public enum RoomTypes
{
    [Display(Name = "Deluxe Suite")]
    DeluxeSuite,

    [Display(Name = "Standard Room")]
    StandardRoom,

    [Display(Name = "Family Suite")]
    FamilySuite,

    [Display(Name = "Single Room")]
    SingleRoom,

    [Display(Name = "Presidential Suite")]
    PresidentialSuite,

    [Display(Name = "Business Suite")]
    BusinessSuite,

    [Display(Name = "Penthouse Suite")]
    PenthouseSuite
}
