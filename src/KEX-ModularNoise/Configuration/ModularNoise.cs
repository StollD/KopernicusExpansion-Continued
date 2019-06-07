using Kopernicus;
using Kopernicus.Configuration.ModLoader;
using System;
using Kopernicus.ConfigParser;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
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
                    get { return Mod.deformity; }
                    set { Mod.deformity = value; }
                }

                [ParserTarget("normalizeHeight")]
                public NumericParser<Boolean> normalizeHeight
                {
                    get { return Mod.normalizeHeight; }
                    set { Mod.normalizeHeight = value; }
                }

                [ParserTarget("Noise", NameSignificance = NameSignificance.Type, Optional = false)]
                public INoiseLoader noise
                {
                    get
                    {
                        if (Mod.noise != null)
                        {
                            Type noiseType = Mod.noise.GetType();
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
                                loader.Create(Mod.noise);
                                return loader;
                            }
                        }
                        return null;
                    }
                    set { Mod.noise = value.Noise; }
                }
            }
        }
    }
}