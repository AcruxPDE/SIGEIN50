﻿CREATE TABLE [ADM].[C_EMPLEADO_IDIOMA] (
    [ID_EMPLEADO_IDIOMA]      INT             IDENTITY (1, 1) NOT NULL,
    [ID_EMPLEADO]             INT             NULL,
    [ID_CANDIDATO]            INT             NULL,
    [ID_IDIOMA]               INT             NOT NULL,
    [PR_LECTURA]              DECIMAL (5, 2)  NULL,
    [PR_ESCRITURA]            DECIMAL (5, 2)  NULL,
    [PR_CONVERSACIONAL]       DECIMAL (5, 2)  NULL,
    [CL_INSTITUCION]          INT             NULL,
    [NO_PUNTAJE]              DECIMAL (10, 3) NULL,
    [FE_CREACION]             DATETIME        NOT NULL,
    [FE_MODIFICACION]         DATETIME        NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)   NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)   NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)   NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)   NULL,
    CONSTRAINT [PK_C_EMPLEADO_IDIOMA] PRIMARY KEY CLUSTERED ([ID_EMPLEADO_IDIOMA] ASC),
    CONSTRAINT [FK_C_EMPLEADO_IDIOMA_C_CANDIDATO] FOREIGN KEY ([ID_CANDIDATO]) REFERENCES [ADM].[C_CANDIDATO] ([ID_CANDIDATO])
);

