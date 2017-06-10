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
            public class InvertInput : NoiseLoader<LibNoise.Modifiers.InvertInput>
            {
                [PreApply]
                [ParserTarget("Source", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader sourceModule;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.InvertInput(sourceModule.noise);
                }
            }
        }
    }
}