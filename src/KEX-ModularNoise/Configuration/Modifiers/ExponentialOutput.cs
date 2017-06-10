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
            public class ExponentialOutput : NoiseLoader<LibNoise.Modifiers.ExponentialOutput>
            {
                [ParserTarget("exponent")]
                public NumericParser<Double> exponent
                {
                    get { return noise.Exponent; }
                    set { noise.Exponent = value; }
                }

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.ExponentialOutput(sourceModule.noise, 0);
                }
            }
        }
    }
}