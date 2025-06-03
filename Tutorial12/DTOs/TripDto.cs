namespace Tutorial12.DTOs;

public record TripDto(
    string Name,
    string? Description,
    DateTime DateFrom,
    DateTime DateTo,
    int MaxPeople,
    IReadOnlyCollection<CountryDto> Countries,
    IReadOnlyCollection<ClientDto> Clients);