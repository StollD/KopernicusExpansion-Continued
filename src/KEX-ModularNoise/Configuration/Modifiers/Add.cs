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
                [ParserTargetCollection("self", nameSignificance = NameSignificance.Type)]
                public List<NoiseLoader<IModule>> modules;

                public override void Apply(ConfigNode node)
                {
                    IModule m = modules[0].noise;
                    for (Int32 i = 1; i < modules.Count; i++)
                    {
                        m = new LibNoise.Modifiers.Add(m, modules[i].noise);
                    }
                    noise = (LibNoise.Modifiers.Add)m;
                }
            }
        }
    }
}