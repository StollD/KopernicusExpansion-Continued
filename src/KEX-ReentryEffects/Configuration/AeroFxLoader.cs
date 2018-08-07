using System;
using Kopernicus;
using Kopernicus.UI;

namespace KopernicusExpansion
{
    namespace ReentryEffects
    {
        namespace Configuration
        {
            /// <summary>
            /// Loads an AeroFX from a Kopernicus Body config
            /// </summary>
            [RequireConfigType(ConfigType.Node)]
            public class AeroFxLoader : ITypeParser<AeroFXState>
            {
                [RequireConfigType(ConfigType.Node)]
                public class MinMaxFloatParser : ITypeParser<MinMaxFloat>
                {
                    // The value that is being parsed
                    public MinMaxFloat Value { get; set; }

                    [ParserTarget("min")]
                    public NumericParser<Single> min
                    {
                        get { return Value.min; }
                        set { Value.min = value; }
                    }

                    [ParserTarget("max")]
                    public NumericParser<Single> max
                    {
                        get { return Value.max; }
                        set { Value.max = value; }
                    }

                    [KittopiaConstructor(KittopiaConstructor.Parameter.Empty)]
                    public MinMaxFloatParser()
                    {
                        Value = new MinMaxFloat();
                    }

                    public MinMaxFloatParser(MinMaxFloat value)
                    {
                        Value = value;
                    }

                    public static implicit operator MinMaxFloat(MinMaxFloatParser parser)
                    {
                        return parser.Value;
                    }

                    public static implicit operator MinMaxFloatParser(MinMaxFloat value)
                    {
                        return new MinMaxFloatParser(value);
                    }
                }
                
                [RequireConfigType(ConfigType.Node)]
                public class MinMaxColorParser : ITypeParser<MinMaxColor>
                {
                    // The value that is being parsed
                    public MinMaxColor Value { get; set; }

                    [ParserTarget("min")]
                    public ColorParser min
                    {
                        get { return Value.min; }
                        set { Value.min = value; }
                    }

                    [ParserTarget("max")]
                    public ColorParser max
                    {
                        get { return Value.max; }
                        set { Value.max = value; }
                    }

                    [KittopiaConstructor(KittopiaConstructor.Parameter.Empty)]
                    public MinMaxColorParser()
                    {
                        Value = new MinMaxColor();
                    }

                    public MinMaxColorParser(MinMaxColor value)
                    {
                        Value = value;
                    }

                    public static implicit operator MinMaxColor(MinMaxColorParser parser)
                    {
                        return parser.Value;
                    }

                    public static implicit operator MinMaxColorParser(MinMaxColor value)
                    {
                        return new MinMaxColorParser(value);
                    }
                }
                
                // The effect that is being parsed
                public AeroFXState Value { get; set; }

                [ParserTarget("airspeedNoisePitch")]
                public MinMaxFloatParser airspeedNoisePitch
                {
                    get { return Value.airspeedNoisePitch; }
                    set { Value.airspeedNoisePitch = value; }
                }

                [ParserTarget("airspeedNoiseVolume")]
                public MinMaxFloatParser airspeedNoiseVolume
                {
                    get { return Value.airspeedNoiseVolume; }
                    set { Value.airspeedNoiseVolume = value; }
                }

                [ParserTarget("edgeFade")]
                public MinMaxFloatParser edgeFade
                {
                    get { return Value.edgeFade; }
                    set { Value.edgeFade = value; }
                }

                [ParserTarget("falloff1")]
                public MinMaxFloatParser falloff1
                {
                    get { return Value.falloff1; }
                    set { Value.falloff1 = value; }
                }

                [ParserTarget("falloff2")]
                public MinMaxFloatParser falloff2
                {
                    get { return Value.falloff2; }
                    set { Value.falloff2 = value; }
                }

                [ParserTarget("falloff3")]
                public MinMaxFloatParser falloff3
                {
                    get { return Value.falloff3; }
                    set { Value.falloff3 = value; }
                }

                [ParserTarget("intensity")]
                public MinMaxFloatParser intensity
                {
                    get { return Value.intensity; }
                    set { Value.intensity = value; }
                }

                [ParserTarget("length")]
                public MinMaxFloatParser length
                {
                    get { return Value.length; }
                    set { Value.length = value; }
                }

                [ParserTarget("lightPower")]
                public MinMaxFloatParser lightPower
                {
                    get { return Value.lightPower; }
                    set { Value.lightPower = value; }
                }

                [ParserTarget("wobble")]
                public MinMaxFloatParser wobble
                {
                    get { return Value.wobble; }
                    set { Value.wobble = value; }
                }

                [ParserTarget("color")]
                public MinMaxColorParser color
                {
                    get { return Value.color; }
                    set { Value.color = value; }
                }

                [KittopiaConstructor(KittopiaConstructor.Parameter.Empty)]
                public AeroFxLoader()
                {
                    Value = new AeroFXState();
                }

                public AeroFxLoader(AeroFXState value)
                {
                    Value = value;
                }

                public static implicit operator AeroFXState(AeroFxLoader parser)
                {
                    return parser.Value;
                }

                public static implicit operator AeroFxLoader(AeroFXState value)
                {
                    return new AeroFxLoader {Value = value};
                }
            }
        }

    }
}