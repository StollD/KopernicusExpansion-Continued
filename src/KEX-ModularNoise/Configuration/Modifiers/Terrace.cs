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
            public class Terrace : NoiseLoader<LibNoise.Modifiers.Terrace>
            {
                [ParserTarget("controlPoints", optional = false)]
                public NumericCollectionParser<Double> controlPoints
                {
                    get { return new NumericCollectionParser<Double>(noise.ControlPoints); }
                    set { noise.ControlPoints = value.value; }
                }

                [ParserTarget("invertTerraces")]
                public NumericParser<Boolean> invertTerraces
                {
                    get { return noise.InvertTerraces; }
                    set { noise.InvertTerraces = value; }
                }

                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Terrace(sourceModule.noise);
                }
            }
        }
    }
}