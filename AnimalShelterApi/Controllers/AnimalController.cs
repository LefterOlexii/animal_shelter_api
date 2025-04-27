using AnimalShelterApi.Models;
using AnimalShelterApi.Services;
using Microsoft.AspNetCore.Mvc;
using AnimalShelterApi.Exceptions;
using AnimalShelterApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelterApi.Controllers;

public record AnimalAddRequest(string Name, int? Age, string Breed, Gender Gender, Guid ShelterId);

public record AnimalResponse(Guid Id, string Name, int? Age, string? Breed, Gender Gender, Guid ShelterId);

[Route("api/[controller]/[action]")]
[ApiController]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpPost]
    public ActionResult<Guid> AddAnimal(AnimalAddRequest request)
    {
        try
        {
            var animalId = _animalService.AddAnimal(request);
            return CreatedAtAction(nameof(GetAnimalById), new { id = animalId }, animalId);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { errors = ex.Errors });
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
    public ActionResult<AnimalResponse> GetAnimalById(Guid id)
    {
        try
        {
            var animal = _animalService.GetAnimalById(id);
            return Ok(animal);
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
    public ActionResult<IEnumerable<AnimalResponse>> GetAllAnimals()
    {
        try
        {
            var animals = _animalService.GetAllAnimals();
            return Ok(animals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<AnimalResponse>> GetAnimalsWithoutApplications(Guid? shelterId)
    {
        try
        {
            var animals = _animalService.GetAnimalsWithoutApplications(shelterId);
            return Ok(animals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}