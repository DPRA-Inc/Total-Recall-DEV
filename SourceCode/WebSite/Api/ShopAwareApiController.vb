Imports System.Net
Imports System.Web.Http

Namespace Api

    <RoutePrefix("Api/ShopAware")>
    Public Class ShopAwareApiController
        Inherits ApiController

        <HttpGet>
        <Route("GetItem")>
        Public Function GetItem() As IHttpActionResult

            Return Ok("test")

        End Function

    End Class

End Namespace