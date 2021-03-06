﻿#Region " Imports "

Imports Newtonsoft.Json.Linq
Imports NUnit.Framework

#End Region

<TestFixture()>
Public Class OpenFda

    <TestCase()>
    Public Sub AddSearchFilter_DrugEvent_WithDrugName_ReturnsResult()

        'Dim mockRestClient = New Mocks.MockRestClient("AddSearchFilter_DrugEvent_Augmentin.json")
        Dim mockRestClient = New Mocks.MockRestClient()
        Dim drugName As String = "Augmentin"
        Dim service = New ApiDalc.OpenFda(mockRestClient)
        Dim result = service.AddSearchFilter(Enumerations.OpenFdaApiEndPoints.DrugEvent, Enumerations.FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}))

        'Debug.WriteLine(result)
        '(patient.drug.openfda.substance_name:Augmentin+patient.drug.openfda.brand_name:Augmentin+patient.drug.openfda.generic_name:Augmentin+patient.drug.medicinalproduct:Augmentin)
        'Assert.AreEqual(Nothing, result)
        Assert.AreNotEqual(Nothing, result)

    End Sub

    <TestCase()>
    Public Sub AddSearchFilter_DrugEvent_WithDrugName_ReturnsValidResult()

        'Dim mockRestClient = New Mocks.MockRestClient("AddSearchFilter_DrugEvent_Augmentin.json")
        Dim mockRestClient = New Mocks.MockRestClient()
        Dim drugName As String = "Augmentin"
        Dim service = New ApiDalc.OpenFda(mockRestClient)
        Dim result = service.AddSearchFilter(Enumerations.OpenFdaApiEndPoints.DrugEvent, Enumerations.FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}))

        'Debug.WriteLine(result)
        Dim mockData As String = "(patient.drug.openfda.substance_name:Augmentin+patient.drug.openfda.brand_name:Augmentin+patient.drug.openfda.generic_name:Augmentin+patient.drug.medicinalproduct:Augmentin)"

        'Assert.AreEqual(Nothing, result)
        Assert.AreEqual(mockData.ToLower, result.ToLower)

    End Sub

    <TestCase()>
    Public Sub BuildURL_DrugEvent_WithDrugName_ReturnsResult()

        'Dim mockRestClient = New Mocks.MockRestClient("AddSearchFilter_DrugEvent_Augmentin.json")
        Dim mockRestClient = New Mocks.MockRestClient()
        Dim drugName As String = "Augmentin"
        Dim service = New ApiDalc.OpenFda(mockRestClient)
        Dim result = service.AddSearchFilter(Enumerations.OpenFdaApiEndPoints.DrugEvent, Enumerations.FdaFilterTypes.DrugEventDrugName, New List(Of String)({drugName}))

        Dim url As String = service.BuildUrl(Enumerations.OpenFdaApiEndPoints.DrugEvent)
        Debug.WriteLine(url)
        Dim mockData As String = "(patient.drug.openfda.substance_name:Augmentin+patient.drug.openfda.brand_name:Augmentin+patient.drug.openfda.generic_name:Augmentin+patient.drug.medicinalproduct:Augmentin)"

        'Assert.AreEqual(Nothing, result)
        Assert.AreNotEqual(Nothing, url)

    End Sub

End Class
