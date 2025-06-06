using async.dto;
using FluentValidation;

namespace async.Validators
{
    public class BeerUpdateValidator : AbstractValidator<BeerUpdateDto>
    {
        public BeerUpdateValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("El Id no puede ser nulo");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nombre: obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("Longitud permitida: 2 a 20 caracteres");
            RuleFor(x => x.BrandId).NotNull().WithMessage(x => "El id de la marca no piede ser Null");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a cero");
        }
    }
}
