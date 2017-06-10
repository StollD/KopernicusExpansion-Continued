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
            public class RotateInput : NoiseLoader<LibNoise.Modifiers.RotateInput>
            {
                [ParserTarget("xAngle")]
                public NumericParser<Double> xAngle;

                [ParserTarget("yAngle")]
                public NumericParser<Double> yAngle;

                [ParserTarget("zAngle")]
                public NumericParser<Double> zAngle;

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.RotateInput(sourceModule.noise, 0, 0 ,0);
                }

                public override void PostApply(ConfigNode node)
                {
                    noise.SetAngles(xAngle, yAngle, zAngle);
                }
            }
        }
    }
}