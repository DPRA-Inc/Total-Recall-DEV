namespace ShopAware.Core
{
    namespace Enumerations
    {
        /// <summary>
        ///     Reaction Outcomes
        /// </summary>
        /// <remarks></remarks>
        public enum ReactionOutcome
        {
            RecoveredResolved = 1,
            RecoveringResolving = 2,
            NotRecoveredNotResolved = 3,
            RecoveredResolvedWithSequelae = 4,
            Fatal = 5,
            Unknown = 6
        }
    }
}