using ShopAware.Core.Attributes;

namespace ShopAware.Core.Enumerations
{
    /// <summary>
    ///     Reported role of the drug in the adverse event.
    /// </summary>
    /// <remarks></remarks>
    public enum DrugCharacterization
    {
        [Description("Suspect drug")] SuspectDrug = 1,

        [Description("Concomitant drug")] ConcomitantDrug = 2,

        [Description("Interacting drug")] InteractingDrug = 3
    }
}