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
            public class BiasOutput : NoiseLoader<LibNoise.Modifiers.BiasOutput>
            {
                [ParserTarget("bias")]
                public NumericParser<Double> bias
                {
                    get { return noise.Bias; }
                    set { noise.Bias = value; }
                }
                
                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.BiasOutput(sourceModule.noise, 0);
                }
            }
        }
    }
}