namespace Decimatio.WebApi.Validations
{
    public class EventoValidator : AbstractValidator<Evento>
    {
        public EventoValidator()
        {
            RuleFor(x => x.IdLugar).GreaterThan(0).WithMessage("IdLugar debe ser un número positivo.");
            RuleFor(x => x.NombreEvento).NotEmpty().WithMessage("NombreEvento es obligatorio.");
            RuleFor(x => x.Direccion).NotEmpty().WithMessage("Direccion es obligatorio.");
            RuleFor(x => x.Fecha).NotEmpty().WithMessage("Fecha es obligatorio.");
        }
    }
}
