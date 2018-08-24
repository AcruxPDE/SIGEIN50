CREATE SCHEMA [ADM]
    AUTHORIZATION [dbo];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esquema  donde  se encontrara todas las tablas y operaciones que sean parte de la configuracion  u otros procesos ', @level0type = N'SCHEMA', @level0name = N'ADM';

