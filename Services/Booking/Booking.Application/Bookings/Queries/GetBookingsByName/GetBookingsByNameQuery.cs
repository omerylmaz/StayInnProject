using Booking.Application.Dtos;
using BuildingBlocks.CQRS;
using System.Collections.Generic;

namespace Booking.Application.Bookings.Queries.GetBookingsByRoom;

public record GetBookingsByNameQuery(string BookingName) : IQuery<GetBookingsByRoomResult>;

public record GetBookingsByRoomResult(IEnumerable<BookingDto> Booking);