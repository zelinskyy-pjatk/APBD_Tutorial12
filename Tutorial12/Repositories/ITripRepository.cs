using Tutorial12.DTOs;

namespace Tutorial12.Repositories;

public interface ITripRepository
{
    Task<(List<TripDto> Trips, int Total)> GetTripsAsync(int page, int pageSize);
}