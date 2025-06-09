using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.DTOs;
using Tutorial12.Models;
using Tutorial12.Repositories;

namespace Tutorial12.Services;

public class TripService : ITripService
{   
    private readonly ITripRepository _tripRepository;
    private readonly ApbdContext _context;
    
    public TripService(ITripRepository tripRepository, ApbdContext context) => (_tripRepository, _context) = (tripRepository, context);
    
    public async Task<TripPageDto> GetTripsPageAsync(int page, int pageSize)
    {
        var (trips, total) = await _tripRepository.GetTripsAsync(page, pageSize);
        var allPages = (int)Math.Ceiling(total / (double)pageSize);

        return new TripPageDto(page, pageSize, allPages, trips);
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var hasTrips = await _context.ClientTrips.AnyAsync(x => x.IdClient == clientId);
        
        if (hasTrips) throw new InvalidOperationException("Client is assigned to at least one trip.");
        
        var client = await _context.Clients.FindAsync(new object?[] {clientId}) ?? throw new KeyNotFoundException("Client was not found.");
        
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task AssignClientAsync(int idTrip, AssignClientRequest assignClientRequest,
        DateTime now)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var trip = await _context.Trips.Include(t => t.ClientTrips)
                           .SingleOrDefaultAsync(t => t.IdTrip == idTrip)
                       ?? throw new KeyNotFoundException("Trip was not found.");

            if (trip.DateFrom <= now)
                throw new InvalidOperationException("Cannot register for a trip that has already started.");

            if (trip.ClientTrips.Count >= trip.MaxPeople)
                throw new InvalidOperationException("Trip is already full.");

            var client = await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == assignClientRequest.Pesel);

            if (client is null)
            {
                client = new Client
                {
                    FirstName = assignClientRequest.FirstName,
                    LastName = assignClientRequest.LastName,
                    Email = assignClientRequest.Email,
                    Telephone = assignClientRequest.Telephone,
                    Pesel = assignClientRequest.Pesel
                };
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
            }
            else
            {
                var isAlreadyOnTrip = _context.ClientTrips.Any(ct => ct.IdClient == client.IdClient);

                if (isAlreadyOnTrip) throw new InvalidOperationException("Client already registered for this trip.");
            }

            var currentClientCount = await _context.ClientTrips.CountAsync(ct => ct.IdTrip == idTrip);
            if (currentClientCount >= trip.MaxPeople)
                throw new InvalidOperationException("Trip is already full.");

            _context.ClientTrips.Add(new ClientTrip
            {
                IdTrip = idTrip,
                IdClient = client.IdClient,
                RegisteredAt = now,
                PaymentDate = assignClientRequest.PaymentDate
            });

            await _context.SaveChangesAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
