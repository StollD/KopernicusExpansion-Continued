using Kopernicus;
using Kopernicus.Configuration.ModLoader;
using LibNoise;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class ModularNoise : ModLoader<PQSMod_ModularNoise>
            {
                [ParserTarget("deformity")]
                public NumericParser<Double> deformity
                {
                    get { return mod.deformity; }
                    set { mod.deformity = value; }
                }

                [ParserTarget("normalizeHeight")]
                public NumericParser<Boolean> normalizeHeight
                {
                    get { return mod.normalizeHeight; }
                    set { mod.normalizeHeight = value; }
                }

                [ParserTarget("Noise", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader noise
                {
                    set { mod.InitializeSerialization(); mod.noise = value.noise; }
                }
            }
        }
    }
}