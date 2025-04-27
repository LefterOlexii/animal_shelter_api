using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelterApi.Controllers;

public record ShelterAddRequest(string Name);

public record ShelterResponse(Guid Id, string Name);

[Route("api/[controller]/[action]")]
[ApiController]
public class ShelterController : ControllerBase
{
    private readonly IShelterService _shelterService;

    public ShelterController(IShelterService shelterService)
    {
        _shelterService = shelterService;
    }


    [HttpPost]
    public ActionResult<Guid> AddShelter(ShelterAddRequest request)
    {
        try
        {
            var shelterId = _shelterService.AddShelter(request);
            return CreatedAtAction(nameof(GetShelterById), new { id = shelterId }, shelterId);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<ShelterResponse> GetShelterById(Guid id)
    {
        try
        {
            var shelter = _shelterService.GetShelterById(id);
            return Ok(shelter);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<ShelterResponse>> GetAllShelters()
    {
        try
        {
            var shelters = _shelterService.GetAllShelters();
            return Ok(shelters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}