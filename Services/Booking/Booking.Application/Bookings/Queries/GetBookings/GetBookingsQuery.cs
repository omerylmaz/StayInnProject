using Booking.Application.Dtos;
using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;

namespace Booking.Application.Bookings.Queries.GetBookings;

public record GetBookingsQuery(PaginationRequest PaginationRequest) : IQuery<GetBookingsResult>;

public record GetBookingsResult(PaginatedResult<BookingDto> PaginatedResult);