﻿CREATE TABLE [IDP].[C_EMPLEADO_ESCOLARIDAD] (
    [ID_EMPLEADO_ESCOLARIDAD] INT           IDENTITY (1, 1) NOT NULL,
    [ID_EMPLEADO]             INT           NULL,
    [ID_CANDIDATO]            INT           NULL,
    [ID_ESCOLARIDAD]          INT           NOT NULL,
    [CL_INSTITUCION]          INT           NULL,
    [NB_INSTITUCION]          NVARCHAR (50) NULL,
    [FE_PERIODO_INICIO]       DATE          NULL,
    [FE_PERIODO_FIN]          DATE          NULL,
    [CL_ESTADO_ESCOLARIDAD]   NVARCHAR (10) CONSTRAINT [DF_C_EMPLEADO_ESCOLARIDAD_CL_ESTADO_ESCOLARIDAD] DEFAULT ('TERMINADO') NOT NULL,
    [FE_CREACION]             DATETIME      NOT NULL,
    [FE_MODIFICACION]         DATETIME      NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50) NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50) NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50) NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_C_EMPLEADO_ESCOLARIDAD] PRIMARY KEY CLUSTERED ([ID_EMPLEADO_ESCOLARIDAD] ASC),
    CONSTRAINT [FK_C_EMPLEADO_ESCOLARIDAD_C_CANDIDATO] FOREIGN KEY ([ID_CANDIDATO]) REFERENCES [ADM].[C_CANDIDATO] ([ID_CANDIDATO]),
    CONSTRAINT [FK_C_EMPLEADO_ESCOLARIDAD_C_ESCOLARIDAD] FOREIGN KEY ([ID_ESCOLARIDAD]) REFERENCES [ADM].[C_ESCOLARIDAD] ([ID_ESCOLARIDAD])
);
