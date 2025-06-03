using Tutorial12.DTOs;

namespace Tutorial12.Repositories;

public interface ITripRepository
{
    Task<(IReadOnlyList<TripDto> Trips, int Total)> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}