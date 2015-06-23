#Region " Imports "

Imports System.IO
Imports System.Resources
Imports System.Reflection

#End Region

Namespace Mocks

    Public Class MockRestClient
        Implements IRestClient

#Region " Member Variables "

        Private _sampleData As String

#End Region

#Region " Constructors "

        Public Sub New()
        End Sub

        Public Sub New(sampleDataFile As String)

            Dim asm = Assembly.GetExecutingAssembly()

            Using stream = asm.GetManifestResourceStream(String.Format("{0}.{1}", asm.GetName().Name, sampleDataFile))

                Using reader = New StreamReader(stream)
                    _sampleData = reader.ReadToEnd()
                End Using

            End Using

        End Sub

#End Region

#Region " IRestClient Implementation "

        Public Function Execute(url As String) As String Implements IRestClient.Execute
            Return _sampleData
        End Function

#End Region

    End Class

End Namespace
