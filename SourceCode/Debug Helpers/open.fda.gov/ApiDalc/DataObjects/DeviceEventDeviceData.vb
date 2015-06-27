#Region " Imports "

Imports ApiDalc.DataObjects
Imports Newtonsoft.Json.Linq ' OpenSource

#End Region

Namespace DataObjects

    Public Class DeviceEventDeviceData

#Region " Properties "

        'Number identifying this particular device. 
        'For example, the first device object will have the value 1. This is an enumeration corresponding to the number of patients involved in an adverse event.
        Public Property DeviceSequenceNumber As String
        Public Property DeviceEventKey As String
        Public Property DateReceived As String

        'IDENTIFICATION

        'The trade or proprietary name of the suspect medical device as used in product labeling or in the catalog (e.g. Flo-Easy Catheter, Reliable Heart Pacemaker, etc.). 
        'If the suspect device is a reprocessed single-use device, this field will contain NA.
        Public Property BrandName As String
        'The generic or common name of the suspect medical device or a generally descriptive name (e.g. urological catheter, heart pacemaker, patient restraint, etc.).
        Public Property GenericName As String
        'Three-letter FDA Product Classification Code. Medical devices are classified under 21 CFR Parts 862-892. 
        'The assigned FDA Product Classification Code (procode) can be identified using the Product Classification Database.
        Public Property DeviceReportProductCode As String

        'Device model and catalog numbers

        'The exact model number found on the device label or accompanying packaging.
        Public Property ModelNumber As String
        'The exact number as it appears in the manufacturer’s catalog, device labeling, or accompanying packaging.
        Public Property CatalogNumber As String
        'If available, the lot number found on the label or packaging material.
        Public Property LotNumber As String
        'Any other identifier that might be used to identify the device. Expect wide variability in the use of this field. It is commonly empty, or marked NA, N/A, *, or UNK, if unknown or not applicable.
        Public Property OtherIdNumber As String

        'Age and expiration date

        'If available; this date is often be found on the device itself or printed on the accompanying packaging.
        Public Property ExpirationDateOfDevice As String
        'Age of the device or a best estimate, often including the unit of time used. Contents vary widely, but common patterns include:
        'nn YR or n.n YR = Device age nn or n.n years.
        'nn MO or n.n MO = Device age nn or n.n months.
        'nn DA or nn DA or nn DAY = Device age nn or n.n days.
        'UNK or UNKNOWN = Device age unknown.
        'DA = Documentation forthcoming.
        'NO INFO = Documentation forthcoming.* or 
        'empty if information not provided.
        Public Property DeviceAgeText As String

        'Evaluation by manufacturer

        'Whether the device is available for evaluation by the manufacturer, or whether the device was returned to the manufacturer.
        'Yes
        'No
        'Device was returned to manufacturerNo answer provided
        'I = Documentation forthcoming.May also be 
        'empty if no answer provided.
        Public Property DeviceAvailability As String
        'Date the device was returned to the manufacturer, if applicable.
        Public Property DateReturnedToManufacturer As String
        Public Property DeviceEvaluatedByManufacturer As String
        'Whether the suspect device was evaluated by the manufacturer.
        'Yes = An evaluation was made of the suspect or related medical device.
        'No = An evaluation of a returned suspect or related medical device was not conducted.
        'Device not returned to manufacturer = An evaluation could not be made because the device was not returned to, or made available to, the manufacturer.
        'No answer provided or 
        'empty = No answer was provided or this information was unavailable.

        'Use of device

        Public Property DeviceOperator As String
        'Whether a device was implanted or not. May be either marked N or left empty if this was not applicable.
        Public Property ImplantFlag As String
        'Whether an implanted device was removed from the patient, and if so, what kind of date was provided.Month and year provided only day defaults to 01 = Only a year and month were provided. Day was set to 01.Year provided only = Only a year was provided. Month was set to 01 (January) and day set to 01.No information at this time = Documentation forthcoming.Not available = Documentation forthcoming.Unknown = Documentation forthcoming.* = Documentation forthcoming.B = Documentation forthcoming.V = Documentation forthcoming.
        Public Property DateRemovedFlag As String

        'Manufacturer

        'Device manufacturer name.
        Public Property ManufacturerName As String
        'Device manufacturer address line 1.
        Public Property ManufacturerAddress1 As String
        'Device manufacturer address line 2.
        Public Property ManufacturerAddress2 As String
        Public Property ManufacturerCity As String
        Public Property ManufacturerState As String
        Public Property ManufacturerCountry As String
        'Device manufacturer 5-digit zip code.
        Public Property ManufacturerZipCode As String
        'Device manufacturer 4-digit zip code extension (zip+4 code).
        Public Property ManufacturerZipCodeExt As String
        'Device manufacturer postal code. May contain the zip code for addresses in the United States.
        Public Property ManufacturerPostalCode As String

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Convert JSON Data
        ''' </summary>
        ''' <param name="jsondata">JSON Object</param>
        ''' <returns>List(Of DeviceEventDeviceData)</returns>
        ''' <remarks></remarks>
        Public Shared Function CnvJsonDataToList(jsondata As JArray) As List(Of DeviceEventDeviceData)

            Dim result As New List(Of DeviceEventDeviceData)

            For Each obj In jsondata

                Dim tmp As New DeviceEventDeviceData

                With tmp

                    .DeviceSequenceNumber = obj("device_sequence_number")
                    .DeviceEventKey = obj("device_event_key")
                    .DateReceived = obj("date_received")

                    .BrandName = obj("brand_name")
                    .GenericName = obj("generic_name")
                    .DeviceReportProductCode = obj("device_report_product_code")

                    .ModelNumber = obj("model_number")
                    .CatalogNumber = obj("catalog_number")
                    .LotNumber = obj("device.")
                    .OtherIdNumber = obj("other_id_number")

                    .ExpirationDateOfDevice = obj("expiration_date_of_device")
                    .DeviceAgeText = obj("device_age_text")

                    .DeviceAvailability = obj("device_availability")
                    .DateReturnedToManufacturer = obj("date_returned_to_manufacturer")
                    .DeviceEvaluatedByManufacturer = obj("device_evaluated_by_manufacturer")

                    .DeviceOperator = obj("device_operator")
                    .ImplantFlag = obj("implant_flag")
                    .DateRemovedFlag = obj("date_removed_flag")

                    .ManufacturerName = obj("manufacturer_d_name")
                    .ManufacturerAddress1 = obj("manufacturer_d_address_1")
                    .ManufacturerAddress2 = obj("manufacturer_d_address_2")
                    .ManufacturerCity = obj("manufacturer_d_city")
                    .ManufacturerState = obj("manufacturer_d_state")
                    .ManufacturerCountry = obj("manufacturer_d_country")
                    .ManufacturerZipCode = obj("manufacturer_d_zip_code")
                    .ManufacturerZipCodeExt = obj("manufacturer_d_zip_code_ext")
                    .ManufacturerPostalCode = obj("manufacturer_d_postal_code")

                End With

                result.Add(tmp)

            Next

            Return result

        End Function

#End Region

    End Class

End Namespace
