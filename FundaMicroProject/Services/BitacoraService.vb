Imports System.Data.SqlClient
Imports FundaMicroProject.Data

Public Class BitacoraService
    Public Function ObtenerBitacora() As DataTable
        Dim dt As New DataTable()
        Using con As SqlConnection = Conexion.ObtenerConexion()
            Dim query As String = "SELECT TOP 50 IdBitacora, Accion, IdClienteAfectado, Detalle, UsuarioResponsable, CONVERT(VARCHAR(19), FechaHora, 120) AS FechaHora FROM Bitacora ORDER BY IdBitacora DESC"
            Using cmd As New SqlCommand(query, con)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function
End Class
