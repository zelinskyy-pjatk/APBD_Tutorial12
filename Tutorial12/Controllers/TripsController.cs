using Microsoft.AspNetCore.Mvc;
using Tutorial12.DTOs;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    public TripsController(ITripService tripService) => _tripService = tripService;

    [HttpGet]
    public async Task<ActionResult<TripDto>> GetTripsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        if (page < 1) return BadRequest("Page must be  >= 1");
        if (pageSize < 1) return BadRequest("Page size must be >= 1");
        
        var result = await _tripService.GetTripsPageAsync(page, pageSize, cancellationToken);
        return Ok(result);
    }

    [HttpPost("{idTrip:int}/clients")]
    public async Task<IActionResult> AssignClient(int idTrip,
        [FromBody] AssignClientRequest assignClientRequest,
        CancellationToken cancellationToken)
    {
        await _tripService.AssignClientAsync(idTrip, assignClientRequest, DateTime.UtcNow, cancellationToken);
        return Created();
    }
    
}