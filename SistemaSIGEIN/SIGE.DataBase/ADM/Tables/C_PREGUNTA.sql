﻿CREATE TABLE [ADM].[C_PREGUNTA] (
    [ID_PREGUNTA]             INT            IDENTITY (1, 1) NOT NULL,
    [CL_PREGUNTA]             NVARCHAR (32)  NOT NULL,
    [NB_PREGUNTA]             NVARCHAR (300) NOT NULL,
    [DS_PREGUNTA]             NVARCHAR (300) NULL,
    [CL_TIPO_PREGUNTA]        NVARCHAR (10)  CONSTRAINT [DF_C_PREGUNTA_CL_TIPO_PREGUNTA] DEFAULT ('OPCION') NOT NULL,
    [NO_VALOR]                DECIMAL (8, 3) NOT NULL,
    [FG_REQUERIDO]            BIT            CONSTRAINT [DF_C_PREGUNTA_FG_REQUERIDO] DEFAULT ((1)) NOT NULL,
    [ID_CUESTIONARIO]         INT            NULL,
    [FG_ACTIVO]               BIT            CONSTRAINT [DF_C_PREGUNTA_FG_ACTIVO] DEFAULT ((1)) NOT NULL,
    [ID_COMPETENCIA]          INT            NULL,
    [ID_BITACORA]             INT            NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_C_PREGUNTA] PRIMARY KEY CLUSTERED ([ID_PREGUNTA] ASC)
);
