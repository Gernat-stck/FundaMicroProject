Imports System.Configuration
Imports System.Data.SqlClient

Namespace Data
    Public Class Conexion
        Public Shared Function ObtenerConexion() As SqlConnection
            Dim cadenaConexion As String = ConfigurationManager.ConnectionStrings("ConexionDB").ConnectionString
            Return New SqlConnection(cadenaConexion)
        End Function
    End Class
End Namespace