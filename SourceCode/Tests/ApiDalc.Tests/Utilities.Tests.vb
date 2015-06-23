#Region " Imports "

Imports Newtonsoft.Json.Linq
Imports NUnit.Framework

#End Region

<TestFixture()>
Public Class Utilities

    <TestCase()>
    Public Sub AddBackSlash_PathIsNothing_ReturnNothing()

        Dim resultNullParameter = ApiDalc.Utilities.AddBackslash(Nothing)

        Assert.AreEqual(Nothing, resultNullParameter)

    End Sub

    <TestCase()>
    Public Sub AddBackSlash_PathIsEmpty_ReturnBackslash()

        Dim resultEmptyParameter = ApiDalc.Utilities.AddBackslash(String.Empty)

        Assert.AreEqual("\", resultEmptyParameter)

    End Sub

    <TestCase()>
    Public Sub AddBackSlash_PathWithOutBackslash_ReturnPathWithBackslash()

        Dim exceptedNonNullValue = "c:\folder\"
        Dim resultParameterWithoutBackslash = ApiDalc.Utilities.AddBackslash("c:\folder")

        Assert.AreEqual(exceptedNonNullValue, resultParameterWithoutBackslash)

    End Sub

    <TestCase()>
    Public Sub AddBackSlash_PathWithBackslash_ReturnPathWithBackslash()

        Dim exceptedNonNullValue = "c:\folder\"
        Dim resultParameterWithBackslash = ApiDalc.Utilities.AddBackslash("c:\folder\")

        Assert.AreEqual(exceptedNonNullValue, resultParameterWithBackslash)

    End Sub

    <TestCase()>
    Public Sub AddForwardSlash_PathIsNothing_ReturnNothing()

        Dim resultNullParameter = ApiDalc.Utilities.AddForwardSlash(Nothing)

        Assert.AreEqual(Nothing, resultNullParameter)

    End Sub

    <TestCase()>
    Public Sub AddForwardSlash_PathIsEmpty_ReturnForwardslash()

        Dim resultEmptyParameter = ApiDalc.Utilities.AddForwardSlash(String.Empty)

        Assert.AreEqual("/", resultEmptyParameter)

    End Sub

    <TestCase()>
    Public Sub AddForwardkSlash_PathWithOutForwardslash_ReturnPathWithForwardslash()

        Dim exceptedValue = "http://localhost/"
        Dim resultParameterWithoutForwardslash = ApiDalc.Utilities.AddForwardSlash("http://localhost")

        Assert.AreEqual(exceptedValue, resultParameterWithoutForwardslash)

    End Sub

    <TestCase()>
    Public Sub AddForwardSlash_PathWithForwardslash_ReturnPathWithBackslash()

        Dim exceptedValue = "http://localhost/"
        Dim resultParameterWithForwardslash = ApiDalc.Utilities.AddForwardSlash("http://localhost/")

        Assert.AreEqual(exceptedValue, resultParameterWithForwardslash)

    End Sub

    <TestCase()>
    Public Sub IsJTokenValid_TokenIsNothing_ReturnNotValid()

        Dim isValid = ApiDalc.Utilities.IsJTokenValid(Nothing)

        Assert.IsFalse(isValid)

    End Sub

    <TestCase()>
    Public Sub IsJTokenValid_TokenIsValid_ReturnIsValid()

        Dim json = "{""color"":""yellow"",""size"":""medium""}"
        Dim value = JToken.Parse(json)
        Dim isValid = ApiDalc.Utilities.IsJTokenValid(value)

        Assert.IsTrue(isValid)

    End Sub

    <TestCase()>
    Public Sub GetEnumDescription_EnumIsNothing_ReturnEmptyString()

        Dim result = ApiDalc.GetEnumDescription(Nothing)

        Assert.IsTrue(result.Length = 0)

    End Sub

    <TestCase()>
    Public Sub GetEnumDescription_EnumWithDescription_ReturnDescription()

        Dim expectedValue = "Dangerous or defective products that predictably could cause serious health problems or death"
        Dim result = ApiDalc.GetEnumDescription(ClassificationTest.Class1)

        Assert.AreEqual(expectedValue, result)

    End Sub

    <TestCase()>
    Public Sub GetEnumDescription_EnumWithOutDescription_ReturnEnumName()

        Dim expectedValue = "Class2"
        Dim result = ApiDalc.GetEnumDescription(ClassificationTest.Class2)

        Assert.AreEqual(expectedValue, result)

    End Sub

    <TestCase()>
    Public Sub GetEnumDefaultValue_EnumIsNothing_ReturnEmptyString()

        Dim result = ApiDalc.GetEnumDefaultValue(Nothing)

        Assert.IsTrue(result.Length = 0)

    End Sub

    <TestCase()>
    Public Sub GetEnumDefaultValue_EnumWithDefaultValue_ReturnCorrectValue()

        Dim expectedValue = "1"
        Dim result = ApiDalc.GetEnumDefaultValue(ClassificationTest.Class1)

        Assert.AreEqual(expectedValue, result)

    End Sub

    <TestCase()>
    Public Sub GetEnumDefaultValue_EnumWithOutDefaultValue_TheEnumEndIsReturned()

        Dim expectedValue = "Class2"
        Dim result = ApiDalc.GetEnumDefaultValue(ClassificationTest.Class2)

        Assert.AreEqual(expectedValue, result)

    End Sub

End Class

