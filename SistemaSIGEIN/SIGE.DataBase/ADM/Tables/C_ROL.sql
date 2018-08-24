﻿CREATE TABLE [ADM].[C_ROL] (
    [ID_ROL]                  INT            IDENTITY (1, 1) NOT NULL,
    [CL_ROL]                  NVARCHAR (30)  NOT NULL,
    [NB_ROL]                  NVARCHAR (100) NOT NULL,
    [XML_AUTORIZACION]        XML            NULL,
    [FG_ACTIVO]               BIT            CONSTRAINT [DF_C_ROL_FG_ACTIVO] DEFAULT ((1)) NOT NULL,
    [FE_INACTIVO]             DATETIME       NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [PK_C_ROL] PRIMARY KEY CLUSTERED ([ID_ROL] ASC),
    CONSTRAINT [UC_CL_ROL] UNIQUE NONCLUSTERED ([CL_ROL] ASC),
    CONSTRAINT [UC_NB_ROL] UNIQUE NONCLUSTERED ([NB_ROL] ASC)
);
