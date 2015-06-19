Imports System.Web
Imports System.Web.Services

''' <summary>
''' We will use a handler for now to switch code on Server side to all the Data
''' lookups and connect to the FDA Api
''' </summary>
''' <remarks></remarks>
Public Class QuickHandler
    Implements System.Web.IHttpHandler

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return True
        End Get
    End Property

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

        ServiceWrapper.ProcessRequest(context)

    End Sub


End Class