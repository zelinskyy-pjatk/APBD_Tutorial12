namespace Tutorial12.DTOs;

public record TripPageDto
(
    int PageNum,
    int PageSize,
    int AllPages,
    IReadOnlyCollection<TripDto> Trips
);