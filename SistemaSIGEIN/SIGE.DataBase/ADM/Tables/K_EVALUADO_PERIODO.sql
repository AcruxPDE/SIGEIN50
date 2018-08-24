﻿CREATE TABLE [ADM].[K_EVALUADO_PERIODO] (
    [ID_EVALUADOR_PERIODO]    INT             IDENTITY (1, 1) NOT NULL,
    [ID_PERIODO]              INT             NOT NULL,
    [ID_EMPLEADO]             INT             NOT NULL,
    [ID_PUESTO]               INT             CONSTRAINT [DF_K_EVALUADO_PERIODO_ID_PUESTO] DEFAULT ((1)) NOT NULL,
    [FG_PUESTO_ACTUAL]        INT             NOT NULL,
    [NO_CONSUMO_SUP]          INT             NOT NULL,
    [MN_CUOTA_BASE]           DECIMAL (13, 2) CONSTRAINT [DF_K_EVALUADO_PERIODO_MN_CUOTA_BASE] DEFAULT ((0)) NOT NULL,
    [MN_CUOTA_CONSUMO]        DECIMAL (13, 2) CONSTRAINT [DF_K_EVALUADO_PERIODO_MN_CUOTA_CONSUMO] DEFAULT ((0)) NOT NULL,
    [MN_CUOTA_ADICIONAL]      DECIMAL (13, 2) CONSTRAINT [DF_K_EVALUADO_PERIODO_MN_CUOTA_ADICIONAL] DEFAULT ((0)) NOT NULL,
    [FE_CREACION]             DATETIME        NOT NULL,
    [FE_MODIFICACION]         DATETIME        NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)   NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)   NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)   NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)   NULL,
    CONSTRAINT [PK_K_EVALUADO_PERIODO] PRIMARY KEY CLUSTERED ([ID_EVALUADOR_PERIODO] ASC)
);
