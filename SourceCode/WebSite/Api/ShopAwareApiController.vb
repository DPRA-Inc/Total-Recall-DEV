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

        <HttpGet>
        <Route("Regions/GetStateJson/{selected}")>
        Public Function GetStateJson(selected As String) As String

            Dim path As String = HttpContext.Current.Server.MapPath("~/json/")

            Dim regions As New List(Of String)
            regions.AddRange(Split(selected, ","))

            Dim statePolys As New List(Of String)

            For Each state In regions
                Dim folder As String = String.Format("{0}{1}.geo.json.txt", path, state)
                Dim data As String() = IO.File.ReadAllLines(folder)

                statePolys.Add(data(1))

            Next

            Dim result As New List(Of String)
            result.Add("{""type"":""FeatureCollection"",""features"":[")
            result.Add(Join(statePolys.ToArray, ","))
            result.Add("]}")

            Return Join(result.ToArray, "")

        End Function


    End Class

End Namespace