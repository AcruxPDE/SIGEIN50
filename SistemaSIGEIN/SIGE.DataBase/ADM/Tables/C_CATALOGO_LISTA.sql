﻿CREATE TABLE [ADM].[C_CATALOGO_LISTA] (
    [ID_CATALOGO_LISTA]       INT             IDENTITY (1, 1) NOT NULL,
    [NB_CATALOGO_LISTA]       NVARCHAR (100)  NOT NULL,
    [DS_CATALOGO_LISTA]       NVARCHAR (1000) NULL,
    [ID_CATALOGO_TIPO]        INT             NOT NULL,
    [FG_SISTEMA]              BIT             CONSTRAINT [DF_C_CATALOGO_LISTA_FG_SISTEMA] DEFAULT ((0)) NULL,
    [FE_CREACION]             DATETIME        NOT NULL,
    [FE_MODIFICACION]         DATETIME        NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)   NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)   NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)   NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)   NULL,
    CONSTRAINT [PK_C_CATALOGO_LISTA] PRIMARY KEY CLUSTERED ([ID_CATALOGO_LISTA] ASC),
    CONSTRAINT [UC_DS_CATALOGO_LISTA] UNIQUE NONCLUSTERED ([DS_CATALOGO_LISTA] ASC),
    CONSTRAINT [UC_NB_CATALOGO_LISTA] UNIQUE NONCLUSTERED ([NB_CATALOGO_LISTA] ASC)
);
