using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.DTOs;
using Tutorial12.Models;
using Tutorial12.Repositories;

namespace Tutorial12.Services;

public class TripService : ITripService
{   
    private readonly ITripRepository _tripRepository;
    private readonly TravelDbContext _context;
    
    public TripService(ITripRepository tripRepository, TravelDbContext context) => (_tripRepository, _context) = (tripRepository, context);
    
    public async Task<TripPageDto> GetTripsPageAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var (trips, total) = await _tripRepository.GetTripsAsync(page, pageSize, cancellationToken);
        var allPages = (int)Math.Ceiling(total / (double)pageSize);

        return new TripPageDto(page, pageSize, allPages, trips);
    }

    public async Task DeleteClientAsync(int clientId, CancellationToken cancellationToken = default)
    {
        var hasTrips = await _context.ClientTrips.AnyAsync(x => x.IdClient == clientId, cancellationToken);
        
        if (hasTrips) throw new InvalidOperationException("Client is assigned to at least one trip.");
        
        var client = await _context.Clients.FindAsync(new object?[] {clientId}, cancellationToken) ?? throw new KeyNotFoundException("Client was not found.");
        
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AssignClientAsync(int idTrip, AssignClientRequest assignClientRequest,
        CancellationToken cancellationToken = default)
    {
        var trip = await _context.Trips.Include(t => t.ClientTrips)
            .SingleOrDefaultAsync(t => t.IdTrip == idTrip, cancellationToken)
            ?? throw new KeyNotFoundException("Trip was not found.");

        if (trip.DateFrom <= DateTime.Now.Date)
            throw new InvalidOperationException("Cannot register for a trip that has already started.");
        
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Pesel == assignClientRequest.Pesel, cancellationToken);

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
            await _context.SaveChangesAsync(cancellationToken);
        } else
        {
            var isAlreadyOnTrip = await _context.ClientTrips.AnyAsync(ct => ct.IdTrip == idTrip &&
                                                                            ct.IdClient == client.IdClient);

            if (isAlreadyOnTrip)
                throw new InvalidOperationException("Client already registered for this trip.");
        }

        _context.ClientTrips.Add(new ClientTrip
        {
            IdTrip = idTrip,
            IdClient = client.IdClient,
            RegisteredAt = DateTime.Now,
            PaymentDate = assignClientRequest.PaymentDate
        });
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}
