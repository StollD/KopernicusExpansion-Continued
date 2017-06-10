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
            public class Blend : NoiseLoader<LibNoise.Modifiers.Blend>
            {
                [PreApply]
                [ParserTarget("SourceA", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleA;

                [PreApply]
                [ParserTarget("SourceB", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModuleB;

                [PreApply]
                [ParserTarget("Weight", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader weightModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Blend(sourceModuleA.noise, sourceModuleB.noise, weightModule.noise);
                }
            }
        }
    }
}