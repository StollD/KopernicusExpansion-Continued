using System;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;

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
                    FootprintRemover mod = new FootprintRemover();
                    mod.Create(generatedBody.pqsVersion);
                    mod.Order = 0;
                    mod.Enabled = true;
                }
            }
            
            [RequireConfigType(ConfigType.Node)]
            public class FootprintRemover : ModLoader<PQSMod_FootprintRemover> {}
        }
    }
}