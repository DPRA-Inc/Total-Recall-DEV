Public Class IconStyle

    'icon: {
    '                                       anchor: [0.5, 0.5],
    '                                       anchorXUnits: 'fraction',
    '                                       anchorYUnits: 'fraction',
    '                                       opacity: 0.85,
    '                                       src: 'img/mapIcon/' + mapIcon + '.png'
    '                                   }

    Public Property anchor() As Array = {0.5, 0.5}

    Public Property anchorXUnits As String = "fraction"

    Public Property anchorYUnits As String = "fraction"

    Public Property opacity As Double = 0.85D

    Public Property src As String = "img/mapIcon/{0}.png"

End Class
