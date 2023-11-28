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
    internal class Querys {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Querys() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Decimatio.Infraestructure.Repositories.Querys", typeof(Querys).Assembly);
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
        ///   Busca una cadena traducida similar a UPDATE dbo.Evento
        ///SET Activo = 0
        ///WHERE IdEvento = @IdEvento.
        /// </summary>
        internal static string DELETE_EVENTO {
            get {
                return ResourceManager.GetString("DELETE_EVENTO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE [dbo].[Lugar]
        ///   SET 
        ///      [Activo] = 0
        /// WHERE IdLugar = @IdLugar.
        /// </summary>
        internal static string DELETE_LUGAR {
            get {
                return ResourceManager.GetString("DELETE_LUGAR", resourceCulture);
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
        ///   Busca una cadena traducida similar a UPDATE dbo.Sector
        ///SET 	Activo = 0
        ///WHERE 	IdSector = @IdSector.
        /// </summary>
        internal static string DELETE_SECTOR {
            get {
                return ResourceManager.GetString("DELETE_SECTOR", resourceCulture);
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
        ///   Busca una cadena traducida similar a UPDATE [dbo].[Usuario]
        ///   SET
        ///       [Activo] = 0
        /// WHERE IdUsuario = @IdUsuario.
        /// </summary>
        internal static string DELETE_USUARIO {
            get {
                return ResourceManager.GetString("DELETE_USUARIO", resourceCulture);
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
        ///	   IdComuna
        ///      ,IdRegion
        ///      ,NombreComuna
        ///      ,Activo
        ///  FROM dbo.Comuna
        ///  WHERE IdRegion = @IdRegion.
        /// </summary>
        internal static string GET_COMUNAS_BY_REGION {
            get {
                return ResourceManager.GetString("GET_COMUNAS_BY_REGION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT 
        ///	   E.IdEvento
        ///      ,E.IdLugar
        ///      ,E.NombreEvento
        ///	  ,E.Descripcion
        ///      ,E.Direccion
        ///      ,E.Fecha
        ///	  ,E.Flyer
        ///	  ,E.ContenidoFlyer
        ///	  ,E.Observacion
        ///	  ,E.ProductoraResponsable
        ///      ,E.Activo
        ///	  ,L.NombreLugar
        ///	  ,L.Ubicacion
        ///	  ,L.Numeracion
        ///	  ,L.MapaReferencial
        ///  FROM [dbo].[Evento] E
        ///	INNER JOIN dbo.Lugar L ON L.IdLugar = E.IdLugar
        ///  WHERE IdEvento = @IdEvento.
        /// </summary>
        internal static string GET_EVENTO_ID {
            get {
                return ResourceManager.GetString("GET_EVENTO_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT E.IdEvento
        ///      ,E.IdLugar
        ///      ,E.NombreEvento
        ///      ,E.Descripcion
        ///      ,E.Direccion
        ///      ,E.Fecha
        ///      ,E.Flyer
        ///      ,E.ContenidoFlyer
        ///      ,E.Observacion
        ///      ,E.ProductoraResponsable
        ///      ,E.Activo
        ///      ,L.IdLugar
        ///      ,L.NombreLugar
        ///      ,L.Ubicacion
        ///      ,L.Numeracion
        ///      ,L.MapaReferencial
        ///  FROM dbo.Evento E
        ///	INNER JOIN dbo.Lugar L ON L.IdLugar = E.IdLugar.
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
        ///   Busca una cadena traducida similar a SELECT L.IdLugar
        ///      ,L.IdComuna
        ///      ,L.NombreLugar
        ///      ,L.Ubicacion
        ///      ,L.Numeracion
        ///      ,L.FechaCreacion
        ///	  ,L.MapaReferencial
        ///      ,L.Activo
        ///	  ,C.IdComuna
        ///	  ,C.IdRegion
        ///	  ,C.NombreComuna
        ///	  ,R.IdRegion
        ///	  ,R.NombreRegion
        ///	  ,R.Abreviatura
        ///  FROM dbo.Lugar L
        ///	  INNER JOIN dbo.Comuna C ON C.IdComuna = L.IdComuna
        ///	  INNER JOIN dbo.Region R ON R.IdRegion = C.IdRegion
        ///  WHERE L.IdLugar = @IdLugar.
        /// </summary>
        internal static string GET_LUGAR_ID {
            get {
                return ResourceManager.GetString("GET_LUGAR_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT L.IdLugar
        ///      ,L.IdComuna
        ///      ,L.NombreLugar
        ///      ,L.Ubicacion
        ///      ,L.Numeracion
        ///      ,L.FechaCreacion
        ///	  ,L.MapaReferencial
        ///      ,L.Activo
        ///	  ,C.IdComuna
        ///	  ,C.IdRegion
        ///	  ,C.NombreComuna
        ///	  ,R.IdRegion
        ///	  ,R.NombreRegion
        ///	  ,R.Abreviatura
        ///  FROM dbo.Lugar L
        ///  INNER JOIN dbo.Comuna C ON C.IdComuna = L.IdComuna
        ///  INNER JOIN dbo.Region R ON R.IdRegion = C.IdRegion.
        /// </summary>
        internal static string GET_LUGARES {
            get {
                return ResourceManager.GetString("GET_LUGARES", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT  [IdMedioPago]
        ///      ,[NombreMedioPago]
        ///      ,[Descripcion]
        ///      ,[Activo]
        ///  FROM [dbo].[MedioPago] WHERE IdMedioPago = @IdMedioPago.
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
        ///  FROM [dbo].[MedioPago].
        /// </summary>
        internal static string GET_MEDIOS_PAGOS {
            get {
                return ResourceManager.GetString("GET_MEDIOS_PAGOS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT IdRegion
        ///      ,NumeroRegion
        ///      ,Abreviatura
        ///      ,NombreRegion
        ///      ,Activo
        ///  FROM dbo.Region.
        /// </summary>
        internal static string GET_REGIONES {
            get {
                return ResourceManager.GetString("GET_REGIONES", resourceCulture);
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
        ///	  ,S.Cargo
        ///	  ,S.Total
        ///	  ,S.ColorHexa
        ///      ,S.[Activo]
        ///  FROM [dbo].[Sector] S
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
        ///	  ,S.Cargo
        ///	  ,S.Total
        ///	  ,S.ColorHexa
        ///      ,S.[Activo]
        ///	  ,E.IdEvento
        ///	  ,E.NombreEvento
        ///  FROM [dbo].[Sector] S
        ///	INNER JOIN dbo.Evento E ON E.IdEvento = S.IdEvento.
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
        ///	  ,S.Cargo
        ///	  ,S.Total
        ///	  ,S.ColorHexa
        ///      ,S.[Activo]
        ///	  ,E.IdEvento
        ///	  ,E.NombreEvento
        ///  FROM [dbo].[Sector] S
        ///	INNER JOIN Evento E ON E.IdEvento = S.IdEvento
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
        ///   Busca una cadena traducida similar a SELECT [IdTipoUsuario]
        ///      ,[NombreTipoUsuario]
        ///      ,[Activo]
        ///  FROM [dbo].[TipoUsuario].
        /// </summary>
        internal static string GET_TIPO_USUARIO {
            get {
                return ResourceManager.GetString("GET_TIPO_USUARIO", resourceCulture);
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
        ///  FROM [dbo].[Usuario]
        ///  WHERE IdUsuario = @IdUsuario.
        /// </summary>
        internal static string GET_USUARIO_ID {
            get {
                return ResourceManager.GetString("GET_USUARIO_ID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT U.[IdUsuario]
        ///      ,U.[IdTipoUsuario]
        ///      ,U.[Rut]
        ///      ,U.[DV]
        ///      ,U.[Nombres]
        ///      ,U.[ApellidoP]
        ///      ,U.[ApellidoM]
        ///      ,U.[Direccion]
        ///      ,U.[Telefono]
        ///      ,U.[Correo]
        ///      ,U.[Activo]
        ///      ,U.[FechaCreacion]
        ///	  ,TU.IdTipoUsuario
        ///	  ,TU.NombreTipoUsuario
        ///  FROM [dbo].[Usuario] U
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
        ///  FROM [dbo].[Usuario] U
        ///	INNER JOIN dbo.TipoUsuario TU ON TU.IdTipoUsuario = U.IdTipoUsuario
        ///  WHERE Nombres LIKE &apos;%&apos;+@Filtro+&apos;%&apos;
        ///	OR ApellidoP LIKE &apos;%&apos;+ @Filtro+&apos;%&apos;
        ///	OR ApellidoM LIKE &apos;%&apos;+ @Filtro+&apos;%&apos;
        ///	OR CAST(Rut AS VARCHAR) LIKE &apos;%&apos;+ @Filtro+&apos;%&apos;.
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
        ///   Busca una cadena traducida similar a INSERT INTO dbo.Evento(
        ///	IdLugar,
        ///	NombreEvento,
        ///	Descripcion,
        ///	Direccion,
        ///	Fecha,
        ///	Flyer,
        ///	ContenidoFlyer,
        ///	Observacion,
        ///	ProductoraResponsable,
        ///	Activo
        ///)
        ///
        ///VALUES (
        ///	@IdLugar,
        ///	@NombreEvento,
        ///	@Descripcion,
        ///	@Direccion,
        ///	@Fecha,
        ///	@Flyer,
        ///	@ContenidoFlyer,
        ///	@Observacion,
        ///	@ProductoraResponsable,
        ///	@Activo
        ///).
        /// </summary>
        internal static string INSERT_EVENTO {
            get {
                return ResourceManager.GetString("INSERT_EVENTO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[Lugar]
        ///           ([IdComuna]
        ///           ,[NombreLugar]
        ///           ,[Ubicacion]
        ///           ,[Numeracion]
        ///           ,[FechaCreacion]
        ///           ,[MapaReferencial]
        ///           ,[Activo])
        ///     VALUES
        ///           (@IdComuna
        ///           ,@NombreLugar
        ///           ,@Ubicacion
        ///           ,@Numeracion
        ///           ,GETDATE()
        ///          ,@MapaReferencial
        ///           ,@Activo).
        /// </summary>
        internal static string INSERT_LUGAR {
            get {
                return ResourceManager.GetString("INSERT_LUGAR", resourceCulture);
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
        ///   Busca una cadena traducida similar a INSERT INTO dbo.Sector(
        ///	IdEvento,
        ///	NombreSector,
        ///	CapacidadDisponible,
        ///	CapacidadActual,
        ///	CapacidadTotal,
        ///	Precio,
        ///	Cargo,
        ///	Total,
        ///	ColorHexa,
        ///	Activo
        ///)
        ///VALUES(
        ///	@IdEvento,
        ///	@NombreSector,
        ///	@CapacidadDisponible,
        ///	@CapacidadActual,
        ///	@CapacidadTotal,
        ///	@Precio,
        ///	@Cargo,
        ///	@Total,
        ///	@ColorHexa,
        ///	@Activo
        ///).
        /// </summary>
        internal static string INSERT_SECTOR {
            get {
                return ResourceManager.GetString("INSERT SECTOR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE dbo.Sector 
        ///SET CapacidadDisponible = CapacidadDisponible - 1,
        ///	CapacidadActual = CapacidadActual + 1
        ///WHERE IdSector = @IdSector
        ///	AND IdEvento =  @IdEvento
        ///
        ///INSERT INTO [dbo].[Ticket]
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
        ///  [resto de la cadena truncado]&quot;;.
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
        ///   Busca una cadena traducida similar a INSERT INTO [dbo].[Usuario]
        ///           ([IdTipoUsuario]
        ///           ,[Rut]
        ///           ,[DV]
        ///           ,[Nombres]
        ///           ,[ApellidoP]
        ///           ,[ApellidoM]
        ///           ,[Direccion]
        ///           ,[Telefono]
        ///           ,[Correo]
        ///           ,[Activo]
        ///           ,[FechaCreacion])
        ///     VALUES
        ///           (@IdTipoUsuario
        ///           ,@Rut
        ///           ,@Dv
        ///           ,@Nombres
        ///           ,@ApellidoP
        ///           ,@ApellidoM
        ///           ,@Direccion
        ///           ,@Telefono
        ///           ,@Correo
        ///        [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string INSERT_USUARIO {
            get {
                return ResourceManager.GetString("INSERT_USUARIO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE dbo.Evento
        ///SET IdLugar = @IdLugar,
        ///	NombreEvento = @NombreEvento,
        ///	Descripcion = @Descripcion,
        ///	Direccion = @Direccion,
        ///	Fecha = @Fecha,
        ///	Flyer = @Flyer,
        ///	ContenidoFlyer = @ContenidoFlyer,
        ///	Observacion = @Observacion,
        ///	ProductoraResponsable = @ProductoraResponsable,
        ///	Activo = @Activo
        ///WHERE IdEvento = @IdEvento.
        /// </summary>
        internal static string UPDATE_EVENTO {
            get {
                return ResourceManager.GetString("UPDATE_EVENTO", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE [dbo].[Lugar]
        ///   SET [IdComuna] = @IdComuna
        ///      ,[NombreLugar] = @NombreLugar
        ///      ,[Ubicacion] = @Ubicacion
        ///      ,[Numeracion] = @Numeracion
        ///      ,[FechaCreacion] = GETDATE()
        ///	  ,MapaReferencial = @MapaReferencial
        ///      ,[Activo] = @Activo
        /// WHERE IdLugar = @IdLugar.
        /// </summary>
        internal static string UPDATE_LUGAR {
            get {
                return ResourceManager.GetString("UPDATE_LUGAR", resourceCulture);
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
        ///   Busca una cadena traducida similar a UPDATE dbo.Sector
        ///SET 	IdEvento = @IdEvento,
        ///	NombreSector = @NombreSector,
        ///	CapacidadDisponible = @CapacidadDisponible,
        ///	CapacidadActual = @CapacidadActual,
        ///	CapacidadTotal = @CapacidadTotal,
        ///	Precio = @Precio,
        ///	Cargo = @Cargo,
        ///	Total = @Total,
        ///	ColorHexa = @ColorHexa,
        ///	Activo = @Activo
        ///WHERE 	IdSector = @IdSector.
        /// </summary>
        internal static string UPDATE_SECTOR {
            get {
                return ResourceManager.GetString("UPDATE_SECTOR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE [dbo].[Usuario]
        ///   SET [IdTipoUsuario] = @IdTipoUsuario
        ///      ,[Rut] = @Rut
        ///      ,[DV] = @Dv
        ///      ,[Nombres] = @Nombres
        ///      ,[ApellidoP] = @ApellidoP
        ///      ,[ApellidoM] = @ApellidoM
        ///      ,[Direccion] = @Direccion
        ///      ,[Telefono] = @Telefono
        ///      ,[Correo] = @Correo
        ///      ,[Activo] = @Activo
        ///      ,[FechaCreacion] = GETDATE()
        /// WHERE IdUsuario = @IdUsuario.
        /// </summary>
        internal static string UPDATE_USUARIO {
            get {
                return ResourceManager.GetString("UPDATE_USUARIO", resourceCulture);
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
