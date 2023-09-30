﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Decimatio.Infraestructure.Repositories {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Queries {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Queries() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Decimatio.Infraestructure.Repositories.Queries", typeof(Queries).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a DELETE FROM dbo.MedioPago WHERE IdMedioPago = @IdMedioPago.
        /// </summary>
        internal static string DELETE_MEDIO_PAGO {
            get {
                return ResourceManager.GetString("DELETE_MEDIO_PAGO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE dbo.Ticket
        ///SET Activo = @Activo,
        ///FechaModified = GETDATE()
        ///WHERE IdTicket = @IdTicket
        ///
        ///UPDATE dbo.TicketQR
        ///SET Activo = @Activo
        ///WHERE IdTicket = @IdTicket.
        /// </summary>
        internal static string DELETE_TICKET {
            get {
                return ResourceManager.GetString("DELETE_TICKET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT 
        ///	   AE.IdAccesoEvento
        ///      ,AE.IdTicket
        ///	  ,T.IdUsuario
        ///	  ,U.Rut
        ///	  ,U.Dv
        ///	  ,U.Nombres
        ///	  ,U.ApellidoP
        ///	  ,U.ApellidoM
        ///      ,AE.IdEstadoTicket
        ///	  ,ET.EstadoTicket
        ///      ,AE.FechaHoraEntrada
        ///      ,AE.FechaHoraSalida
        ///FROM dbo.AccesoEvento AE
        ///	INNER JOIN dbo.Ticket T ON T.IdTicket = AE.IdTicket AND T.Activo = 1
        ///	INNER JOIN dbo.Usuario U ON U.IdUsuario = T.IdUsuario
        ///	INNER JOIN dbo.EstadoTicket ET ON ET.IdEstadoTicket = AE.IdEstadoTicket.
        /// </summary>
        internal static string GET_ACCESOS_TICKET {
            get {
                return ResourceManager.GetString("GET_ACCESOS_TICKET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT 
        ///	   E.IdEvento
        ///      ,E.IdLugar
        ///      ,E.NombreEvento
        ///      ,E.Direccion
        ///      ,E.Fecha
        ///	  ,E.Flyer
        ///      ,E.Activo
        ///	  ,L.NombreLugar
        ///	  ,L.Ubicacion
        ///	  ,L.Numeracion
        ///  FROM [Decimatio].[dbo].[Evento] E
        ///	INNER JOIN dbo.Lugar L ON L.IdLugar = E.IdLugar
        ///  WHERE IdEvento = @IdEvento.
        /// </summary>
        internal static string GET_EVENTO_ID {
            get {
                return ResourceManager.GetString("GET_EVENTO_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT [IdEvento]
        ///      ,[IdLugar]
        ///      ,[NombreEvento]
        ///      ,[Direccion]
        ///      ,[Fecha]
        ///      ,[Activo]
        ///  FROM [Decimatio].[dbo].[Evento].
        /// </summary>
        internal static string GET_EVENTOS {
            get {
                return ResourceManager.GetString("GET_EVENTOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT t.IdTicket, t.IdUsuario, t.IdEvento, t.IdSector, t.IdMedioPago,
        ///        t.MontoPago, t.MontoTotal, t.FechaTicket, t.Activo, t.FechaCreacion, t.FechaModified,
        ///        u.*, e.*, s.*, m.*, TQR.*, L.*,
        ///		c.*, r.*
        ///FROM Ticket t
        ///INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
        ///INNER JOIN Evento e ON t.IdEvento = e.IdEvento
        ///INNER JOIN [dbo].[Lugar] L ON L.IdLugar = E.IdLugar
        ///INNER JOIN Sector s ON t.IdSector = s.IdSector
        ///INNER JOIN MedioPago m ON t.IdMedioPago = m.IdMedioPago
        ///LEFT JOIN TicketQR T [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string GET_INFO_TICKET {
            get {
                return ResourceManager.GetString("GET_INFO_TICKET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT  [IdMedioPago]
        ///      ,[NombreMedioPago]
        ///      ,[Descripcion]
        ///      ,[Activo]
        ///  FROM [Decimatio].[dbo].[MedioPago] WHERE IdMedioPago = @IdMedioPago.
        /// </summary>
        internal static string GET_MEDIO_PAGO {
            get {
                return ResourceManager.GetString("GET_MEDIO_PAGO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT  [IdMedioPago]
        ///      ,[NombreMedioPago]
        ///      ,[Descripcion]
        ///      ,[Activo]
        ///  FROM [Decimatio].[dbo].[MedioPago].
        /// </summary>
        internal static string GET_MEDIOS_PAGOS {
            get {
                return ResourceManager.GetString("GET_MEDIOS_PAGOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT S.[IdSector]
        ///      ,S.IdEvento
        ///      ,S.[NombreSector]
        ///	  ,S.[CapacidadDisponible]
        ///	  ,S.[CapacidadActual]
        ///      ,S.[CapacidadTotal]
        ///	  ,S.Precio
        ///      ,S.[Activo]
        ///  FROM [Decimatio].[dbo].[Sector] S
        ///  WHERE S.IdSector = @IdSector.
        /// </summary>
        internal static string GET_SECTOR_ID {
            get {
                return ResourceManager.GetString("GET_SECTOR_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT S.[IdSector]
        ///      ,S.IdEvento
        ///      ,S.[NombreSector]
        ///      ,S.[CapacidadDisponible]
        ///	  ,S.[CapacidadActual]
        ///      ,S.[CapacidadTotal]
        ///	  ,S.Precio
        ///      ,S.[Activo]
        ///  FROM [Decimatio].[dbo].[Sector] S.
        /// </summary>
        internal static string GET_SECTORES {
            get {
                return ResourceManager.GetString("GET_SECTORES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT S.[IdSector]
        ///      ,S.[IdEvento]
        ///      ,S.[NombreSector]
        ///      ,S.[CapacidadDisponible]
        ///	  ,S.[CapacidadActual]
        ///      ,S.[CapacidadTotal]
        ///      ,S.Precio
        ///      ,S.[Activo]
        ///  FROM [Decimatio].[dbo].[Sector] S
        ///	JOIN Evento E ON E.IdEvento = S.IdEvento
        ///  WHERE E.IdEvento = @IdEvento.
        /// </summary>
        internal static string GET_SECTORES_BY_EVENTO {
            get {
                return ResourceManager.GetString("GET_SECTORES_BY_EVENTO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT TQ.* , T.*
        ///FROM dbo.Ticket T
        ///	LEFT JOIN dbo.TicketQR TQ ON T.IdTicket =  TQ.IdTicket
        ///WHERE T.IdTicket = @IdTicket.
        /// </summary>
        internal static string GET_TICKET_ID {
            get {
                return ResourceManager.GetString("GET_TICKET_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT t.IdTicket, t.IdUsuario, t.IdEvento, t.IdSector, t.IdMedioPago,
        ///        t.MontoPago, t.MontoTotal, t.FechaTicket, t.Activo, t.FechaCreacion, t.FechaModified,
        ///        u.*, e.*, s.*, m.*, TQR.*, L.*,
        ///		c.*, r.*
        ///FROM Ticket t
        ///INNER JOIN Usuario u ON t.IdUsuario = u.IdUsuario
        ///INNER JOIN Evento e ON t.IdEvento = e.IdEvento
        ///INNER JOIN [dbo].[Lugar] L ON L.IdLugar = E.IdLugar
        ///INNER JOIN Sector s ON t.IdSector = s.IdSector
        ///INNER JOIN MedioPago m ON t.IdMedioPago = m.IdMedioPago
        ///LEFT JOIN TicketQR T [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string GET_TICKETS {
            get {
                return ResourceManager.GetString("GET_TICKETS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT [IdUsuario]
        ///      ,[IdTipoUsuario]
        ///      ,[Rut]
        ///      ,[DV]
        ///      ,[Nombres]
        ///      ,[ApellidoP]
        ///      ,[ApellidoM]
        ///      ,[Direccion]
        ///      ,[Telefono]
        ///      ,[Correo]
        ///      ,[Activo]
        ///      ,[FechaCreacion]
        ///  FROM [Decimatio].[dbo].[Usuario]
        ///  WHERE IdUsuario = @IdUsuario.
        /// </summary>
        internal static string GET_USUARIO_ID {
            get {
                return ResourceManager.GetString("GET_USUARIO_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT [IdUsuario]
        ///      ,U.[IdTipoUsuario]
        ///	  ,NombreTipoUsuario
        ///      ,[Rut]
        ///      ,[DV]
        ///      ,[Nombres]
        ///      ,[ApellidoP]
        ///      ,[ApellidoM]
        ///      ,[Direccion]
        ///      ,[Telefono]
        ///      ,[Correo]
        ///      ,U.[Activo]
        ///      ,U.[FechaCreacion]
        ///  FROM [Decimatio].[dbo].[Usuario] U
        ///	INNER JOIN dbo.TipoUsuario TU ON TU.IdTipoUsuario = U.IdTipoUsuario.
        /// </summary>
        internal static string GET_USUARIOS {
            get {
                return ResourceManager.GetString("GET_USUARIOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT [IdUsuario]
        ///      ,U.[IdTipoUsuario]
        ///	  ,NombreTipoUsuario
        ///      ,[Rut]
        ///      ,[DV]
        ///      ,[Nombres]
        ///      ,[ApellidoP]
        ///      ,[ApellidoM]
        ///      ,[Direccion]
        ///      ,[Telefono]
        ///      ,[Correo]
        ///      ,U.[Activo]
        ///      ,U.[FechaCreacion]
        ///  FROM [Decimatio].[dbo].[Usuario] U
        ///	INNER JOIN dbo.TipoUsuario TU ON TU.IdTipoUsuario = U.IdTipoUsuario
        ///  WHERE Nombres LIKE &apos;%&apos;+@Filtro+&apos;%&apos;
        ///	OR ApellidoP LIKE &apos;%&apos;+ @Filtro+&apos;%&apos;
        ///	OR ApellidoM LIKE &apos;%&apos;+ @Filtro+&apos;%&apos;
        ///	OR CAST(Rut AS VARCHAR) LIKE &apos;%&apos;+ @F [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string GET_USUARIOS_FILTRO {
            get {
                return ResourceManager.GetString("GET_USUARIOS_FILTRO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO dbo.AccesoEvento
        ///(
        ///	IdTicket,
        ///	IdEstadoTicket,
        ///	FechaHoraEntrada,
        ///	FechaHoraSalida
        ///)
        ///
        ///VALUES (
        ///	@IdTicket,
        ///	@IdEstadoTicket,
        ///	@FechaHoraEntrada,
        ///	NULL
        ///).
        /// </summary>
        internal static string INSERT_ACCESO_EVENTO_IN {
            get {
                return ResourceManager.GetString("INSERT_ACCESO_EVENTO_IN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE dbo.AccesoEvento
        ///SET FechaHoraSalida = GETDATE()
        ///WHERE IdAccesoEvento = @IdAccesoEvento.
        /// </summary>
        internal static string INSERT_ACCESO_EVENTO_OUT {
            get {
                return ResourceManager.GetString("INSERT_ACCESO_EVENTO_OUT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[MedioPago]
        ///           ([NombreMedioPago]
        ///           ,[Descripcion]
        ///           ,[Activo])
        ///     VALUES
        ///           (@NombreMedioPago
        ///  ,@Descripcion
        ///           ,@Activo).
        /// </summary>
        internal static string INSERT_MEDIO_PAGO {
            get {
                return ResourceManager.GetString("INSERT_MEDIO_PAGO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[Ticket]
        ///           ([IdUsuario]
        ///           ,[IdEvento]
        ///           ,[IdSector]
        ///           ,[IdMedioPago]
        ///           ,[MontoPago]
        ///           ,[MontoTotal]
        ///           ,[FechaTicket]
        ///           ,[Activo]
        ///           ,[FechaCreacion])
        ///     VALUES
        ///           (@IdUsuario
        ///           ,@IdEvento
        ///           ,@IdSector
        ///           ,@IdMedioPago
        ///           ,@MontoPago
        ///           ,@MontoTotal
        ///           ,@FechaTicket
        ///           ,1
        ///           ,GETDATE()) 
        ///	SELECT CAST(SCOPE_IDENTITY()  [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string INSERT_TICKET {
            get {
                return ResourceManager.GetString("INSERT_TICKET", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[TicketQR]
        ///           ([IdTicket]
        ///           ,[Contenido]
        ///           ,[NombreTicketComprobante]
        ///           ,[Activo]
        ///           ,[FechaCreacion])
        ///     VALUES
        ///           (@IdTicket
        ///           ,@Contenido
        ///           ,@NombreTicketComprobante
        ///           ,1
        ///           ,GETDATE()).
        /// </summary>
        internal static string INSERT_TICKETQR {
            get {
                return ResourceManager.GetString("INSERT_TICKETQR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE dbo.MedioPago SET [NombreMedioPago] =@NombreMedioPago
        ///           ,[Descripcion] = @Descripcion
        ///           ,[Activo] = @Activo WHERE IdMedioPago = @IdMedioPago.
        /// </summary>
        internal static string UPDATE_MEDIO_PAGO {
            get {
                return ResourceManager.GetString("UPDATE_MEDIO_PAGO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a dbo.sp_ValidarTicket @IdTicket,  @Rut, @Dv, @IdEvento.
        /// </summary>
        internal static string VALIDAR_ACCESO_TICKET {
            get {
                return ResourceManager.GetString("VALIDAR_ACCESO_TICKET", resourceCulture);
            }
        }
    }
}
