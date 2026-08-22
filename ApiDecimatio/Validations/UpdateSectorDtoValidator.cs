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
