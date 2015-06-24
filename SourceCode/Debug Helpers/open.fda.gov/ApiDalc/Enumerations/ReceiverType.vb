Imports System.ComponentModel

Namespace Enumerations

    ''' <summary>
    ''' The name of the organization receiving the report.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ReceiverType

        <Description("Pharmaceutical Company")>
        PharmaceuticalCompany = 1

        <Description("Regulatory Authority")>
        RegulatoryAuthority = 2

        <Description("Health Professional")>
        HealthProfessional = 3

        <Description("Regional Pharmacovigilance Center")>
        RegionalPharmacovigilanceCenter = 4

        <Description("WHO Collaborating Center for International Drug Monitoring")>
        WHO = 5 'for International Drug Monitoring

        Other = 6

    End Enum

End Namespace