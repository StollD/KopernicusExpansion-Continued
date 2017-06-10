using Kopernicus;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Spheres : NoiseLoader<LibNoise.Spheres>
            {
                [ParserTarget("frequency")]
                public NumericParser<Double> frequency
                {
                    get { return noise.Frequency; }
                    set { noise.Frequency = value; }
                }
            }
        }
    }
}