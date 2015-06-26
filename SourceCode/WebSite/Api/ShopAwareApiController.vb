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
        <Route("ProductResults/{product}/{region}")>
        Public Function ProductResults(product As String, region As String) As SearchResult

            Dim cacheKey As String = String.Concat(product.ToLower, region.ToUpper, "QuickSearch")

            ' attempt to load the object from the cache
            Dim result As SearchResult = HttpRuntime.Cache(cacheKey)

            If result Is Nothing Then

                If region.ToLower.Trim = "all" Then
                    region = String.Empty
                End If

                Dim wrapper As New ShopAwareService
                result = wrapper.GetSearchResult(product, region)

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

        <HttpGet>
        <Route("GetStates")>
        Public Function GetStates() As List(Of StateData)

            Dim wrapper As New ShopAwareService
            Dim result As List(Of StateData) = wrapper.GetStates

            result.Insert(0, New StateData With {.name = "Nationwide", .abbreviation = "All"})

            Return result

        End Function

        <HttpGet>
        <Route("GetReportData/{product}/{region}")>
        Public Function GetReportData(product As String, region As String) As Dictionary(Of String, String)

            Dim service As New ShopAwareService
            Dim data As Dictionary(Of String, String) = service.GetReportDataItemByRegion(product, region)

            Return data

        End Function


    End Class

End Namespace