﻿CREATE TABLE [ADM].[C_EVALUADOR_EXTERNO] (
    [ID_EVALUADOR_EXTERNO]    INT            IDENTITY (1, 1) NOT NULL,
    [CL_EVALUADOR_EXTERNO]    NVARCHAR (32)  NOT NULL,
    [NB_EVALUADOR_EXTERNO]    NVARCHAR (100) NOT NULL,
    [DS_EVALUARDO_EXTERNO]    NVARCHAR (200) NULL,
    [FG_ACTIVO]               BIT            NOT NULL,
    [XML_CAMPOS_ADICIONALES]  XML            NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL
);
