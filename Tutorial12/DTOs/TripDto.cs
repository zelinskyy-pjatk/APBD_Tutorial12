namespace Tutorial12.DTOs;

public class TripDto {
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDto> Countries { get; set; }
    public List<ClientDto> Clients { get; set; }
    
    public TripDto() { }

    public TripDto(string name, string? description,
        DateTime dateFrom, DateTime dateTo, int maxPeople,
        List<CountryDto> countries, List<ClientDto> clients)
    {
        Name = name;
        Description = description;
        DateFrom = dateFrom;
        DateTo = dateTo;
        MaxPeople = maxPeople;
        Countries = countries;
        Clients = clients;
    }
}