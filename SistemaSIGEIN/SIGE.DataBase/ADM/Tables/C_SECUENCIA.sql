﻿CREATE TABLE [ADM].[C_SECUENCIA] (
    [CL_SECUENCIA]            NVARCHAR (20) NOT NULL,
    [CL_PREFIJO]              NVARCHAR (5)  NULL,
    [NO_ULTIMO_VALOR]         INT           CONSTRAINT [DF_C_SECUENCIA_NO_ULTIMO_VALOR] DEFAULT ((0)) NOT NULL,
    [NO_VALOR_MAXIMO]         INT           CONSTRAINT [DF_C_SECUENCIA_NO_VALOR_MAXIMO] DEFAULT ((9999999)) NOT NULL,
    [CL_SUFIJO]               NVARCHAR (5)  NULL,
    [NO_DIGITOS]              TINYINT       CONSTRAINT [DF_C_SECUENCIA_NO_DIGITOS] DEFAULT ((5)) NOT NULL,
    [FE_CREACION]             DATETIME      NOT NULL,
    [FE_MODIFICACION]         DATETIME      NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50) NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50) NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50) NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50) NULL,
    CONSTRAINT [PK_C_SECUENCIA] PRIMARY KEY CLUSTERED ([CL_SECUENCIA] ASC)
);
