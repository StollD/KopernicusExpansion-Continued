using Kopernicus;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;

namespace KopernicusExpansion
{
    namespace EVAFootprints
    {
        namespace Configuration
        {
            [ParserTargetExternal("Body", "PQS", "Kopernicus")]
            public class KerbalEVAFootprints : BaseLoader, IParserEventSubscriber
            {
                [ParserTarget("allowFootprints")]
                public NumericParser<bool> allowFootprints
                {
                    set { if (value) { FootprintSpawner.FootprintsAllowed.Add(generatedBody.celestialBody); } }
                }
                
                // Apply event
                void IParserEventSubscriber.Apply(ConfigNode node) { }

                // Post apply event
                void IParserEventSubscriber.PostApply(ConfigNode node)
                {
                    ModLoader<PQSMod_FootprintRemover> mod = new ModLoader<PQSMod_FootprintRemover>();
                    mod.Create(generatedBody.pqsVersion);
                    mod.order = 0;
                    mod.enabled = true;
                }
            }
        }
    }
}