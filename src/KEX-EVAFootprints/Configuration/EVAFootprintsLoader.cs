using System;
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
                public NumericParser<Boolean> allowFootprints
                {
                    get { return FootprintSpawner.FootprintsAllowed.Contains(generatedBody.celestialBody.transform.name); }
                    set { if (value) { FootprintSpawner.FootprintsAllowed.Add(generatedBody.celestialBody.transform.name); } }
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