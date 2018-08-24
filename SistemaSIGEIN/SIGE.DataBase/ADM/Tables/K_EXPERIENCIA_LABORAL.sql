﻿CREATE TABLE [ADM].[K_EXPERIENCIA_LABORAL] (
    [ID_EXPERIENCIA_LABORAL]    INT             IDENTITY (1, 1) NOT NULL,
    [ID_CANDIDATO]              INT             NULL,
    [ID_EMPLEADO]               INT             NULL,
    [NB_EMPRESA]                NVARCHAR (100)  NOT NULL,
    [DS_DOMICILIO]              NVARCHAR (500)  NULL,
    [NB_GIRO]                   NVARCHAR (50)   NULL,
    [NB_WEBSITE]                NVARCHAR (500)  NULL,
    [FE_INICIO]                 DATE            NOT NULL,
    [FE_FIN]                    DATE            NULL,
    [NB_PUESTO]                 NVARCHAR (100)  NULL,
    [NB_FUNCION]                NVARCHAR (100)  NULL,
    [DS_FUNCIONES]              NVARCHAR (1000) NULL,
    [MN_PRIMER_SUELDO]          DECIMAL (13, 2) NULL,
    [MN_ULTIMO_SUELDO]          DECIMAL (13, 2) NULL,
    [CL_TIPO_CONTRATO]          NVARCHAR (20)   NULL,
    [CL_TIPO_CONTRATO_OTRO]     NVARCHAR (50)   NULL,
    [NO_TELEFONO_CONTACTO]      NVARCHAR (20)   NULL,
    [CL_CORREO_ELECTRONICO]     NVARCHAR (200)  NULL,
    [NB_CONTACTO]               NVARCHAR (100)  NULL,
    [NB_PUESTO_CONTACTO]        NVARCHAR (100)  NULL,
    [CL_INFORMACION_CONFIRMADA] BIT             CONSTRAINT [DF_K_EXPERIENCIA_LABORAL_CL_INFORMACION_CONFIRMADA] DEFAULT ((0)) NOT NULL,
    [DS_COMENTARIOS]            NVARCHAR (1000) NULL,
    [FE_CREACION]               DATETIME        NOT NULL,
    [FE_MODIFICACION]           DATETIME        NULL,
    [CL_USUARIO_APP_CREA]       NVARCHAR (50)   NOT NULL,
    [CL_USUARIO_APP_MODIFICA]   NVARCHAR (50)   NULL,
    [NB_PROGRAMA_CREA]          NVARCHAR (50)   NOT NULL,
    [NB_PROGRAMA_MODIFICA]      NVARCHAR (50)   NULL,
    CONSTRAINT [PK_K_EXPERIENCIA_LABORAL] PRIMARY KEY CLUSTERED ([ID_EXPERIENCIA_LABORAL] ASC),
    CONSTRAINT [FK_K_EXPERIENCIA_LABORAL_C_CANDIDATO] FOREIGN KEY ([ID_CANDIDATO]) REFERENCES [ADM].[C_CANDIDATO] ([ID_CANDIDATO])
);
