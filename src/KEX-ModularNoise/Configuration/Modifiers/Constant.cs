using Kopernicus;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Constant : NoiseLoader<LibNoise.Modifiers.Constant>
            {
                [ParserTarget("value")]
                public NumericParser<Double> value
                {
                    get { return noise.Value; }
                    set { noise.Value = value; }
                }

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Constant(0);
                }
            }
        }
    }
}