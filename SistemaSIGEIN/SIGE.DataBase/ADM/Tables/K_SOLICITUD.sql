﻿CREATE TABLE [ADM].[K_SOLICITUD] (
    [ID_SOLICITUD]            INT           IDENTITY (1, 1) NOT NULL,
    [ID_CANDIDATO]            INT           NULL,
    [ID_EMPLEADO]             INT           NULL,
    [ID_DESCRIPTIVO]          INT           NULL,
    [ID_REQUISICION]          INT           NULL,
    [CL_SOLICITUD]            NVARCHAR (20) NULL,
    [CL_ACCESO_EVALUACION]    NVARCHAR (40) NULL,
    [ID_PLANTILLA_SOLICITUD]  INT           NULL,
    [CL_SOLICITUD_ESTATUS]    NVARCHAR (20) NULL,
    [FE_SOLICITUD]            DATETIME      NULL,
    [XML_PLANTILLA_SOLICITUD] XML           NULL,
    [FE_CREACION]             DATETIME      NOT NULL,
    [FE_MODIFICACION]         DATETIME      NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50) NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50) NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50) NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_K_SOLICITUD] PRIMARY KEY CLUSTERED ([ID_SOLICITUD] ASC),
    CONSTRAINT [FK_K_SOLICITUD_K_REQUISICION] FOREIGN KEY ([ID_REQUISICION]) REFERENCES [ADM].[K_REQUISICION] ([ID_REQUISICION])
);
