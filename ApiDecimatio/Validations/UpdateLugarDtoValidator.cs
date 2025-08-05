namespace Decimatio.WebApi.Validations
{
    public class UpdateLugarDtoValidator : AbstractValidator<UpdateLugarDto>
    {
        public UpdateLugarDtoValidator()
        {
            RuleFor(x => x.IdComuna)
                .GreaterThan(0).WithMessage("IdComuna debe ser un número positivo.")
                .When(x => x.IdComuna.HasValue);
            RuleFor(x => x.NombreLugar)
                .MinimumLength(3)
                .MaximumLength(300)
                .When(x => !string.IsNullOrEmpty(x.NombreLugar));
            RuleFor(x => x.Ubicacion).
                MinimumLength(3).MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Ubicacion));
            RuleFor(x => x.Numeracion).
                MinimumLength(2).MaximumLength(20)
                .When(x => !string.IsNullOrEmpty(x.Numeracion));
            RuleFor(x => x.MapaReferencial).
                MinimumLength(3).MaximumLength(200)
                .When(x => !string.IsNullOrEmpty(x.MapaReferencial));
            RuleFor(x => x.Base64ImagenMapaReferencial).
                MinimumLength(3)
                .When(x => !string.IsNullOrEmpty(x.Base64ImagenMapaReferencial));
            RuleFor(x => x.Activo)
                .NotNull()
                .When(x => x.Activo.HasValue);

        }
    }
}
