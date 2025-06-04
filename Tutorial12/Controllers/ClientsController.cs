using Microsoft.AspNetCore.Mvc;
using Tutorial12.Services;

namespace Tutorial12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ITripService _tripService;
    public ClientsController(ITripService tripService) => _tripService = tripService;

    [HttpDelete("{idClient::int}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            await _tripService.DeleteClientAsync(idClient);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return Conflict(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}