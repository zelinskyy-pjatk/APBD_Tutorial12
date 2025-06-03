namespace Tutorial12.DTOs;

public record AssignClientRequest(
    string FirstName, 
    string LastName,
    string Email,
    string Telephone, 
    string Pesel,
    DateTime? PaymentDate
);