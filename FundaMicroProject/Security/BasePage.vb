Imports System.Web.UI

Namespace Security
    ''' <summary>
    ''' Clase base que intercepta las peticiones y actúa como Middleware de autenticación
    ''' </summary>
    Public Class BasePage
        Inherits Page

        Protected Overrides Sub OnInit(ByVal e As EventArgs)
            MyBase.OnInit(e)
            If Session("Usuario") Is Nothing Then
                Response.Redirect("~/Login.aspx", True)
            End If
        End Sub
        Protected ReadOnly Property UsuarioSesion As String
            Get
                Return If(Session("Usuario") IsNot Nothing, Session("Usuario").ToString(), String.Empty)
            End Get
        End Property

        Protected ReadOnly Property NombreCompletoSesion As String
            Get
                Return If(Session("NombreCompleto") IsNot Nothing, Session("NombreCompleto").ToString(), String.Empty)
            End Get
        End Property
    End Class
End Namespace