using Kopernicus;
using LibNoise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class NoiseLoader<T> : NoiseLoader where T : IModule
            {
                // The noise we are loading
                public new T noise
                {
                    get { return (T)base.noise; }
                    set { base.noise = value; }
                }

                public override void Apply(ConfigNode node)
                {
                    noise = Activator.CreateInstance<T>();
                }
            }

            [RequireConfigType(ConfigType.Node)]
            public class NoiseLoader : IParserEventSubscriber
            {
                public IModule noise;

            public virtual void Apply(ConfigNode node)
            {
                
            }

            public virtual void PostApply(ConfigNode node)
            {

            }
        }
        }
    }
}