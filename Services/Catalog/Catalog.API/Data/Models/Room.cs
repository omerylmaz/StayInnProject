using Catalog.API.Data.Enums;

namespace Catalog.API.Data.Models;

public class Room
{
    public Guid Id { get; set; }
    public RoomTypes RoomType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public decimal PricePerNight { get; set; }
    public Guid OwnerId { get; set; }
    public int RoomNumber { get; set; }
    public short Beds { get; set; }
    public int Capacity { get; set; }
    public ICollection<string> Amenities { get; set; }
    public ICollection<string> ImageFiles { get; set; }
    public ICollection<string> Rules { get; set; }
    public decimal Rating { get; set; }
    public bool IsAvailable { get; set; }
}