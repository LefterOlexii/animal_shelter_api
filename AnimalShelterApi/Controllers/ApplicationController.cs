using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelterApi.Controllers;

public record ApplicationAddRequest(Guid AnimalId);

public record ApplicationResponse(Guid Id, Guid AnimalId);

[Route("api/[controller]/[action]")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    public ActionResult<Guid> AddApplication(ApplicationAddRequest request)
    {
        try
        {
            var applicationId = _applicationService.AddApplication(request);
            return CreatedAtAction(nameof(GetApplicationById), new { id = applicationId }, applicationId);
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
    public ActionResult<ApplicationResponse> GetApplicationById(Guid id)
    {
        try
        {
            var application = _applicationService.GetApplicationById(id);
            return Ok(application);
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
    public ActionResult<IEnumerable<ApplicationResponse>> GetAllApplications()
    {
        try
        {
            var shelters = _applicationService.GetAllApplications();
            return Ok(shelters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}