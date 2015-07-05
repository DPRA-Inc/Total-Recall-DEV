using ShopAware.Core.Attributes;

namespace ShopAware.Core
{
    namespace Enumerations
    {
        /// <summary>
        ///     Actions taken with the drug
        /// </summary>
        /// <remarks></remarks>
        public enum ActionDrug
        {
            [Description("Drug Withdrawn")] DrugWithdrawn = 1,

            [Description("Dose Reduced")] DoseReduced = 2,

            [Description("Dose Increased")] DoseIncreased = 3,

            [Description("Dose not changed")] DoseNotChanged = 4,

            [Description("Unknown")] Unknown = 5,

            [Description("Not Applicable")] NotApplicable = 6,
        }
    }
}