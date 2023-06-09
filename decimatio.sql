USE [Decimatio]
GO
/****** Object:  Table [dbo].[Banda]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comuna]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 25-04-2023 15:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento](
	[IdEvento] [bigint] IDENTITY(1,1) NOT NULL,
	[IdLugar] [int] NOT NULL,
	[NombreEvento] [varchar](250) NOT NULL,
	[Direccion] [varchar](300) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Evento] PRIMARY KEY CLUSTERED 
(
	[IdEvento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventoBanda]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lugar]    Script Date: 25-04-2023 15:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lugar](
	[IdLugar] [int] NOT NULL,
	[IdComuna] [int] NULL,
	[NombreLugar] [varchar](300) NOT NULL,
	[Ubicacion] [varchar](500) NOT NULL,
	[Numeracion] [varchar](20) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Lugar] PRIMARY KEY CLUSTERED 
(
	[IdLugar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedioPago]    Script Date: 25-04-2023 15:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedioPago](
	[IdMedioPago] [bigint] IDENTITY(1,1) NOT NULL,
	[NombreMedioPago] [varchar](100) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_MedioPago] PRIMARY KEY CLUSTERED 
(
	[IdMedioPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sector]    Script Date: 25-04-2023 15:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sector](
	[IdSector] [bigint] IDENTITY(1,1) NOT NULL,
	[IdLugar] [int] NOT NULL,
	[NombreSector] [varchar](200) NOT NULL,
	[Capacidad] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Sector] PRIMARY KEY CLUSTERED 
(
	[IdSector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketQR]    Script Date: 25-04-2023 15:51:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketQR](
	[IdTicketQR] [bigint] IDENTITY(1,1) NOT NULL,
	[IdTicket] [bigint] NOT NULL,
	[Contenido] [varchar](max) NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_TicketQR] PRIMARY KEY CLUSTERED 
(
	[IdTicketQR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoUsuario]    Script Date: 25-04-2023 15:51:57 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 25-04-2023 15:51:57 ******/
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
	[Activo] [bit] NOT NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EventoBanda] ADD  CONSTRAINT [DF_EventoBanda_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Lugar] ADD  CONSTRAINT [DF_Lugar_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Ticket] ADD  CONSTRAINT [DF_Ticket_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[TicketQR] ADD  CONSTRAINT [DF_TicketQR_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
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
ALTER TABLE [dbo].[Sector]  WITH CHECK ADD  CONSTRAINT [FK_Sector_Lugar] FOREIGN KEY([IdLugar])
REFERENCES [dbo].[Lugar] ([IdLugar])
GO
ALTER TABLE [dbo].[Sector] CHECK CONSTRAINT [FK_Sector_Lugar]
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
