using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.DTOs;


namespace Tutorial12.Repositories;

public class TripRepository : ITripRepository
{
    private readonly TravelDbContext _context;
    public TripRepository(TravelDbContext context) => _context = context;
    
    public async Task<(IReadOnlyList<TripDto> Trips, int Total)> GetTripsAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var baseQuery = _context.Trips
                .AsNoTracking()
                .Include(t => t.IdCountries)
                    .ThenInclude(ct => ct.IdCountry)
                .Include(t => t.ClientTrips)
                    .ThenInclude(ct => ct.IdClientNavigation);
        
        var total = await baseQuery.CountAsync(cancellationToken);
            
        var trips = await baseQuery
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDto(
                    t.Name,
                    t.Description,
                    t.DateFrom,
                    t.DateTo,
                    t.MaxPeople,
                    t.IdCountries
                        .Select(ct => new CountryDto(ct.Name))
                        .ToList(),
                    t.ClientTrips
                        .Select(ct => new ClientDto(
                            ct.IdClientNavigation.FirstName,
                            ct.IdClientNavigation.LastName))
                        .ToList()))
            .ToListAsync(cancellationToken);
        return (trips, total);
    }
}