using AnimalShelterApi.Models;
using FluentValidation;

namespace AnimalShelterApi.Validators;

public class AnimalValidator : AbstractValidator<Animal>
{
    public AnimalValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 255).WithMessage("Name should be between 3 and 255 characters");
        RuleFor(a => a.Breed)
            .Length(3, 255).When(a => a.Breed != null)
            .WithMessage("Breed, if provided, must be between 3 and 255 characters");
        RuleFor(a => a.Age)
            .InclusiveBetween(0, 30).When(a => a.Age.HasValue)
            .WithMessage("Age, if provided, must be between 0 and 30 years");
        RuleFor(a => a.Gender)
            .IsInEnum().WithMessage("Gender must be either 'Male' or 'Female'");
        RuleFor(a => a.ShelterId)
            .NotEmpty().WithMessage("Shelter ID is required");
    }
}