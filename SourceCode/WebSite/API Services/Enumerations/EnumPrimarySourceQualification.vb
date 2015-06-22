#Region " Public Enumerations "

''' <summary>
''' An encoded value for the category of individual submitting the report
''' </summary>
''' <remarks></remarks>
Public Enum EnumPrimarySourceQualification

    Physician = 1
    Pharmacist = 2

    <System.ComponentModel.Description("Other Health Professional")>
    OtherHealthProfessional = 3

    Lawyer = 4

    <System.ComponentModel.Description("Consumer or non-health professional")>
    Consumer = 5

End Enum

#End Region
