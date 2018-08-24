﻿CREATE TABLE [ADM].[C_MUNICIPIO] (
    [ID_MUNICIPIO]            INT            IDENTITY (1, 1) NOT NULL,
    [CL_PAIS]                 NVARCHAR (10)  CONSTRAINT [DF_C_MUNICIPIO_CL_PAIS] DEFAULT ('MEX') NOT NULL,
    [CL_ESTADO]               NVARCHAR (10)  NOT NULL,
    [CL_MUNICIPIO]            NVARCHAR (10)  NOT NULL,
    [NB_MUNICIPIO]            NVARCHAR (100) NOT NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_C_MUNICIPIO] PRIMARY KEY CLUSTERED ([ID_MUNICIPIO] ASC)
);
