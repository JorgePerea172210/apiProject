using async.dto;
using FluentValidation;

namespace async.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nombre: obligatorio");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("Longitud permitida: 2 a 20 caracteres");
            RuleFor(x => x.BrandId).NotNull().WithMessage(x => "El id de la marca no piede ser Null");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a cero");
        }
    }
}
