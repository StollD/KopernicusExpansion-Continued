using Kopernicus;
using Kopernicus.Configuration;
using KopernicusExpansion.CometTail.Effects;
using System.Collections.Generic;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;
using Kopernicus.Configuration.Parsing;

namespace KopernicusExpansion
{
    namespace CometTail
    {
        namespace Configuration
        {
            [ParserTargetExternal("Body", "CometTails", "Kopernicus")]
            public class CometTailsLoader : BaseLoader, IParserEventSubscriber
            {
                [ParserTargetCollection("self", NameSignificance = NameSignificance.None)]
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
