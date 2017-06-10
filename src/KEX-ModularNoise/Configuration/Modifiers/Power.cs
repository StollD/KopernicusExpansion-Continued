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
            public class Power : NoiseLoader<LibNoise.Modifiers.Power>
            {
                [PreApply]
                [ParserTarget("Base", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader baseModule;

                [PreApply]
                [ParserTarget("Power", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader powerModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Power(baseModule.noise, powerModule.noise);
                }
            }
        }
    }
}