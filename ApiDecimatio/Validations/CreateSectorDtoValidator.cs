namespace Decimatio.WebApi.Validations
{
    public class CreateSectorDtoValidator : AbstractValidator<CreateSectorDto>
    {
        public CreateSectorDtoValidator()
        {
            RuleFor(x => x.IdEvento).GreaterThan(0).WithMessage("IdLugar debe ser un número positivo.");
            RuleFor(x => x.NombreSector).NotEmpty().WithMessage("NombreEvento es obligatorio.");
            RuleFor(x => x.ColorHexa).NotEmpty().WithMessage("El Color es obligatorio");
            RuleFor(x => x.Activo).NotNull().WithMessage("El Color es obligatorio");
        }
    }
}
