namespace ShopAware.Core
{
    namespace Enumerations
    {
        /// <summary>
        ///     The name of the organization sending the report. Because FDA is providing these reports to you, it will always
        ///     appear as  2
        /// </summary>
        /// <remarks></remarks>
        public enum SenderType
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