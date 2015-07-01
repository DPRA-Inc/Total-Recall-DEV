Imports Newtonsoft.Json

<Serializable>
Public Class FeatureObject

    Public Property Type As String
    Public Property Properties As FeaturePropertyObject
    Public Property Teometery As GeometryObject

End Class

<Serializable>
Public Class FeaturePropertyObject

    Public Property GEO_ID As String
    Public Property STATE As String
    Public Property NAME As String
    Public Property LSAD As String
    Public Property CENSUSAREA As Double


End Class

<Serializable>
Public Class GeometryObject
    Public Property type As String
    Public Property coordinates As List(Of LatLon)
End Class

<Serializable>
Public Class LatLon
    Public Property loc As List(Of Double)
End Class