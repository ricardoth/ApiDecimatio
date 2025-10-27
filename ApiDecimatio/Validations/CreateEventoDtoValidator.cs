using Decimatio.Application.DTOs;

namespace Decimatio.WebApi.Validations
{
    public class CreateEventoDtoValidator : AbstractValidator<CreateEventoDto>
    {
        public CreateEventoDtoValidator()
        {
            RuleFor(x => x.IdLugar)
                .GreaterThan(0).WithMessage("IdLugar debe ser un número positivo.");
            RuleFor(x => x.NombreEvento).NotEmpty().WithMessage("NombreEvento es obligatorio.");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("Descripcion es obligatorio.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("Fecha es obligatorio.");
            RuleFor(x => x.ProductoraResponsable).NotEmpty().WithMessage("ProductoraResponsable es obligatorio.");
            RuleFor(x => x.Banner)
                .NotNull()
                .When(x => x.Banner);
            RuleFor(x => x.ContenidoBanner)
                .MinimumLength(3).MaximumLength(400)
                .When(x => !string.IsNullOrEmpty(x.ContenidoBanner));
            RuleFor(x => x.Observacion).
                MinimumLength(3).MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Observacion));

        }
    }
}
