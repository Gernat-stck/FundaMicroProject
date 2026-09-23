Imports System.Data.SqlClient
Imports FundaMicroProject.Data
Imports FundaMicroProject.Models
Namespace DAL
    Public Class UsuarioDAL
        '<-- Validacion de credenciales de usuario -->
        Public Function ValidarCredenciales(ByVal nombreUsuario As String, ByVal passwordHash As String) As Usuario
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "SELECT IdUsuario, Usuario, NombreCompleto, Estado " &
                                      "FROM Usuarios " &
                                      "WHERE Usuario = @Usuario AND PasswordHash = @PasswordHash AND Estado = 1"

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Usuario", nombreUsuario)
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash)

                    con.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New Usuario With {
                                .IdUsuario = Convert.ToInt32(reader("IdUsuario")),
                                .NombreUsuario = reader("Usuario").ToString(),
                                .NombreCompleto = reader("NombreCompleto").ToString(),
                                .Estado = Convert.ToBoolean(reader("Estado"))
                            }
                        End If
                    End Using
                End Using
            End Using

            Return Nothing
        End Function

        '<-- Listar todos los usuarios -->
        Public Function ListarTodos() As DataTable
            Dim dt As New DataTable()
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "SELECT IdUsuario, Usuario, NombreCompleto, Estado, CONVERT(VARCHAR(19), FechaCreacion, 120) AS FechaCreacion FROM Usuarios ORDER BY IdUsuario DESC"
                Using cmd As New SqlCommand(query, con)
                    Using da As New SqlDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using
            End Using
            Return dt
        End Function

        '<-- Obtener un usuario por su ID -->
        Public Function ObtenerPorId(ByVal idUsuario As Integer) As Usuario
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "SELECT IdUsuario, Usuario, NombreCompleto, Estado FROM Usuarios WHERE IdUsuario = @id"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@id", idUsuario)
                    con.Open()
                    Using dr As SqlDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            Return New Usuario With {
                                .IdUsuario = Convert.ToInt32(dr("IdUsuario")),
                                .NombreUsuario = dr("Usuario").ToString(),
                                .NombreCompleto = dr("NombreCompleto").ToString(),
                                .Estado = Convert.ToBoolean(dr("Estado"))
                            }
                        End If
                    End Using
                End Using
            End Using
            Return Nothing
        End Function

        '<-- Crear un nuevo usuario -->
        Public Sub Insertar(ByVal usuario As Usuario)
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String = "INSERT INTO Usuarios (Usuario, PasswordHash, NombreCompleto, Estado) VALUES (@Usuario, @PasswordHash, @NombreCompleto, @Estado)"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario)
                    cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash)
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto)
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        '<-- Actualizar un usuario existente -->
        Public Sub Actualizar(ByVal usuario As Usuario, ByVal cambiarPassword As Boolean)
            Using con As SqlConnection = Conexion.ObtenerConexion()
                Dim query As String
                If cambiarPassword Then
                    query = "UPDATE Usuarios SET Usuario = @Usuario, PasswordHash = @PasswordHash, NombreCompleto = @NombreCompleto, Estado = @Estado WHERE IdUsuario = @Id"
                Else
                    query = "UPDATE Usuarios SET Usuario = @Usuario, NombreCompleto = @NombreCompleto, Estado = @Estado WHERE IdUsuario = @Id"
                End If

                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario)
                    cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto)
                    cmd.Parameters.AddWithValue("@Estado", usuario.Estado)
                    cmd.Parameters.AddWithValue("@Id", usuario.IdUsuario)
                    If cambiarPassword Then
                        cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash)
                    End If
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub
    End Class
End Namespace