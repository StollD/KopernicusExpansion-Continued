using Kopernicus;
using Kopernicus.Configuration.ModLoader;
using System;
using Kopernicus.Configuration.NoiseLoader;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class ModularNoise : ModLoader<PQSMod_ModularNoise>
            {
                [ParserTarget("deformity")]
                public NumericParser<Double> deformity
                {
                    get { return mod.deformity; }
                    set { mod.deformity = value; }
                }

                [ParserTarget("normalizeHeight")]
                public NumericParser<Boolean> normalizeHeight
                {
                    get { return mod.normalizeHeight; }
                    set { mod.normalizeHeight = value; }
                }

                [ParserTarget("Noise", NameSignificance = NameSignificance.Type, Optional = false)]
                public INoiseLoader noise
                {
                    get
                    {
                        if (mod.noise != null)
                        {
                            Type noiseType = mod.noise.GetType();
                            foreach (Type loaderType in Parser.ModTypes)
                            {
                                if (loaderType.BaseType == null)
                                    continue;
                                if (loaderType.BaseType.Namespace != "Kopernicus.Configuration.NoiseLoader")
                                    continue;
                                if (!loaderType.BaseType.Name.StartsWith("NoiseLoader"))
                                    continue;
                                if (loaderType.BaseType.GetGenericArguments()[0] != noiseType)
                                    continue;
                        
                                // We found our loader type
                                INoiseLoader loader = (INoiseLoader) Activator.CreateInstance(loaderType);
                                loader.Create(mod.noise);
                                return loader;
                            }
                        }
                        return null;
                    }
                    set { mod.noise = value.Noise; }
                }
            }
        }
    }
}