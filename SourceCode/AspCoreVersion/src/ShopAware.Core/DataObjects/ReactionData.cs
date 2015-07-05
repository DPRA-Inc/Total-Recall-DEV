using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ShopAware.Core.Enumerations;

namespace ShopAware.Core
{
    namespace DataObjects
    {
        /// <summary>
        ///     Reaction Data
        /// </summary>
        /// <remarks></remarks>
        public class ReactionData
        {
            #region Properties

            public string ReactionMedDrapt { get; set; }

            public string ReactionMeddraversionPt { get; set; }

            public ReactionOutcome ReactionOutcome { get; set; }

            #endregion

            #region Public Methods

            /// <summary>
            ///     Convert JSON Data
            /// </summary>
            /// <param name="jToken">Token</param>
            /// <returns>List of Reaction Data</returns>
            /// <remarks></remarks>
            public static List<ReactionData> ConvertJsonData(JToken jToken)
            {
                var data = new List<ReactionData>();

                if (Utilities.IsJTokenValid(jToken))
                {
                    foreach (var reaction in jToken)
                    {
                        var obj = new ReactionData();

                        obj.ReactionMedDrapt = Utilities.GetJTokenString(reaction, "reactionmeddrapt");
                        obj.ReactionMeddraversionPt = Utilities.GetJTokenString(reaction, "reactionmeddraversionpt");

                        var reactionOutcome = (int) obj.ReactionOutcome;

                        int.TryParse((Utilities.GetJTokenString(reaction, "reactionoutcome")), out reactionOutcome);
                        obj.ReactionOutcome = (ReactionOutcome) reactionOutcome;

                        data.Add(obj);
                    }
                }

                return data;
            }

            #endregion
        }
    }
}