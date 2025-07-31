namespace Decimatio.WebApi.Validations
{
    public class CreateLugarDtoValidator : AbstractValidator<CreateLugarDto>
    {
        public CreateLugarDtoValidator()
        {
            RuleFor(x => x.IdComuna)
                .GreaterThan(0).WithMessage("IdComuna debe ser un número positivo.");
            RuleFor(x => x.NombreLugar).NotEmpty().WithMessage("NombreLugar es obligatorio.");
            RuleFor(x => x.Ubicacion).NotEmpty().WithMessage("Ubicacion es obligatorio.");
            RuleFor(x => x.Numeracion).NotEmpty().WithMessage("Numeracion es obligatorio.");
        }
    }
}
