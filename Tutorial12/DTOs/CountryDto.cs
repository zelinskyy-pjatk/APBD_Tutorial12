namespace Tutorial12.DTOs;

public class CountryDto
{
    public string Name { get; set; }

    public CountryDto() { }

    public CountryDto(string name)
    {
        Name = name;
    }
}