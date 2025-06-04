using Tutorial12.DTOs;

namespace Tutorial12.Services;

public interface ITripService
{
    Task<TripPageDto> GetTripsPageAsync(int page, int pageSize);
    
    Task DeleteClientAsync(int clientId);
    
    Task AssignClientAsync(int idTrip, AssignClientRequest assignClientRequest, DateTime now);
}