﻿CREATE TABLE [ADM].[C_PUESTO_EXPERIENCIA] (
    [ID_PUESTO_EXPERIENCIA]   INT            IDENTITY (1, 1) NOT NULL,
    [ID_PUESTO]               INT            NOT NULL,
    [ID_AREA_INTERES]         INT            NOT NULL,
    [NO_TIEMPO]               DECIMAL (5, 1) CONSTRAINT [DF_C_PUESTO_EXPERIENCIA_NO_TIEMPO] DEFAULT ((1)) NOT NULL,
    [CL_UNIDAD_TIEMPO]        NVARCHAR (10)  CONSTRAINT [DF_C_PUESTO_EXPERIENCIA_CL_UNIDAD_TIEMPO] DEFAULT ('AÑOS') NOT NULL,
    [CL_NIVEL_REQUERIDO]      NVARCHAR (10)  CONSTRAINT [DF_C_PUESTO_EXPERIENCIA_CL_NIVEL_REQUERIDO] DEFAULT ('DESEADA') NOT NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_C_PUESTO_EXPERIENCIA] PRIMARY KEY CLUSTERED ([ID_PUESTO_EXPERIENCIA] ASC),
    CONSTRAINT [FK_C_PUESTO_EXPERIENCIA_C_AREA_INTERES] FOREIGN KEY ([ID_AREA_INTERES]) REFERENCES [ADM].[C_AREA_INTERES] ([ID_AREA_INTERES])
);

