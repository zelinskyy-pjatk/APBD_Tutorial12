namespace Tutorial12.DTOs;

public class TripPageDto
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public IReadOnlyCollection<TripDto> Trips { get; set; }
    
    public TripPageDto() { }

    public TripPageDto(int pageNum, int pageSize, int allPages, List<TripDto> trips)
    {
        PageNum = pageNum;
        PageSize = pageSize;
        AllPages = allPages;
        Trips = trips;
    }
    
}