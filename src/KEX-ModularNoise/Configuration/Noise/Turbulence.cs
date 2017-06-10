using Kopernicus;
using LibNoise;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Turbulence : NoiseLoader<LibNoise.Turbulence>
            {
                [ParserTarget("frequency")]
                public NumericParser<Double> frequency
                {
                    get { return noise.Frequency; }
                    set { noise.Frequency = value; }
                }

                [ParserTarget("roughness")]
                public NumericParser<Int32> roughness
                {
                    get { return noise.Roughness; }
                    set { noise.Roughness = Mathf.Clamp(value, 1, 30); }
                }

                [ParserTarget("power")]
                public NumericParser<Double> power
                {
                    get { return noise.Power; }
                    set { noise.Power = value; }
                }

                [ParserTarget("seed")]
                public NumericParser<Int32> seed
                {
                    get { return noise.Seed; }
                    set { noise.Seed = value; }
                }

                [PreApply]
                [ParserTargetCollection("self", nameSignificance = NameSignificance.Type)]
                public List<NoiseLoader> modules;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Turbulence(modules[0].noise);
                }
            }
        }
    }
}