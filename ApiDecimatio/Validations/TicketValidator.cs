namespace Decimatio.WebApi.Validations
{
    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("El IdUsuario debe ser un número positivo.");
            RuleFor(x => x.IdEvento).GreaterThan(0).WithMessage("El IdEvento debe ser un número positivo.");
            RuleFor(x => x.IdSector).GreaterThan(0).WithMessage("El IdSector debe ser un número positivo.");
            RuleFor(x => x.IdMedioPago).GreaterThan(0).WithMessage("El IdMedioPago debe ser un número positivo.");

            RuleFor(x => x.MontoPago).GreaterThan(0).WithMessage("El MontoPago debe ser mayor que 0.");
            RuleFor(x => x.MontoTotal).GreaterThan(0).WithMessage("El MontoTotal debe ser mayor que 0.");

            RuleFor(x => x.FechaTicket).NotEmpty().WithMessage("La FechaTicket es obligatoria.");
        }
    }
}
