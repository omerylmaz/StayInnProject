﻿namespace Catalog.API.Data.Models;

public class Service
{
    public Guid Id { get; set; }
    //public Guid HotelId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    //public Hotel Hotel { get; set; }
}