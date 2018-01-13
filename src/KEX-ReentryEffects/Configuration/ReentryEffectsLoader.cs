using Kopernicus;
using Kopernicus.Configuration;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ReentryEffects
    {
        namespace Configuration
        {
            [ParserTargetExternal("Body", "ReentryEffects", "Kopernicus")]
            [RequireConfigType(ConfigType.Node)]
            public class ReentryEffectsLoader : BaseLoader
            {
                [ParserTarget("ReentryHeat", allowMerge = true)]
                public AeroFxLoader reentryHeat
                {
                    get { return generatedBody.Get<AeroFXState>("reentryHeat"); }
                    set { generatedBody.Set("reentryHeat", value.Value); }
                }
                
                [ParserTarget("Condensation", allowMerge = true)]
                public AeroFxLoader condensation
                {
                    get { return generatedBody.Get<AeroFXState>("condensation"); }
                    set { generatedBody.Set("condensation", value.Value); }
                }

                public ReentryEffectsLoader()
                {
                    reentryHeat = ReentryDefault();
                    condensation = CondensationDefault();
                }

                private static AeroFXState ReentryDefault()
                {
                    AeroFXState fx = new AeroFXState();
                    fx.airspeedNoisePitch = new MinMaxFloat {min = 0.5f, max = 2f};
                    fx.airspeedNoiseVolume = new MinMaxFloat {min = 0, max = 1};
                    fx.color = new MinMaxColor
                    {
                        min = new Color(1, 0.294f, 0.114f, 0f),
                        max = new Color(1f, 0.294f, 0.114f, 1f)
                    };
                    fx.edgeFade = new MinMaxFloat {min = 0, max = 0.3f};
                    fx.falloff1 = new MinMaxFloat {min = 0.9f, max = 0.9f};
                    fx.falloff2 = new MinMaxFloat {min = 1, max = 1};
                    fx.falloff3 = new MinMaxFloat {min = 2, max = 1};
                    fx.intensity = new MinMaxFloat {min = 0, max = 0.11f};
                    fx.length = new MinMaxFloat {min = 5, max = 15};
                    fx.lightPower = new MinMaxFloat {min = -3, max = 8};
                    fx.wobble = new MinMaxFloat {min = 1, max = 1};
                    return fx;
                }

                private static AeroFXState CondensationDefault()
                {
                    AeroFXState fx = new AeroFXState ();
                    fx.airspeedNoisePitch = new MinMaxFloat {min = 0.3f, max = 1f};
                    fx.airspeedNoiseVolume = new MinMaxFloat {min = 0, max = 0.5f};
                    fx.color = new MinMaxColor
                    {
                        min = new Color(0.22f, 0.22f, 0.22f, 0f),
                        max = new Color(0.22f, 0.22f, 0.22f, 1f)
                    };
                    fx.edgeFade = new MinMaxFloat {min = 0, max = 0};
                    fx.falloff1 = new MinMaxFloat {min = 0.9f, max = 0.9f};
                    fx.falloff2 = new MinMaxFloat {min = -0.6f, max = -0.6f};
                    fx.falloff3 = new MinMaxFloat {min = 0.5f, max = 0.5f};
                    fx.intensity = new MinMaxFloat {min = 0, max = 0.11f};
                    fx.length = new MinMaxFloat {min = 2, max = 3.5f};
                    fx.lightPower = new MinMaxFloat {min = 0f, max = 0f};
                    fx.wobble = new MinMaxFloat {min = 0, max = -0.2f};
                    return fx;
                }
            }
        }
    }
}