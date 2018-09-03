
CREATE VIEW [ADM].[VW_TIPO_ASENTAMIENTO]
AS
SELECT '01' AS CL_TIPO_ASENTAMIENTO, 'Aeropuerto' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '02' AS CL_TIPO_ASENTAMIENTO, 'Barrio' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '04' AS CL_TIPO_ASENTAMIENTO, 'Campamento' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '08' AS CL_TIPO_ASENTAMIENTO, 'Ciudad' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '09' AS CL_TIPO_ASENTAMIENTO, 'Colonia' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '10' AS CL_TIPO_ASENTAMIENTO, 'Condominio' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '11' AS CL_TIPO_ASENTAMIENTO, 'Congregación' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '12' AS CL_TIPO_ASENTAMIENTO, 'Conjunto habitacional' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '15' AS CL_TIPO_ASENTAMIENTO, 'Ejido' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '16' AS CL_TIPO_ASENTAMIENTO, 'Estación' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '17' AS CL_TIPO_ASENTAMIENTO, 'Equipamiento' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '18' AS CL_TIPO_ASENTAMIENTO, 'Exhacienda' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '20' AS CL_TIPO_ASENTAMIENTO, 'Finca' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '21' AS CL_TIPO_ASENTAMIENTO, 'Fraccionamiento' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '22' AS CL_TIPO_ASENTAMIENTO, 'Gran usuario' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '23' AS CL_TIPO_ASENTAMIENTO, 'Granja' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '24' AS CL_TIPO_ASENTAMIENTO, 'Hacienda' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '25' AS CL_TIPO_ASENTAMIENTO, 'Ingenio' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '26' AS CL_TIPO_ASENTAMIENTO, 'Parque industrial' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '27' AS CL_TIPO_ASENTAMIENTO, 'Poblado comunal' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '28' AS CL_TIPO_ASENTAMIENTO, 'Pueblo' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '29' AS CL_TIPO_ASENTAMIENTO, 'Ranchería' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '30' AS CL_TIPO_ASENTAMIENTO, 'Residencial' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '31' AS CL_TIPO_ASENTAMIENTO, 'Unidad habitacional' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '32' AS CL_TIPO_ASENTAMIENTO, 'Villa' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '33' AS CL_TIPO_ASENTAMIENTO, 'Zona comercial' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '34' AS CL_TIPO_ASENTAMIENTO, 'Zona federal' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '37' AS CL_TIPO_ASENTAMIENTO, 'Zona industrial' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '38' AS CL_TIPO_ASENTAMIENTO, 'Ampliación' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '39' AS CL_TIPO_ASENTAMIENTO, 'Club de golf' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '40' AS CL_TIPO_ASENTAMIENTO, 'Puerto' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '45' AS CL_TIPO_ASENTAMIENTO, 'Paraje' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '47' AS CL_TIPO_ASENTAMIENTO, 'Zona militar' AS NB_TIPO_ASENTAMIENTO UNION ALL
SELECT '48' AS CL_TIPO_ASENTAMIENTO, 'Rancho' AS NB_TIPO_ASENTAMIENTO


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[32] 4[30] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "C_COLONIA (ADM)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1155
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'VIEW', @level1name = N'VW_TIPO_ASENTAMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'ADM', @level1type = N'VIEW', @level1name = N'VW_TIPO_ASENTAMIENTO';

