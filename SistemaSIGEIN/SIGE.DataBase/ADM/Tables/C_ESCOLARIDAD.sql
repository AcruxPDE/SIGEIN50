﻿CREATE TABLE [ADM].[C_ESCOLARIDAD] (
    [ID_ESCOLARIDAD]          INT            IDENTITY (1, 1) NOT NULL,
    [CL_ESCOLARIDAD]          NVARCHAR (20)  NOT NULL,
    [NB_ESCOLARIDAD]          NVARCHAR (100) NOT NULL,
    [DS_ESCOLARIDAD]          NVARCHAR (200) NOT NULL,
    [CL_NIVEL_ESCOLARIDAD]    NVARCHAR (100) NULL,
    [CL_INSTITUCION]          INT            NULL,
    [FG_ACTIVO]               BIT            CONSTRAINT [DF_C_ESCOLARIDAD_FG_ACTIVO] DEFAULT ((1)) NOT NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_C_ESCOLARIDAD] PRIMARY KEY CLUSTERED ([ID_ESCOLARIDAD] ASC),
    UNIQUE NONCLUSTERED ([CL_ESCOLARIDAD] ASC),
    CONSTRAINT [uc_NB_ESCOLARIDAD] UNIQUE NONCLUSTERED ([NB_ESCOLARIDAD] ASC)
);

