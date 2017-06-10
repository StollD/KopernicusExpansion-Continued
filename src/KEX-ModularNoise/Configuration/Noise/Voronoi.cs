using Kopernicus;
using LibNoise;
using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Voronoi : NoiseLoader<LibNoise.Voronoi>
            {
                [ParserTarget("displacement")]
                public NumericParser<Double> displacement
                {
                    get { return noise.Displacement; }
                    set { noise.Displacement = value; }
                }

                [ParserTarget("frequency")]
                public NumericParser<Double> frequency
                {
                    get { return noise.Frequency; }
                    set { noise.Frequency = value; }
                }

                [ParserTarget("distanceEnabled")]
                public NumericParser<Boolean> distanceEnabled
                {
                    get { return noise.DistanceEnabled; }
                    set { noise.DistanceEnabled = value; }
                }

                [ParserTarget("seed")]
                public NumericParser<Int32> seed
                {
                    get { return noise.Seed; }
                    set { noise.Seed = value; }
                }
            }
        }
    }
}