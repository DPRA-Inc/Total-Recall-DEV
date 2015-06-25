Imports System.Net.Http
Imports System.Web.Http

Public Module WebApiConfig

    Public Sub Register(config As HttpConfiguration)

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

    End Sub

End Module

