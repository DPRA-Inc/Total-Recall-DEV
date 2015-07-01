Imports System.ComponentModel

Namespace Enumerations

    ''' <summary>
    ''' An encoded value for the category of individual submitting the report
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum PrimarySourceQualification

        Physician = 1

        Pharmacist = 2

        <Description("Other Health Professional")>
        OtherHealthProfessional = 3

        Lawyer = 4

        <Description("Consumer or non-health professional")>
        Consumer = 5

    End Enum

End Namespace