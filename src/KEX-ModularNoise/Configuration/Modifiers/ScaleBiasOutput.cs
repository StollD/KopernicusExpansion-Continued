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
            public class ScaleBiasOutput : NoiseLoader<LibNoise.Modifiers.ScaleBiasOutput>
            {
                [ParserTarget("bias")]
                public NumericParser<Double> bias
                {
                    get { return noise.Bias; }
                    set { noise.Bias = value; }
                }

                [ParserTarget("scale")]
                public NumericParser<Double> scale
                {
                    get { return noise.Scale; }
                    set { noise.Scale = value; }
                }

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.ScaleBiasOutput(sourceModule.noise);
                }
            }
        }
    }
}