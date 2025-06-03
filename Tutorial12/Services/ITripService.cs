using Tutorial12.DTOs;

namespace Tutorial12.Services;

public interface ITripService
{
    Task<TripPageDto> GetTripsPageAsync(
        int page, int pageSize, CancellationToken cancellationToken = default);
    
    Task DeleteClientAsync(int clientId, CancellationToken cancellationToken = default);
    
    Task AssignClientAsync(int idTrip, AssignClientRequest assignClientRequest, CancellationToken cancellationToken = default);
}