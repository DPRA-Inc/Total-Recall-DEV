Public Class IconStyle

    ''' <summary>
    ''' Anchor location of icon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property anchor() As Array = {0.5, 0.5}

    ''' <summary>
    ''' Anchor X units
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property anchorXUnits As String = "fraction"

    ''' <summary>
    ''' Anchor Y units
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property anchorYUnits As String = "fraction"

    ''' <summary>
    ''' Opacity of icon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property opacity As Double = 0.85D

    ''' <summary>
    ''' Image location
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property src As String = "img/mapIcon/{0}.png"

End Class
