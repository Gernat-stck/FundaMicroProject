Imports System.Data
Imports System.Data.SqlClient
Imports FundaMicroProject.DAL
Imports FundaMicroProject.Data
Imports FundaMicroProject.Models

Namespace BLL
    Public Class ClienteService
        Private ReadOnly _clienteDal As New ClienteDAL()

        Public Function ObtenerClientes() As DataTable
            Return _clienteDal.ListarTodos()
        End Function

        Public Function ObtenerCliente(ByVal idCliente As Integer) As Cliente
            Return _clienteDal.ObtenerPorId(idCliente)
        End Function

        Public Sub RegistrarCliente(ByVal cliente As Cliente, ByVal usuarioResponsable As String)
            ValidarCliente(cliente)

            Using con As SqlConnection = Conexion.ObtenerConexion()
                con.Open()
                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        ' 1. Insertar Cliente
                        Dim queryInsert As String = "INSERT INTO Clientes (DocumentoIdentidad, Nombres, Apellidos, Telefono, Email, Direccion) " &
                                                    "VALUES (@doc, @nom, @ape, @tel, @mail, @dir); SELECT SCOPE_IDENTITY();"

                        Dim nuevoId As Integer
                        Using cmd As New SqlCommand(queryInsert, con, tran)
                            cmd.Parameters.AddWithValue("@doc", cliente.DocumentoIdentidad)
                            cmd.Parameters.AddWithValue("@nom", cliente.Nombres)
                            cmd.Parameters.AddWithValue("@ape", cliente.Apellidos)
                            cmd.Parameters.AddWithValue("@tel", If(String.IsNullOrEmpty(cliente.Telefono), DBNull.Value, CObj(cliente.Telefono)))
                            cmd.Parameters.AddWithValue("@mail", If(String.IsNullOrEmpty(cliente.Email), DBNull.Value, CObj(cliente.Email)))
                            cmd.Parameters.AddWithValue("@dir", If(String.IsNullOrEmpty(cliente.Direccion), DBNull.Value, CObj(cliente.Direccion)))
                            nuevoId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' 2. Registrar en Bitácora
                        RegistrarBitacora(con, tran, "AGREGAR", nuevoId, $"Creación de cliente doc: {cliente.DocumentoIdentidad}", usuarioResponsable)

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Sub ActualizarCliente(ByVal cliente As Cliente, ByVal usuarioResponsable As String)
            ValidarCliente(cliente)

            Using con As SqlConnection = Conexion.ObtenerConexion()
                con.Open()
                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        Dim queryUpdate As String = "UPDATE Clientes SET DocumentoIdentidad=@doc, Nombres=@nom, Apellidos=@ape, Telefono=@tel, Email=@mail, Direccion=@dir WHERE IdCliente=@id"
                        Using cmd As New SqlCommand(queryUpdate, con, tran)
                            cmd.Parameters.AddWithValue("@doc", cliente.DocumentoIdentidad)
                            cmd.Parameters.AddWithValue("@nom", cliente.Nombres)
                            cmd.Parameters.AddWithValue("@ape", cliente.Apellidos)
                            cmd.Parameters.AddWithValue("@tel", If(String.IsNullOrEmpty(cliente.Telefono), DBNull.Value, CObj(cliente.Telefono)))
                            cmd.Parameters.AddWithValue("@mail", If(String.IsNullOrEmpty(cliente.Email), DBNull.Value, CObj(cliente.Email)))
                            cmd.Parameters.AddWithValue("@dir", If(String.IsNullOrEmpty(cliente.Direccion), DBNull.Value, CObj(cliente.Direccion)))
                            cmd.Parameters.AddWithValue("@id", cliente.IdCliente)
                            cmd.ExecuteNonQuery()
                        End Using

                        RegistrarBitacora(con, tran, "EDITAR", cliente.IdCliente, $"Actualización de datos para doc: {cliente.DocumentoIdentidad}", usuarioResponsable)

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Public Sub EliminarCliente(ByVal idCliente As Integer, ByVal usuarioResponsable As String)
            Using con As SqlConnection = Conexion.ObtenerConexion()
                con.Open()
                Using tran As SqlTransaction = con.BeginTransaction()
                    Try
                        Dim queryDelete As String = "DELETE FROM Clientes WHERE IdCliente=@id"
                        Using cmd As New SqlCommand(queryDelete, con, tran)
                            cmd.Parameters.AddWithValue("@id", idCliente)
                            cmd.ExecuteNonQuery()
                        End Using

                        RegistrarBitacora(con, tran, "ELIMINAR", idCliente, "Eliminación de cliente del sistema", usuarioResponsable)

                        tran.Commit()
                    Catch
                        tran.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Sub

        Private Sub RegistrarBitacora(ByVal con As SqlConnection, ByVal tran As SqlTransaction, ByVal accion As String, ByVal idCliente As Integer, ByVal detalle As String, ByVal usuario As String)
            Dim query As String = "INSERT INTO Bitacora (Accion, IdClienteAfectado, Detalle, UsuarioResponsable, FechaHora) " &
                                  "VALUES (@accion, @idCliente, @detalle, @usuario, GETDATE())"
            Using cmd As New SqlCommand(query, con, tran)
                cmd.Parameters.AddWithValue("@accion", accion)
                cmd.Parameters.AddWithValue("@idCliente", idCliente)
                cmd.Parameters.AddWithValue("@detalle", detalle)
                cmd.Parameters.AddWithValue("@usuario", usuario)
                cmd.ExecuteNonQuery()
            End Using
        End Sub

        Private Sub ValidarCliente(ByVal c As Cliente)
            If String.IsNullOrWhiteSpace(c.DocumentoIdentidad) OrElse String.IsNullOrWhiteSpace(c.Nombres) OrElse String.IsNullOrWhiteSpace(c.Apellidos) Then
                Throw New ArgumentException("Documento, Nombres y Apellidos son obligatorios.")
            End If
        End Sub
    End Class
End Namespace