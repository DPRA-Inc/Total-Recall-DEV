Imports System.Web.Http
Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
    End Sub

End Class