using AnimalShelterApi.Models;
using FluentValidation;

namespace AnimalShelterApi.Validators;

public class ShelterValidator : AbstractValidator<Shelter>
{
    public ShelterValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3, 255).WithMessage("Name should be between 3 and 255 characters");
    }
}