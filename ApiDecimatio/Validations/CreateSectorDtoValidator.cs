namespace Decimatio.WebApi.Validations
{
    public class CreateSectorDtoValidator : AbstractValidator<CreateSectorDto>
    {
        public CreateSectorDtoValidator()
        {
            RuleFor(x => x.IdEvento).GreaterThan(0).WithMessage("IdLugar debe ser un número positivo.");
            RuleFor(x => x.NombreSector).NotEmpty().WithMessage("NombreEvento es obligatorio.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
            RuleFor(x => x.Cargo).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
            RuleFor(x => x.Total).GreaterThan(0).WithMessage("El precio debe ser mayor a 0");
            RuleFor(x => x.CapacidadActual).NotEmpty().WithMessage("La Capacidad Actual es obligatoria");
            RuleFor(x => x.CapacidadDisponible).NotEmpty().WithMessage("La Capacidad Disponible es obligatoria");
            RuleFor(x => x.CapacidadTotal).NotEmpty().WithMessage("La Capacidad Total es obligatoria");
            RuleFor(x => x.ColorHexa).NotEmpty().WithMessage("El Color es obligatorio");
            RuleFor(x => x.Activo).NotNull().WithMessage("El Color es obligatorio");
        }
    }
}
