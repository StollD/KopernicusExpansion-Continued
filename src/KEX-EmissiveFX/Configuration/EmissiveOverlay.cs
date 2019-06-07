using System;
using Kopernicus;
using Kopernicus.Components;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Interfaces;
using Kopernicus.Configuration;
using Kopernicus.Configuration.Parsing;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EmissiveFX
    {
        namespace Configuration
        {
            [ParserTargetExternal("ScaledVersion", "EmissiveOverlay", "Kopernicus")]
            public class EmissiveOverlay : BaseLoader, IParserEventSubscriber
            {
                // The new material for the scaled space renderer
                private Material scaledMaterial;

                // The texture defining how emissive a spot on the planet is.
                [ParserTarget("emissiveMap")]
                private Texture2DParser emissiveMap
                {
                    get { return (Texture2D)scaledMaterial.GetTexture("_EmissiveMap"); }
                    set { scaledMaterial.SetTexture("_EmissiveMap", value); }
                }

                // The color of the emission
                [ParserTarget("color", Optional = false)]
                private ColorParser color
                {
                    get { return scaledMaterial.GetColor("_Color"); }
                    set { scaledMaterial.SetColor("_Color", value); }
                }

                // How bright should the emission be?
                [ParserTarget("brightness")]
                private NumericParser<Single> brightness
                {
                    get { return scaledMaterial.GetFloat("_Brightness"); }
                    set { scaledMaterial.SetFloat("_Brightness", value); }
                }

                // How much of the original texture should be visible?
                [ParserTarget("transparency")]
                private NumericParser<Single> transparency
                {
                    get { return scaledMaterial.GetFloat("_Transparency"); }
                    set { scaledMaterial.SetFloat("_Transparency", value); }
                }

                // Apply Event
                void IParserEventSubscriber.Apply(ConfigNode node)
                {
                    // Create scaled material and set proprties to defaults
                    scaledMaterial = new Material(ShaderLoader.GetShader("KopernicusExpansion/EmissiveFX"));
                    scaledMaterial.SetTextureScale("_EmissiveMap", Vector2.one);
                    scaledMaterial.SetTextureOffset("_EmissiveMap", Vector2.zero);
                    scaledMaterial.SetColor("_Color", new Color(1f, 1f, 1f));
                    scaledMaterial.SetFloat("_Brightness", 1.25f);
                    scaledMaterial.SetFloat("_Transparency", 0.75f);

                    // Assign the scaled space texture
                    Material mat = generatedBody.scaledVersion.GetComponent<Renderer>().sharedMaterial;
                    scaledMaterial.SetTexture("_EmissiveMap", mat.GetTexture("_MainTex"));
                }

                // Post Apply Event
                void IParserEventSubscriber.PostApply(ConfigNode node)
                {
                    // Add material to scaled version
                    Renderer renderer = generatedBody.scaledVersion.GetComponent<Renderer>();
                    Material origMat = renderer.sharedMaterial;
                    renderer.materials = new Material[] { origMat, scaledMaterial };
                }
            }
        }
    }
}
