using Kopernicus;
using LibNoise;
using System;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Select : NoiseLoader<LibNoise.Modifiers.Select>
            {
                [ParserTarget("lowerBound")]
                public NumericParser<Double> lower;

                [ParserTarget("upperBound")]
                public NumericParser<Double> upper;

                [ParserTarget("edgeFalloff")]
                public NumericParser<Double> edgefalloff;

                [PreApply]
                [ParserTarget("Control", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> controlModule;

                [PreApply]
                [ParserTarget("SourceA", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> sourceModuleA;

                [PreApply]
                [ParserTarget("SourceB", nameSignificance = NameSignificance.Type, optional = false)]
                public NoiseLoader<IModule> sourceModuleB;

                public override void Apply(ConfigNode node)
                {
                    noise = new LibNoise.Modifiers.Select(controlModule.noise, sourceModuleA.noise, sourceModuleB.noise);
                }

                public override void PostApply(ConfigNode node)
                {
                    noise.SetBounds(lower, upper);
                    noise.EdgeFalloff = edgefalloff;
                }
            }
        }
    }
}