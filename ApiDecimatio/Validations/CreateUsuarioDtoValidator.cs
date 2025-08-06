namespace Decimatio.WebApi.Validations
{
    public class CreateUsuarioDtoValidator : AbstractValidator<CreateUsuarioDto>
    {
        public CreateUsuarioDtoValidator()
        {
            
            RuleFor(e => e.Nombres).NotNull().WithMessage("El nombre no puede ser núlo");
            RuleFor(e => e.Nombres).NotEmpty().WithMessage("El nombre no puede estar vacío");
            RuleFor(e => e.Nombres).Length(3, 200).WithMessage("El nombre debe tener un mínimo de 3 y máximo 200 caractéres");

            RuleFor(e => e.Correo).NotNull().WithMessage("El correo no puede ser núlo");
            RuleFor(e => e.Correo).NotEmpty().WithMessage("El correo no puede estar vacío");
            RuleFor(e => e.Correo).Length(3, 200).WithMessage("El correo paterno debe tener un mínimo de 3 y máximo 200 caractéres");
        }
    }
}
