using System;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.ConfigParser.Enumerations;
using Kopernicus.ConfigParser.Interfaces;
using KopernicusExpansion.CometTail.Effects;

namespace KopernicusExpansion
{
    namespace CometTail
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class CometTailLoader : IParserEventSubscriber
            {
                // Constructor
                public CometTailLoader()
                {
                    tail = new Tail();
                }

                public Tail tail;

                [PreApply]
                [ParserTarget("type", Optional = false)]
                public EnumParser<CometTailType> type
                {
                    get { return tail.type; }
                    set { tail.type = value; }
                }

                [ParserTarget("color")]
                public ColorParser color
                {
                    get { return tail.color; }
                    set { tail.color = value; }
                }

                [ParserTarget("rimPower")]
                public NumericParser<Single> rimPower
                {
                    get { return tail.rimPower; }
                    set { tail.rimPower = value; }
                }

                [ParserTarget("distortion")]
                public NumericParser<Single> distortion
                {
                    get { return tail.distortion; }
                    set { tail.distortion = value; }
                }

                [ParserTarget("alphaDistortion")]
                public NumericParser<Single> alphaDistortion
                {
                    get { return tail.alphaDistortion; }
                    set { tail.alphaDistortion = value; }
                }

                [ParserTarget("zDistortion")]
                public NumericParser<Single> zDistortion
                {
                    get { return tail.zDistortion; }
                    set { tail.zDistortion = value; }
                }

                [ParserTarget("frequency")]
                public NumericParser<Single> frequency
                {
                    get { return tail.frequency; }
                    set { tail.frequency = value; }
                }

                [ParserTarget("lacunarity")]
                public NumericParser<Single> lacunarity
                {
                    get { return tail.lacunarity; }
                    set { tail.lacunarity = value; }
                }

                [ParserTarget("gain")]
                public NumericParser<Single> gain
                {
                    get { return tail.gain; }
                    set { tail.gain = value; }
                }

                [ParserTarget("radius")]
                public NumericParser<Single> radius
                {
                    get { return tail.radius; }
                    set { tail.radius = value; }
                }

                [ParserTarget("length")]
                public NumericParser<Single> maxLength
                {
                    get { return tail.length; }
                    set { tail.length = value; }
                }

                [ParserTarget("opacityCurve")]
                public FloatCurveParser opacityCurve
                {
                    get { return tail.opacityCurve; }
                    set { tail.opacityCurve = value; }
                }

                [ParserTarget("brightnessCurve")]
                public FloatCurveParser brightnessCurve
                {
                    get { return tail.brightnessCurve; }
                    set { tail.brightnessCurve = value; }
                }

                // Apply Event
                void IParserEventSubscriber.Apply(ConfigNode node)
                {
                    //set default curves
                    tail.opacityCurve = new FloatCurve();
                    tail.opacityCurve.Add(0f, 0.6f);
                    tail.opacityCurve.Add(5e9f, 0.45f);
                    tail.opacityCurve.Add(1.25e10f, 0.1f);
                    tail.opacityCurve.Add(2e10f, 0.0075f);
                    tail.opacityCurve.Add(3e10f, 0f);
                    tail.opacityCurve.Add(Single.MaxValue, 0f);

                    if (tail.type == CometTailType.Ion)
                    {
                        tail.brightnessCurve = new FloatCurve();
                        tail.brightnessCurve.Add(0f, 1f);
                        tail.brightnessCurve.Add(5e9f, 0.4f);
                        tail.brightnessCurve.Add(1.25e10f, 0.09f);
                        tail.brightnessCurve.Add(2e10f, 0.0075f);
                        tail.brightnessCurve.Add(3e10f, 0f);
                        tail.brightnessCurve.Add(Single.MaxValue, 0f);
                    }
                    else
                    {
                        tail.brightnessCurve = new FloatCurve();
                        tail.brightnessCurve.Add(0f, 1f);
                        tail.brightnessCurve.Add(5e9f, 0.53f);
                        tail.brightnessCurve.Add(1.25e10f, 0.1f);
                        tail.brightnessCurve.Add(2e10f, 0.008f);
                        tail.brightnessCurve.Add(3e10f, 0f);
                        tail.brightnessCurve.Add(Single.MaxValue, 0f);
                    }
                }

                // Post Apply Event
                void IParserEventSubscriber.PostApply(ConfigNode node) { }
            }
        }
    }
}