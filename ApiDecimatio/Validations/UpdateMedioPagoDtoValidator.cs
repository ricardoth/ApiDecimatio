namespace Decimatio.WebApi.Validations
{
    public class UpdateMedioPagoDtoValidator : AbstractValidator<UpdateMedioPagoDto>
    {
        public UpdateMedioPagoDtoValidator()
        {
            RuleFor(x => x.NombreMedioPago)
                .MinimumLength(3)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.NombreMedioPago));

            RuleFor(x => x.Descripcion)
                .MinimumLength(3)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.Descripcion));

            RuleFor(x => x.UrlImageBlob)
                .MinimumLength(3)
                .MaximumLength(100)
                .When(x => !string.IsNullOrEmpty(x.UrlImageBlob));

            RuleFor(x => x.Activo)
                .NotNull()
                .When(x => x.Activo.HasValue);
        }
    }
}
