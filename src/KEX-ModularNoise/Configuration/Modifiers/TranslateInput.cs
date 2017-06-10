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
            public class TranslateInput : NoiseLoader<LibNoise.Modifiers.TranslateInput>
            {
                [ParserTarget("x")]
                public NumericParser<Double> x
                {
                    get { return noise.X; }
                    set { noise.X = value; }
                }

                [ParserTarget("y")]
                public NumericParser<Double> y
                {
                    get { return noise.Y; }
                    set { noise.Y = value; }
                }

                [ParserTarget("z")]
                public NumericParser<Double> z
                {
                    get { return noise.Z; }
                    set { noise.Z = value; }
                }

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.TranslateInput(sourceModule.noise, 0, 0 ,0);
                }
            }
        }
    }
}