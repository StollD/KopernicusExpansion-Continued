using Kopernicus;
using Kopernicus.Components;
using Kopernicus.Configuration;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EmissiveFX
    {
        namespace Configuration
        {
            [ParserTargetExternal("ScaledVersion", "EmissiveOverlay")]
            public class EmissiveOverlay : BaseLoader, IParserEventSubscriber
            {
                // The new material for the scaled space renderer
                private Material scaledMaterial;

                // The texture defining how emissive a spot on the planet is.
                [ParserTarget("emissiveMap")]
                private Texture2DParser emissiveMap
                {
                    set { scaledMaterial.SetTexture("_EmissiveMap", value); }
                }

                // The color of the emission
                [ParserTarget("color", optional = false)]
                private ColorParser color
                {
                    set { scaledMaterial.SetColor("_Color", value); }
                }

                // How bright should the emission be?
                [ParserTarget("brightness")]
                private NumericParser<float> brightness
                {
                    set { scaledMaterial.SetFloat("_Brightness", value); }
                }

                // How much of the original texture should be visible?
                [ParserTarget("transparency")]
                private NumericParser<float> transparency
                {
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
