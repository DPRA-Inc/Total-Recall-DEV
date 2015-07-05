namespace ShopAware.Core
{
    namespace Enumerations
    {
        /// <summary>
        ///     The name of the organization receiving the report.
        /// </summary>
        /// <remarks></remarks>
        public enum ReceiverType
        {
            PharmaceuticalCompany = 1,

            RegulatoryAuthority = 2,

            HealthProfessional = 3,

            RegionalPharmacovigilanceCenter = 4,

            WHO = 5, //for International Drug Monitoring

            Other = 6
        }
    }
}