Imports System.Net
Imports System.Web.Http
Imports ApiDalc.DataObjects
Imports ApiDalc

Namespace Api

    <RoutePrefix("Api/ShopAware")>
    Public Class ShopAwareApiController
        Inherits ApiController

        Private Property CacheExpirationDays As Integer = 7

        <HttpGet>
        <Route("QuickSearch/{product}/{region}")>
        Public Function QuickSearch(product As String, region As String) As SearchSummary

            Dim cacheKey As String = String.Concat(product.ToLower, region.ToUpper, "QuickSearch")

            ' attempt to load the object from the cache
            Dim result As SearchSummary = HttpRuntime.Cache(cacheKey)

            If result Is Nothing Then

                Dim wrapper As New ShopAwareService
                result = wrapper.GetSearchSummary(product, region)

                ' add the object to the cache
                HttpRuntime.Cache.Insert(cacheKey, result, Nothing, DateTime.UtcNow.AddDays(CacheExpirationDays), TimeSpan.Zero)

            End If

            Return result

        End Function

        <HttpGet>
        <Route("FDAResults/{product}/{region}")>
        Public Function FdaResults(product As String, region As String) As FDAResult

            Dim cacheKey As String = String.Concat(product.ToLower, region.ToUpper, "FdaResults")

            ' attempt to load the object from the cache
            Dim result As FDAResult = HttpRuntime.Cache(cacheKey)

            If result Is Nothing Then

                If region.ToLower.Trim = "all" Then
                    region = String.Empty
                End If

                Dim wrapper As New ShopAwareService

                result = wrapper.GetFDAResult(product, region)

                ' add the object to the cache
                HttpRuntime.Cache.Insert(cacheKey, result, Nothing, DateTime.UtcNow.AddDays(CacheExpirationDays), TimeSpan.Zero)

            End If

            Return result

        End Function

    End Class

End Namespace