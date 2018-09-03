﻿CREATE TABLE [ADM].[C_CENTRO_OPTVO] (
    [ID_CENTRO_OPTVO]         UNIQUEIDENTIFIER NOT NULL,
    [CL_CLIENTE]              NVARCHAR (10)    NULL,
    [CL_CENTRO_OPTVO]         NVARCHAR (10)    NOT NULL,
    [NB_CENTRO_OPTVO]         NVARCHAR (100)   NOT NULL,
    [NB_CALLE]                NVARCHAR (100)   NULL,
    [NB_NO_EXTERIOR]          NVARCHAR (10)    NULL,
    [NB_NO_INTERIOR]          NVARCHAR (10)    NULL,
    [NB_COLONIA]              NVARCHAR (100)   NULL,
    [CL_ESTADO]               NVARCHAR (10)    NULL,
    [NB_ESTADO]               NVARCHAR (100)   NULL,
    [CL_MUNICIPIO]            NVARCHAR (10)    NULL,
    [NB_MUNICIPIO]            NVARCHAR (100)   NULL,
    [CL_CODIGO_POSTAL]        NVARCHAR (10)    NULL,
    [FE_CREACION]             DATETIME         NOT NULL,
    [FE_MODIFICACION]         DATETIME         NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)    NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)    NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)    NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)    NULL,
    CONSTRAINT [PK_C_CENTRO_OPTVO] PRIMARY KEY CLUSTERED ([ID_CENTRO_OPTVO] ASC)
);

