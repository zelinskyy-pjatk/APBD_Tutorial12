namespace Tutorial12.DTOs;

public class ClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ClientDto() { }

    public ClientDto(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
    
