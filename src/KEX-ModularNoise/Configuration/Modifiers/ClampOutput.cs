using Kopernicus;
using LibNoise;
using System;
using System.Collections.Generic;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class ClampOutput : NoiseLoader<LibNoise.Modifiers.ClampOutput>
            {
                [ParserTarget("lower")]
                public NumericParser<Double> lower;

                [ParserTarget("upper")]
                public NumericParser<Double> upper;

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.ClampOutput(sourceModule.noise);
                    lower = noise.LowerBound;
                    upper = noise.UpperBound;
                }

                public override void PostApply(ConfigNode node)
                {
                    noise.SetBounds(lower, upper);
                }
            }
        }
    }
}