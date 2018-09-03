﻿CREATE TABLE [ADM].[C_EMPLEADO_RELACIONADO] (
    [ID_EMPLEADO]             INT            NOT NULL,
    [ID_EMPLEADO_RELACIONADO] INT            NOT NULL,
    [CL_TIPO_RELACION]        NVARCHAR (20)  NOT NULL,
    [DS_EMPLEADO_RELACIONADO] NVARCHAR (500) NULL,
    [FE_CREACION]             DATETIME       NOT NULL,
    [FE_MODIFICACION]         DATETIME       NULL,
    [CL_USUARIO_APP_CREA]     NVARCHAR (50)  NOT NULL,
    [CL_USUARIO_APP_MODIFICA] NVARCHAR (50)  NULL,
    [NB_PROGRAMA_CREA]        NVARCHAR (50)  NOT NULL,
    [NB_PROGRAMA_MODIFICA]    NVARCHAR (50)  NULL,
    CONSTRAINT [FK_C_EMPLEADO_RELACIONADO_S_TIPO_RELACION_PUESTO] FOREIGN KEY ([CL_TIPO_RELACION]) REFERENCES [ADM].[S_TIPO_RELACION_PUESTO] ([CL_TIPO_RELACION])
);

