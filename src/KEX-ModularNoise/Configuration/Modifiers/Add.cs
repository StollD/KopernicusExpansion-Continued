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
            public class Add : NoiseLoader<LibNoise.Modifiers.Add>
            {
                [PreApply]
                [ParserTarget("SourceA", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleA;

                [PreApply]
                [ParserTarget("SourceB", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleB;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Add(sourceModuleA.noise, sourceModuleB.noise);
                }
            }
        }
    }
}