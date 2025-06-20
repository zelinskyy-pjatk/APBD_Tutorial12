﻿using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.DTOs;


namespace Tutorial12.Repositories;

public class TripRepository : ITripRepository
{
    private readonly ApbdContext _context;
    public TripRepository(ApbdContext context) => _context = context;
    
    public async Task<(List<TripDto> Trips, int Total)> GetTripsAsync(int page, int pageSize)
    {
        var baseQuery = _context.Trips
                .AsNoTracking()
                .Include(t => t.IdCountries)
                .Include(t => t.ClientTrips)
                    .ThenInclude(ct => ct.IdClientNavigation);
        
        var total = await baseQuery.CountAsync();
            
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
                        .Select(c => new CountryDto
                        {
                            Name = c.Name
                        })
                        .ToList(), 
                    t.ClientTrips
                            .Select(ct => new ClientDto
                            {
                                FirstName = ct.IdClientNavigation.FirstName,
                                LastName = ct.IdClientNavigation.LastName
                            })
                        .ToList()))
            .ToListAsync();
        return (trips, total);
    }
}