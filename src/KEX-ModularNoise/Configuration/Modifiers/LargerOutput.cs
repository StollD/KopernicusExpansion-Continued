using Kopernicus;
using LibNoise;
using System.Collections.Generic;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class LargerOutput : NoiseLoader<LibNoise.Modifiers.LargerOutput>
            {
                [PreApply]
                [ParserTarget("SourceA", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleA;

                [PreApply]
                [ParserTarget("SourceB", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleB;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.LargerOutput(sourceModuleA.noise, sourceModuleB.noise);
                }
            }
        }
    }
}