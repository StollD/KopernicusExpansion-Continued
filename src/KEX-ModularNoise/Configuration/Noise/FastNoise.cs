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
            public class FastNoise : NoiseLoader<LibNoise.FastNoise>
            {
                [ParserTarget("frequency")]
                public NumericParser<Double> frequency
                {
                    get { return noise.Frequency; }
                    set { noise.Frequency = value; }
                }

                [ParserTarget("lacunarity")]
                public NumericParser<Double> lacunarity
                {
                    get { return noise.Lacunarity; }
                    set { noise.Lacunarity = value; }
                }
                
                [ParserTarget("quality")]
                public EnumParser<KopernicusNoiseQuality> quality
                {
                    get { return (KopernicusNoiseQuality)(int)noise.NoiseQuality; }
                    set { noise.NoiseQuality = (NoiseQuality)(int)value.value; }
                }

                [ParserTarget("octaves")]
                public NumericParser<Int32> octaves
                {
                    get { return noise.OctaveCount; }
                    set { noise.OctaveCount = Mathf.Clamp(value, 1, 30); }
                }

                [ParserTarget("persistence")]
                public NumericParser<Double> persistence
                {
                    get { return noise.Persistence; }
                    set { noise.Persistence = value; }
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