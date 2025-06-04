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
    public async Task<IActionResult> GetTripsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        if (page < 1) return BadRequest("Page must be  >= 1");
        if (pageSize < 1) return BadRequest("Page size must be >= 1");
        
        var result = await _tripService.GetTripsPageAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClient(int idTrip,
        [FromBody] AssignClientRequest assignClientRequest)
    {
        try
        {
            await _tripService.AssignClientAsync(idTrip, assignClientRequest, DateTime.UtcNow);
            return Created();
        } catch (InvalidOperationException ex)
        { return BadRequest(ex.Message); } 
        catch (ArgumentException ex)
        { return NotFound(ex.Message); } 
        catch (Exception ex)
        {  return BadRequest(ex.Message); }
    }
}