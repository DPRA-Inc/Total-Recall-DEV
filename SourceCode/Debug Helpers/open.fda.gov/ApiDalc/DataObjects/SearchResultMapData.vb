''' <summary>
''' Search Result Map Data Object
''' </summary>
''' <remarks>1 to 1 relation with the state</remarks>
Public Class SearchResultMapData

#Region " Public Properties "

    ''' <summary>
    ''' State 2 character Abbr
    ''' </summary>
    ''' <value>string</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property State As String = "00"

    ''' <summary>
    ''' Tooltip property when placed on the map
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Tooltip As String = String.Empty

    ''' <summary>
    ''' Bitwise Icon Set 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IconSet As IconSet = 0

    ''' <summary>
    ''' Image to retrieve based on IconSet
    ''' </summary>
    ''' <value>string</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Image As String
        Get
            Return String.Format("{0}", CInt(Me.IconSet))
        End Get
    End Property

    ''' <summary>
    ''' Latitude Value of State
    ''' </summary>
    ''' <value>string</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Latitude As String = "0"

    ''' <summary>
    ''' Longitude Value of State
    ''' </summary>
    ''' <value>string</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Longitude As String = "0"

    ''' <summary>
    ''' Icon Style for OL3.angularjs
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property icon As IconStyle
        Get

            Dim ic As New IconStyle

            ic.src = String.Format(ic.src, CInt(Me.IconSet))

            Return ic

        End Get
    End Property

#End Region

End Class
