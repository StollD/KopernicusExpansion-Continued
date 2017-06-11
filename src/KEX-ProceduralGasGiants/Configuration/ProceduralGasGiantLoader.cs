using Kopernicus;
using Kopernicus.Components;
using Kopernicus.Configuration;
using KopernicusExpansion.ProceduralGasGiants.Effects;
using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ProceduralGasGiants
    {
        namespace Configuration
        {
            [ParserTargetExternal("ScaledVersion", "ProceduralGasGiant", "Kopernicus")]
            public class ProceduralGasGiantLoader : BaseLoader, IParserEventSubscriber
            {
                // constructor
                public ProceduralGasGiantLoader()
                {
                    material = new Material(ShaderLoader.GetShader("KopernicusExpansion/ProceduralGasGiant"));
                    material.name = "ProceduralGasGiant_" + Guid.NewGuid().ToString();

                    // Set default values
                    material.SetFloat("_StormDistortion", 0f);
                }

                private Material material;
                private bool _hasStorms = true;
                private bool _animate = true;
                private float _cloudSpeed = 2f;
                private int _seed = 0;

                [ParserTarget("rampTexture", allowMerge = false)]
                public Texture2DParser rampTex
                {
                    get { return (Texture2D)material.GetTexture("_MainTex"); }
                    set { material.SetTexture("_MainTex", value); }
                }

                [ParserTarget("rampTextureGradient", allowMerge = false)]
                public Kopernicus.Configuration.Gradient rampTexGradient
                {
                    set
                    {
                        Texture2D ramp = new Texture2D(2048, 1, TextureFormat.RGB24, false);
                        Color[] colors = ramp.GetPixels(0);
                        for (int i = 0; i < colors.Length; i++)
                        {
                            float k = ((float)i) / ((float)colors.Length);
                            colors[i] = value.ColorAt(k);
                        }
                        ramp.SetPixels(colors, 0);
                        ramp.Apply();

                        material.SetTexture("_MainTex", ramp);
                    }
                }

                [ParserTarget("generateRampFromScaledTexture")]
                public Texture2DParser generateFromPrev
                {
                    set
                    {
                        try
                        {
                            Texture2D prevTex = value;
                            Texture2D newTex = new Texture2D(prevTex.height, 1, TextureFormat.RGB24, false);

                            for (int i = 0; i < prevTex.height; i++)
                            {
                                newTex.SetPixel(i, 0, prevTex.GetPixel(0, i));
                            }
                            newTex.Apply();

                            material.SetTexture("_MainTex", newTex);
                        }
                        catch (Exception exception)
                        {
                            Kopernicus.Logger.Active.LogException(exception);
                        }
                    }
                }

                [ParserTarget("stormMap")]
                public Texture2DParser stormMap
                {
                    get { return (Texture2D)material.GetTexture("_StormMap"); }
                    set { material.SetTexture("_StormMap", value); }
                }

                [ParserTarget("seed")]
                public NumericParser<int> seed
                {
                    get { return _seed; }
                    set { _seed = Math.Max(0, value); }
                }

                [ParserTarget("animate")]
                public NumericParser<bool> animate
                {
                    get { return _animate; }
                    set { _animate = value; }
                }

                [ParserTarget("cloudSpeed")]
                public NumericParser<float> cloudSpeed
                {
                    get { return _cloudSpeed; }
                    set { _cloudSpeed = Mathf.Max(0f, value); }
                }

                [PreApply]
                [ParserTarget("hasStorms")]
                public NumericParser<bool> hasStorms
                {
                    get { return _hasStorms; }
                    set { _hasStorms = value; }
                }

                [ParserTarget("distortion")]
                public NumericParser<float> distortion
                {
                    get { return material.GetFloat("_Distortion"); }
                    set { material.SetFloat("_Distortion", Mathf.Clamp(value, 0f, 0.05f)); }
                }

                [ParserTarget("frequency")]
                public NumericParser<float> frequency
                {
                    get { return material.GetFloat("_MainFrequency"); }
                    set { material.SetFloat("_MainFrequency", Mathf.Max(0f, value)); }
                }

                [ParserTarget("lacunarity")]
                public NumericParser<float> lacunarity
                {
                    get { return material.GetFloat("_Lacunarity"); }
                    set { material.SetFloat("_Lacunarity", Mathf.Max(0f, value)); }
                }

                [ParserTarget("gain")]
                public NumericParser<float> gain
                {
                    get { return material.GetFloat("_Gain"); }
                    set { material.SetFloat("_Gain", Mathf.Max(0f, value)); }
                }

                [ParserTarget("stormFrequency")]
                public NumericParser<float> stormFrequency
                {
                    set
                    {
                        if (_hasStorms)
                            material.SetFloat("_StormFrequency", Mathf.Max(0f, value.value));
                        else
                            material.SetFloat("_StormFrequency", 0f);
                    }
                }

                [ParserTarget("stormDistortion")]
                public NumericParser<float> stormThreshold
                {
                    set
                    {
                        if (_hasStorms)
                            material.SetFloat("_StormDistortion", Mathf.Max(0f, value.value));
                        else
                            material.SetFloat("_StormDistortion", 2f);
                    }
                }

                // Apply Event
                void IParserEventSubscriber.Apply(ConfigNode node) { }

                // Post Apply Event
                void IParserEventSubscriber.PostApply(ConfigNode node)
                {
                    Renderer scaledRenderer = generatedBody.scaledVersion.GetComponent<Renderer>();
                    scaledRenderer.sharedMaterial = material;

                    ProceduralGasGiant gasGiantComponent = generatedBody.scaledVersion.AddComponent<ProceduralGasGiant>();
                    gasGiantComponent.speed = _cloudSpeed / 10000f;
                    gasGiantComponent.doAnimate = _animate;
                    gasGiantComponent.seed = _seed;
                }
            }
        }
    }
}
