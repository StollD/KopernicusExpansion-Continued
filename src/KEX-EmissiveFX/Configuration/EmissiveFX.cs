using System;
using Kopernicus;
using Kopernicus.Configuration.ModLoader;
using UnityEngine;
using Kopernicus.Components;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;

namespace KopernicusExpansion
{
    namespace EmissiveFX
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class EmissiveFX : ModLoader<PQSMod_EmissiveFX>, IParserEventSubscriber
            {
                // The color of the emission
                [ParserTarget("color", Optional = false)]
                private ColorParser PQScolor
                {
                    get { return Mod.EmissiveMaterial.GetColor("_Color"); }
                    set { Mod.EmissiveMaterial.SetColor("_Color", value); }
                }

                // How bright should the emission be?
                [ParserTarget("brightness")]
                private NumericParser<Single> PQSbrightness
                {
                    get { return Mod.EmissiveMaterial.GetFloat("_Brightness"); }
                    set { Mod.EmissiveMaterial.SetFloat("_Brightness", value); }
                }

                // How visible should the original texture be?
                [ParserTarget("transparency")]
                private NumericParser<Single> PQStransparency
                {
                    get { return Mod.EmissiveMaterial.GetFloat("_Transparency"); }
                    set { Mod.EmissiveMaterial.SetFloat("_Transparency", value); }
                }

                // Apply Event
                void IParserEventSubscriber.Apply(ConfigNode node)
                {
                    Mod.EmissiveMaterial = new Material(ShaderLoader.GetShader("KopernicusExpansion/EmissiveFX"));
                    Mod.EmissiveMaterial.SetColor("_Color", new Color(1f, 1f, 1f));
                    Mod.EmissiveMaterial.SetFloat("_Brightness", 1.4f);
                    Mod.EmissiveMaterial.SetFloat("_Transparency", 0.6f);
                }

                // PostApply Event
                void IParserEventSubscriber.PostApply(ConfigNode node) { }
            }
        }
    }
}