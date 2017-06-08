using Kopernicus;
using Kopernicus.Configuration;
using KopernicusExpansion.CometTail.Effects;
using System.Collections.Generic;

namespace KopernicusExpansion
{
    namespace CometTail
    {
        namespace Configuration
        {
            [ParserTargetExternal("Body", "CometTails")]
            public class CometTailsLoader : BaseLoader, IParserEventSubscriber
            {
                [ParserTargetCollection("self", optional = true, nameSignificance = NameSignificance.None)]
                public List<CometTailLoader> tails = new List<CometTailLoader>();

                // Apply Event
                void IParserEventSubscriber.Apply(ConfigNode node) { }

                // Post Apply Event
                void IParserEventSubscriber.PostApply(ConfigNode node)
                {
                    foreach (CometTailLoader tail in tails)
                    {
                        Tail cometTail = tail.tail;
                        Tail.AddCometTail(generatedBody, cometTail);
                        Kopernicus.Logger.Active.Log("Added CometTail to body " + generatedBody.name);
                    }
                }
            }
        }
    }
}
