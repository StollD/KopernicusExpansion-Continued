using Kopernicus;
using LibNoise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class NoiseLoader<T> : IParserEventSubscriber where T : IModule
            {
                // The noise we are loading
                public T noise { get; set; }

                public virtual void Apply(ConfigNode node)
                {
                    noise = Activator.CreateInstance<T>();
                }

                public virtual void PostApply(ConfigNode node)
                {

                }
            }
        }
    }
}