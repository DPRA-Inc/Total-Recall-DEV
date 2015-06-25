Imports System.Net
Imports System.Web.Http
Imports ApiDalc.DataObjects
Imports ApiDalc

Namespace Api

    <RoutePrefix("Api/ShopAware")>
    Public Class ShopAwareApiController
        Inherits ApiController

        <HttpGet>
        <Route("QuickSearch/{product}/{region}")>
        Public Function QuickSearch(product As String, region As String) As SearchSummary

            Dim wrapper As New ShopAwareService
            Dim result = wrapper.GetSearchSummary(product, region)

            Return result

        End Function

        <HttpGet>
        <Route("ProductResults/{product}/{region}")>
        Public Function ProductResults(product As String, region As String) As SearchResult

            Dim wrapper As New ShopAwareService
            Dim result = wrapper.GetSearchResult(product, region)

            Return result

        End Function

        <HttpGet>
        <Route("FDAResults/{product}/{region}")>
        Public Function FDAResults(product As String, region As String) As FDAResult

            Dim wrapper As New ShopAwareService
            Dim result = wrapper.GetFDAResult(product, region)

            Return result

        End Function

        <HttpGet>
        <Route("GetStates")>
        Public Function GetStates() As List(Of StateData)

            Dim wrapper As New ShopAwareService
            Dim result = New List(Of StateData)

            result.Add(New StateData With {.name = "Tennessee", .abbreviation = "TN"})
            result.Add(New StateData With {.name = "Florida", .abbreviation = "FL"})
            result.Add(New StateData With {.name = "Nationwide", .abbreviation = "All"})

            Return result

        End Function

        <HttpGet>
        <Route("GetReportData/{product}/{region}")>
        Public Function GetReportData(product As String, region As String) As FDAResult

            Return Nothing

        End Function


    End Class

End Namespace