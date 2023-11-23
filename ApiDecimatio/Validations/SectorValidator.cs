namespace Decimatio.WebApi.Validations
{
    public class SectorValidator : AbstractValidator<Sector>
    {
        public SectorValidator()
        {
            RuleFor(x => x.IdEvento).GreaterThan(0).WithMessage("IdLugar debe ser un número positivo.");
            RuleFor(x => x.NombreSector).NotEmpty().WithMessage("NombreEvento es obligatorio.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
            RuleFor(x => x.Cargo).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
            RuleFor(x => x.Total).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
        }
    }
}
