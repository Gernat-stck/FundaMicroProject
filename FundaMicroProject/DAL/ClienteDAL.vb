Imports System.Data
Imports System.Data.SqlClient
Imports FundaMicroProject.Data
Imports FundaMicroProject.Models

Namespace DAL
    Public Class ClienteDAL
        Public Function ListarTodos() As DataTable
            Dim dt As New DataTable()
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "SELECT IdCliente, DocumentoIdentidad, Nombres, Apellidos, Telefono, Email, Direccion FROM Clientes ORDER BY IdCliente DESC"
                Using cmd As New SqlCommand(query, con)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        Public Function ObtenerPorId(ByVal idCliente As Integer) As Cliente
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "SELECT * FROM Clientes WHERE IdCliente = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", idCliente)
                    con.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Return New Cliente With {
                                .idCliente = Convert.ToInt32(dr("IdCliente")),
                                .DocumentoIdentidad = dr("DocumentoIdentidad").ToString(),
                                .Nombres = dr("Nombres").ToString(),
                                .Apellidos = dr("Apellidos").ToString(),
                                .Telefono = If(IsDBNull(dr("Telefono")), String.Empty, dr("Telefono").ToString()),
                                .Email = If(IsDBNull(dr("Email")), String.Empty, dr("Email").ToString()),
                                .Direccion = If(IsDBNull(dr("Direccion")), String.Empty, dr("Direccion").ToString())
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function
    End Class
End Namespace