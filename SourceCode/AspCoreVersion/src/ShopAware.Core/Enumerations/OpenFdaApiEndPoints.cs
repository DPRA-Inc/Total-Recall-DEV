using System.ComponentModel;
using ShopAware.Core.Attributes;

namespace ShopAware.Core
{
    namespace Enumerations
    {
        /// [summary]
        /// open.fda.gov API Endpoints
        /// [/summary]
        /// [remarks][/remarks]
        public enum OpenFdaApiEndPoints
        {
            [Description("Drug Event")] [DefaultValue("drug/event")] DrugEvent,

            [Description("Drug Label")] [DefaultValue("drug/label")] DrugLabel,

            [Description("Drug Recall")] [DefaultValue("drug/enforcement")] DrugRecall,

            [Description("Device Event")] [DefaultValue("device/event")] DeviceEvent,

            [Description("Device Recall")] [DefaultValue("device/enforcement")] DeviceRecall,

            [Description("Food Recall")] [DefaultValue("food/enforcement")] FoodRecall
        }
    }
}