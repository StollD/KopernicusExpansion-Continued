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
            public class ScaleOutput : NoiseLoader<LibNoise.Modifiers.ScaleOutput>
            {
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
                    noise = new LibNoise.Modifiers.ScaleOutput(sourceModule.noise, 0);
                }
            }
        }
    }
}