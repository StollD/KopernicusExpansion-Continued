using Kopernicus;
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
                [ParserTarget("type", optional = false)]
                public EnumParser<CometTailType> type
                {
                    set { tail.type = value.value; }
                }

                [ParserTarget("color")]
                public ColorParser color
                {
                    set { tail.color = value.value; }
                }

                [ParserTarget("rimPower")]
                public NumericParser<float> rimPower
                {
                    set { tail.rimPower = value.value; }
                }

                [ParserTarget("distortion")]
                public NumericParser<float> distortion
                {
                    set { tail.distortion = value.value; }
                }

                [ParserTarget("alphaDistortion")]
                public NumericParser<float> alphaDistortion
                {
                    set { tail.alphaDistortion = value.value; }
                }

                [ParserTarget("zDistortion")]
                public NumericParser<float> zDistortion
                {
                    set { tail.zDistortion = value.value; }
                }

                [ParserTarget("frequency")]
                public NumericParser<float> frequency
                {
                    set { tail.frequency = value.value; }
                }

                [ParserTarget("lacunarity")]
                public NumericParser<float> lacunarity
                {
                    set { tail.lacunarity = value.value; }
                }

                [ParserTarget("gain")]
                public NumericParser<float> gain
                {
                    set { tail.gain = value; }
                }

                [ParserTarget("radius")]
                public NumericParser<float> radius
                {
                    set { tail.radius = value; }
                }

                [ParserTarget("length")]
                public NumericParser<float> maxLength
                {
                    set { tail.length = value; }
                }

                [ParserTarget("opacityCurve")]
                public FloatCurveParser opacityCurve
                {
                    set { tail.opacityCurve = value; }
                }

                [ParserTarget("brightnessCurve")]
                public FloatCurveParser brightnessCurve
                {
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
                    tail.opacityCurve.Add(float.MaxValue, 0f);

                    if (tail.type == CometTailType.Ion)
                    {
                        tail.brightnessCurve = new FloatCurve();
                        tail.brightnessCurve.Add(0f, 1f);
                        tail.brightnessCurve.Add(5e9f, 0.4f);
                        tail.brightnessCurve.Add(1.25e10f, 0.09f);
                        tail.brightnessCurve.Add(2e10f, 0.0075f);
                        tail.brightnessCurve.Add(3e10f, 0f);
                        tail.brightnessCurve.Add(float.MaxValue, 0f);
                    }
                    else
                    {
                        tail.brightnessCurve = new FloatCurve();
                        tail.brightnessCurve.Add(0f, 1f);
                        tail.brightnessCurve.Add(5e9f, 0.53f);
                        tail.brightnessCurve.Add(1.25e10f, 0.1f);
                        tail.brightnessCurve.Add(2e10f, 0.008f);
                        tail.brightnessCurve.Add(3e10f, 0f);
                        tail.brightnessCurve.Add(float.MaxValue, 0f);
                    }
                }

                // Post Apply Event
                void IParserEventSubscriber.PostApply(ConfigNode node) { }
            }
        }
    }
}