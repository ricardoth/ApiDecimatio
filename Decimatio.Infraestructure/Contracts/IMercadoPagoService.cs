﻿namespace Decimatio.Infraestructure.Contracts
{
    public interface IMercadoPagoService
    {
        Task<Preference> CrearSolicitudPago(PreferenceData data);
    }
}
