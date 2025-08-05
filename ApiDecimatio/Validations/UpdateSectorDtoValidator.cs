namespace Decimatio.WebApi.Validations
{
    public class UpdateSectorDtoValidator : AbstractValidator<UpdateSectorDto>
    {
        public UpdateSectorDtoValidator()
        {
            RuleFor(x => x.IdEvento)
                .GreaterThan(0).WithMessage("IdEvento debe ser un número positivo.")
                .When(x => x.IdEvento.HasValue);

            RuleFor(x => x.NombreSector)
                .MinimumLength(3)
                .MaximumLength(200)
                .When(x => !string.IsNullOrEmpty(x.NombreSector));

            RuleFor(x => x.CapacidadDisponible)
                .GreaterThanOrEqualTo(0)
                .When(x => x.CapacidadDisponible.HasValue);

            RuleFor(x => x.CapacidadActual)
                .GreaterThanOrEqualTo(0)
                .When(x => x.CapacidadActual.HasValue);

            RuleFor(x => x.CapacidadTotal)
                .GreaterThanOrEqualTo(0)
                .When(x => x.CapacidadTotal.HasValue);


            RuleFor(x => x.Precio)
                .GreaterThan(0)
                .When(x => x.Precio.HasValue);

            RuleFor(x => x.Cargo)
                .GreaterThan(0)
                .When(x => x.Cargo.HasValue);

            RuleFor(x => x.Total)
               .GreaterThan(0)
               .When(x => x.Total.HasValue);

            RuleFor(x => x.ColorHexa)
               .MinimumLength(3)
               .MaximumLength(100)
               .When(x => !string.IsNullOrEmpty(x.ColorHexa));

            RuleFor(x => x.Activo)
                .NotNull()
                .When(x => x.Activo.HasValue);
        }
    }
}
