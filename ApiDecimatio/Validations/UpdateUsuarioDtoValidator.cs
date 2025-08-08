namespace Decimatio.WebApi.Validations
{
    public class UpdateUsuarioDtoValidator : AbstractValidator<UpdateUsuarioDto>
    {
        public UpdateUsuarioDtoValidator()
        {
            RuleFor(x => x.IdTipoUsuario)
                .GreaterThan(0)
                .WithMessage("El valor debe ser 1 (Administrador) o 2 (Cliente)")
                .When(x => x.IdTipoUsuario.HasValue);

            RuleFor(x => x.Rut)
                .GreaterThan(0)
                .WithMessage("El Rut debe ser válido.")
                .When(x => x.Rut.HasValue);

            RuleFor(x => x.DV)
                 .MaximumLength(2)
                 .When(x => !string.IsNullOrEmpty(x.DV));

            RuleFor(x => x.Nombres)
                .MinimumLength(3)
                .WithMessage("El Nombre debe tener un mínimo de 3 carácteres")
                .MaximumLength(250)
                .WithMessage("El Nombre debe tener un máximo de 250 carácteres")
                .When(x => !string.IsNullOrEmpty(x.Nombres));

            RuleFor(x => x.ApellidoP)
               .MinimumLength(3)
               .WithMessage("El Apellido Paterno debe tener un mínimo de 3 carácteres")
               .MaximumLength(100)
               .WithMessage("El Apellido Paterno debe tener un máximo de 100 caracteres")
               .When(x => !string.IsNullOrEmpty(x.ApellidoP));

            RuleFor(x => x.ApellidoM)
               .MinimumLength(3)
               .WithMessage("El Apellido Materno debe tener un mínimo de 3 carácteres")
               .MaximumLength(100)
               .WithMessage("El Apellido Materno debe tener un máximo de 100 caracteres")
               .When(x => !string.IsNullOrEmpty(x.ApellidoM));

            RuleFor(x => x.Direccion)
               .MinimumLength(3)
               .WithMessage("La Dirección debe tener un mínimo de 3 carácteres")
               .MaximumLength(250)
               .WithMessage("La Dirección debe tener un máximo de 100 caracteres")
               .When(x => !string.IsNullOrEmpty(x.Direccion));

            RuleFor(x => x.Telefono)
               .MinimumLength(3)
               .WithMessage("El Teléfono debe tener un mínimo de 3 carácteres")
               .MaximumLength(20)
               .WithMessage("El Teléfono debe tener un máximo de 20 caracteres")
               .When(x => !string.IsNullOrEmpty(x.Telefono));

            RuleFor(x => x.Correo)
              .MinimumLength(3)
              .WithMessage("El Correo debe tener un mínimo de 3 carácteres")
              .MaximumLength(200)
              .WithMessage("El Correo debe tener un máximo de 200 caracteres")
              .When(x => !string.IsNullOrEmpty(x.Correo));


            RuleFor(x => x.Contrasena)
             .MinimumLength(3)
             .WithMessage("La Contraseña debe tener un mínimo de 3 carácteres")
             .MaximumLength(500)
             .WithMessage("La Contraseña debe tener un máximo de 500 caracteres")
             .When(x => !string.IsNullOrEmpty(x.Correo));

            RuleFor(x => x.Activo)
                .NotNull()
                .When(x => x.Activo.HasValue);

            RuleFor(x => x.EsExtranjero)
               .NotNull()
               .When(x => x.EsExtranjero.HasValue);
        }
    }
}
