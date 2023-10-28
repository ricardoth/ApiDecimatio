namespace Decimatio.WebApi.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(e => e.Rut).NotNull().WithMessage("El Rut no puede ser núlo");
            RuleFor(e => e.Rut).NotEmpty().WithMessage("El Rut no puede estar vacío");
            RuleFor(e => e.Rut).NotEqual(0).WithMessage("El Rut no puede ser 0");

            RuleFor(e => e.DV).NotNull().WithMessage("El Dv no puede ser núlo");
            RuleFor(e => e.DV).NotEmpty().WithMessage("El Dv no puede ser vacío");
            RuleFor(e => e.DV).Matches("^[0-9Kk]$").WithMessage("El Dv debe ser un número del 0 al 9 o la letra K");

            RuleFor(e => e.Nombres).NotNull().WithMessage("El nombre no puede ser núlo");
            RuleFor(e => e.Nombres).NotEmpty().WithMessage("El nombre no puede estar vacío");
            RuleFor(e => e.Nombres).Length(3, 200).WithMessage("El nombre debe tener un mínimo de 3 y máximo 200 caractéres");

            RuleFor(e => e.Direccion).NotNull().WithMessage("La direccion no puede ser núlo");
            RuleFor(e => e.Direccion).NotEmpty().WithMessage("La direccion no puede estar vacío");
            RuleFor(e => e.Direccion).Length(3, 200).WithMessage("La direccion paterno debe tener un mínimo de 3 y máximo 200 caractéres");

            RuleFor(e => e.Telefono).NotNull().WithMessage("El teléfono no puede ser núlo");
            RuleFor(e => e.Telefono).NotEmpty().WithMessage("El teléfono no puede estar vacío");
            RuleFor(e => e.Telefono).Length(3, 20).WithMessage("El teléfono debe tener un mínimo de 3 y máximo 20 caractéres");

            RuleFor(e => e.Correo).NotNull().WithMessage("El correo no puede ser núlo");
            RuleFor(e => e.Correo).NotEmpty().WithMessage("El correo no puede estar vacío");
            RuleFor(e => e.Correo).Length(3, 200).WithMessage("El correo paterno debe tener un mínimo de 3 y máximo 200 caractéres");
        }
    }
}
