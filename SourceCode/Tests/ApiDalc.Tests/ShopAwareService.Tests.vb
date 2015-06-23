#Region " Imports "

Imports NUnit.Framework

#End Region

<TestFixture()>
Public Class ShopAwareService

    <TestCase()>
    Public Sub GetSearchSummary_ValidInput_ReturnsSummary()

        Dim mockRestClient = New Mocks.MockRestClient("GetSearchSummary.json")
        Dim service = New ApiDalc.ShopAwareService(mockRestClient)
        Dim result = service.GetSearchSummary("TEST", "TN")

        Assert.IsNotNull(result)

    End Sub

    <TestCase()>
    Public Sub GetSearchResult_ValidInput_ReturnFoundItems()

        Dim mockRestClient = New Mocks.MockRestClient("GetSearchResult.json")
        Dim service = New ApiDalc.ShopAwareService(mockRestClient)
        Dim result = service.GetSearchResult("nuts", "TN")

        Assert.IsNotNull(result)

    End Sub

End Class