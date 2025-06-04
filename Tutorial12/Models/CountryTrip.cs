using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.EntityFrameworkCore;

namespace Tutorial12.Models;

[PrimaryKey(nameof(IdCountry), nameof(IdTrip))]
[Table("Country_Trip")]
public class CountryTrip
{
    [ForeignKey(nameof(Country))]
    public int IdCountry { get; set; }
    [ForeignKey(nameof(Trip))]
    public int IdTrip { get; set; }
    public Country Country { get; set; }
    public Trip Trip { get; set; }
}