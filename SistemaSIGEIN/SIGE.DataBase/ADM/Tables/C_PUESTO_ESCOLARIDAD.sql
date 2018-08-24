﻿CREATE TABLE [ADM].[C_PUESTO_ESCOLARIDAD] (
    [ID_PUESTO_ESCOLARIDAD]   INT           IDENTITY (1, 1) NOT NULL,
    [ID_PUESTO]               INT           NOT NULL,
    [ID_ESCOLARIDAD]          INT           NOT NULL,
    [FE_CREACION]             DATETIME      NOT NULL,
    [FE_MODIFICACION]         DATETIME      NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50) NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50) NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50) NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_C_PUESTO_ESCOLARIDAD] PRIMARY KEY CLUSTERED ([ID_PUESTO_ESCOLARIDAD] ASC),
    CONSTRAINT [FK_C_PUESTO_ESCOLARIDAD_C_ESCOLARIDAD] FOREIGN KEY ([ID_ESCOLARIDAD]) REFERENCES [ADM].[C_ESCOLARIDAD] ([ID_ESCOLARIDAD])
);
