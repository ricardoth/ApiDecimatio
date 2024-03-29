/****** Object:  Table [dbo].[AccesoEvento]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccesoEvento](
	[IdAccesoEvento] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTicket] [bigint] NOT NULL,
	[IdEstadoTicket] [int] NOT NULL,
	[FechaHoraEntrada] [datetime] NULL,
	[FechaHoraSalida] [datetime] NULL,
 CONSTRAINT [PK_AccesoEvento] PRIMARY KEY CLUSTERED 
(
	[IdAccesoEvento] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Banda]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banda](
	[IdBanda] [bigint] IDENTITY(1,1) NOT NULL,
	[NombreBanda] [varchar](100) NOT NULL,
	[Genero] [varchar](50) NOT NULL,
	[Pais] [varchar](100) NOT NULL,
	[Ciudad] [varchar](200) NOT NULL,
	[FormadoEn] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Banda] PRIMARY KEY CLUSTERED 
(
	[IdBanda] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comuna]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comuna](
	[IdComuna] [int] IDENTITY(1,1) NOT NULL,
	[IdRegion] [int] NOT NULL,
	[NombreComuna] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Comuna] PRIMARY KEY CLUSTERED 
(
	[IdComuna] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoTicket]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoTicket](
	[IdEstadoTicket] [int] NOT NULL,
	[EstadoTicket] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_EstadoTicket] PRIMARY KEY CLUSTERED 
(
	[IdEstadoTicket] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[IdEvento] [bigint] IDENTITY(1,1) NOT NULL,
	[IdLugar] [int] NOT NULL,
	[NombreEvento] [varchar](250) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Direccion] [varchar](300) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Flyer] [varchar](400) NULL,
	[ContenidoFlyer] [varchar](max) NULL,
	[Observacion] [varchar](max) NULL,
	[ProductoraResponsable] [varchar](500) NULL,
	[Banner] [bit] NULL,
	[ContenidoBanner] [varchar](400) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[IdEvento] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventoBanda]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventoBanda](
	[IdEventoBanda] [bigint] IDENTITY(1,1) NOT NULL,
	[IdEvento] [bigint] NOT NULL,
	[IdBanda] [bigint] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_EventoBanda] PRIMARY KEY CLUSTERED 
(
	[IdEventoBanda] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lugar]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lugar](
	[IdLugar] [int] IDENTITY(1,1) NOT NULL,
	[IdComuna] [int] NULL,
	[NombreLugar] [varchar](300) NOT NULL,
	[Ubicacion] [varchar](500) NOT NULL,
	[Numeracion] [varchar](20) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[MapaReferencial] [varchar](max) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Lugar] PRIMARY KEY CLUSTERED 
(
	[IdLugar] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedioPago]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedioPago](
	[IdMedioPago] [bigint] IDENTITY(1,1) NOT NULL,
	[NombreMedioPago] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[UrlImageBlob] [varchar](300) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_MedioPago] PRIMARY KEY CLUSTERED 
(
	[IdMedioPago] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreferenceTicket]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreferenceTicket](
	[IdPreference] [bigint] IDENTITY(1,1) NOT NULL,
	[PreferenceCode] [varchar](500) NOT NULL,
	[TransactionId] [varchar](100) NOT NULL,
	[IdUsuario] [int] NULL,
	[IdEvento] [int] NULL,
	[IdSector] [int] NULL,
	[IdMedioPago] [int] NULL,
	[MontoPago] [decimal](18, 4) NULL,
	[MontoTotal] [decimal](18, 4) NULL,
	[FechaTicket] [datetime] NULL,
	[Descargados] [bit] NULL,
	[Activo] [bit] NULL,
	[FecCreacion] [datetime] NULL,
 CONSTRAINT [PK_PreferenceTicket] PRIMARY KEY CLUSTERED 
(
	[IdPreference] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[IdRegion] [int] IDENTITY(1,1) NOT NULL,
	[NumeroRegion] [int] NOT NULL,
	[Abreviatura] [varchar](10) NOT NULL,
	[NombreRegion] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[IdRegion] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sector]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sector](
	[IdSector] [bigint] IDENTITY(1,1) NOT NULL,
	[IdEvento] [bigint] NOT NULL,
	[NombreSector] [varchar](200) NOT NULL,
	[CapacidadDisponible] [int] NULL,
	[CapacidadActual] [int] NULL,
	[CapacidadTotal] [int] NOT NULL,
	[Precio] [numeric](18, 4) NOT NULL,
	[Cargo] [numeric](18, 4) NULL,
	[Total] [numeric](18, 4) NULL,
	[ColorHexa] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[IdSector] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SectorUbicacion]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SectorUbicacion](
	[IdSectorUbicacion] [bigint] NOT NULL,
	[IdSector] [bigint] NOT NULL,
	[NumeroAsiento] [int] NOT NULL,
	[Fila] [varchar](5) NOT NULL,
	[Disponible] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_SectorUbicacion] PRIMARY KEY CLUSTERED 
(
	[IdSectorUbicacion] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[IdTicket] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[IdEvento] [bigint] NOT NULL,
	[IdSector] [bigint] NOT NULL,
	[IdMedioPago] [bigint] NOT NULL,
	[MontoPago] [decimal](18, 4) NOT NULL,
	[MontoTotal] [decimal](18, 4) NOT NULL,
	[FechaTicket] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaModified] [datetime] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[IdTicket] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketQR]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketQR](
	[IdTicketQR] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTicket] [bigint] NOT NULL,
	[Contenido] [varchar](max) NOT NULL,
	[NombreTicketComprobante] [varchar](1000) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TicketQR] PRIMARY KEY CLUSTERED 
(
	[IdTicketQR] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoUsuario](
	[IdTipoUsuario] [smallint] IDENTITY(1,1) NOT NULL,
	[NombreTipoUsuario] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_TipoUsuario] PRIMARY KEY CLUSTERED 
(
	[IdTipoUsuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTipoUsuario] [smallint] NOT NULL,
	[Rut] [int] NOT NULL,
	[DV] [varchar](2) NOT NULL,
	[Nombres] [varchar](250) NOT NULL,
	[ApellidoP] [varchar](100) NOT NULL,
	[ApellidoM] [varchar](100) NULL,
	[Direccion] [varchar](250) NOT NULL,
	[Telefono] [varchar](20) NULL,
	[Correo] [varchar](200) NOT NULL,
	[Contrasena] [varchar](500) NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventoBanda] ADD  CONSTRAINT [DF_EventoBanda_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Lugar] ADD  CONSTRAINT [DF_Lugar_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[PreferenceTicket] ADD  CONSTRAINT [DF_PreferenceTicket_FecCreacion]  DEFAULT (getdate()) FOR [FecCreacion]
GO
ALTER TABLE [dbo].[Ticket] ADD  CONSTRAINT [DF_Ticket_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[TicketQR] ADD  CONSTRAINT [DF_TicketQR_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[AccesoEvento]  WITH CHECK ADD  CONSTRAINT [FK_AccesoEvento_Ticket] FOREIGN KEY([IdEstadoTicket])
REFERENCES [dbo].[EstadoTicket] ([IdEstadoTicket])
GO
ALTER TABLE [dbo].[AccesoEvento] CHECK CONSTRAINT [FK_AccesoEvento_Ticket]
GO
ALTER TABLE [dbo].[AccesoEvento]  WITH CHECK ADD  CONSTRAINT [FK_AccesoEvento_Ticket1] FOREIGN KEY([IdTicket])
REFERENCES [dbo].[Ticket] ([IdTicket])
GO
ALTER TABLE [dbo].[AccesoEvento] CHECK CONSTRAINT [FK_AccesoEvento_Ticket1]
GO
ALTER TABLE [dbo].[Comuna]  WITH CHECK ADD  CONSTRAINT [FK_Comuna_Region] FOREIGN KEY([IdRegion])
REFERENCES [dbo].[Region] ([IdRegion])
GO
ALTER TABLE [dbo].[Comuna] CHECK CONSTRAINT [FK_Comuna_Region]
GO
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Evento_Lugar] FOREIGN KEY([IdLugar])
REFERENCES [dbo].[Lugar] ([IdLugar])
GO
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK_Evento_Lugar]
GO
ALTER TABLE [dbo].[EventoBanda]  WITH CHECK ADD  CONSTRAINT [FK_EventoBanda_Banda] FOREIGN KEY([IdBanda])
REFERENCES [dbo].[Banda] ([IdBanda])
GO
ALTER TABLE [dbo].[EventoBanda] CHECK CONSTRAINT [FK_EventoBanda_Banda]
GO
ALTER TABLE [dbo].[EventoBanda]  WITH CHECK ADD  CONSTRAINT [FK_EventoBanda_Evento] FOREIGN KEY([IdEvento])
REFERENCES [dbo].[Evento] ([IdEvento])
GO
ALTER TABLE [dbo].[EventoBanda] CHECK CONSTRAINT [FK_EventoBanda_Evento]
GO
ALTER TABLE [dbo].[Sector]  WITH CHECK ADD  CONSTRAINT [FK_Sector_Sector] FOREIGN KEY([IdEvento])
REFERENCES [dbo].[Evento] ([IdEvento])
GO
ALTER TABLE [dbo].[Sector] CHECK CONSTRAINT [FK_Sector_Sector]
GO
ALTER TABLE [dbo].[SectorUbicacion]  WITH CHECK ADD  CONSTRAINT [FK_SectorUbicacion_Sector] FOREIGN KEY([IdSector])
REFERENCES [dbo].[Sector] ([IdSector])
GO
ALTER TABLE [dbo].[SectorUbicacion] CHECK CONSTRAINT [FK_SectorUbicacion_Sector]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Evento] FOREIGN KEY([IdEvento])
REFERENCES [dbo].[Evento] ([IdEvento])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Evento]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_MedioPago] FOREIGN KEY([IdMedioPago])
REFERENCES [dbo].[MedioPago] ([IdMedioPago])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_MedioPago]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Sector] FOREIGN KEY([IdSector])
REFERENCES [dbo].[Sector] ([IdSector])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Sector]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Usuario]
GO
ALTER TABLE [dbo].[TicketQR]  WITH CHECK ADD  CONSTRAINT [FK_TicketQR_Ticket] FOREIGN KEY([IdTicket])
REFERENCES [dbo].[Ticket] ([IdTicket])
GO
ALTER TABLE [dbo].[TicketQR] CHECK CONSTRAINT [FK_TicketQR_Ticket]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_TipoUsuario] FOREIGN KEY([IdTipoUsuario])
REFERENCES [dbo].[TipoUsuario] ([IdTipoUsuario])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_TipoUsuario]
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidarTicket]    Script Date: 26-12-2023 16:03:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ValidarTicket]
	@IdTicket BIGINT,
	@Rut INT,
	@Dv VARCHAR(1),
	@IdEvento INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM dbo.Ticket T
		INNER JOIN dbo.TicketQR TQ ON TQ.IdTicket = T.IdTicket AND TQ.Activo = 1
		INNER JOIN dbo.Usuario U ON U.IdUsuario = T.IdUsuario
		INNER JOIN dbo.Evento E ON E.IdEvento = T.IdEvento AND E.Activo = 1
	WHERE T.IdTicket = @IdTicket
		AND U.Rut = @Rut
		AND U.Dv  = @Dv
		AND T.Activo = 1
		AND E.IdEvento = @IdEvento
		AND NOT EXISTS (SELECT 1 FROM dbo.AccesoEvento AE WHERE AE.IdTicket = T.IdTicket
			AND AE.IdEstadoTicket = 1
			AND AE.FechaHoraSalida IS NULL)
	)
	BEGIN
		SELECT 1 AS StatusCode,
			'VÁLIDO' AS OutputMessage;
		RETURN;
	END

	--DUPLICADOS
	IF EXISTS(
		SELECT 1 
		FROM dbo.AccesoEvento AE
			INNER JOIN dbo.Ticket T ON T.IdTicket = AE.IdTicket
			INNER JOIN dbo.Usuario U ON U.IdUsuario = T.IdUsuario
			INNER JOIN dbo.Evento E ON E.IdEvento = T.IdEvento AND E.Activo = 1
		WHERE AE.IdTicket = @IdTicket
			AND U.Rut = @Rut
			AND U.DV = @Dv
			AND E.IdEvento = @IdEvento
			AND AE.FechaHoraSalida IS NULL
	)
	BEGIN
		SELECT 3 AS StatusCode,
			'DUPLICADO' AS OutputMessage;
		RETURN;
	END

	--LÓGICA INVÁLIDOS CON INSERT A ACCESOEVENTO
	SELECT 2 AS StatusCode,
			'INVÁLIDO' AS OutputMessage;
END
GO
