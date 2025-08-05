namespace Decimatio.WebApi.Validations
{
    public class UpdateEventoDtoValidator : AbstractValidator<UpdateEventoDto>
    {
        public UpdateEventoDtoValidator()
        {
            RuleFor(x => x.IdEvento)
                .GreaterThan(0).WithMessage("IdEvento debe ser un número positivo.")
                .When(x => x.IdEvento.HasValue);
            RuleFor(x => x.NombreEvento)
                .MinimumLength(3)
                .MaximumLength(250)
                .When(x => !string.IsNullOrEmpty(x.NombreEvento));
            RuleFor(x => x.Descripcion)
                .MinimumLength(3).MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
            RuleFor(x => x.Fecha)
                .NotNull()
                .When(x => x.Fecha.HasValue);
            RuleFor(x => x.Flyer)
                .MinimumLength(3).MaximumLength(400)
                .When(x => !string.IsNullOrEmpty(x.Flyer));
            RuleFor(x => x.Observacion)
                .MinimumLength(3)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Observacion));
            RuleFor(x => x.Banner)
                .NotNull()
                .When(x => x.Banner.HasValue);
            RuleFor(x => x.ContenidoBanner)
                .MinimumLength(3)
                .MaximumLength(400)
               .When(x => !string.IsNullOrEmpty(x.ContenidoBanner));
            RuleFor(x => x.Activo)
                .NotNull()
                .When(x => x.Activo.HasValue);
        }
    }
}
