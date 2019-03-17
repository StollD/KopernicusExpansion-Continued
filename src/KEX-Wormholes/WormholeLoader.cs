using System;
using Kopernicus;
using Kopernicus.Configuration;
using Kopernicus.UI;

namespace KopernicusExpansion
{
    namespace Wormholes
    {
        [ParserTargetExternal("Body", "Wormhole", "Kopernicus")]
        public class WormholeLoader : BaseLoader, ITypeParser<WormholeComponent>
        {
            public WormholeComponent Value { get; set; }

            [ParserTarget("partner")]
            public String partnerBody
            {
                get { return Value.partnerBody; }
                set { Value.partnerBody = value; }
            }

            [ParserTarget("influenceAltitude")]
            public NumericParser<Double> influenceAltitude
            {
                get { return Value.influenceAltitude; }
                set { Value.influenceAltitude = value; }
            }

            [ParserTarget("jumpMaxAltitude")]
            public NumericParser<Double> jumpMaxAltitude
            {
                get { return Value.jumpMaxAltitude; }
                set { Value.jumpMaxAltitude = value; }
            }

            [ParserTarget("jumpMinAltitude")]
            public NumericParser<Double> jumpMinAltitude
            {
                get { return Value.jumpMinAltitude; }
                set { Value.jumpMinAltitude = value; }
            }

            [ParserTarget("heatRate")]
            public NumericParser<Double> heatRate
            {
                get { return Value.heatRate; }
                set { Value.heatRate = value; }
            }

            [ParserTarget("entryMessage")]
            public String entryMessage
            {
                get { return Value.entryMessage; }
                set { Value.entryMessage = value; }
            }

            [ParserTarget("exitMessage")]
            public String exitMessage
            {
                get { return Value.exitMessage; }
                set { Value.exitMessage = value; }
            }
            
            [ParserTarget("entryMsgDuration")]
            public NumericParser<UInt32> entryMsgDuration
            {
                
            }
            
            
            /// <summary>
            /// Creates a new Wormhole Loader from the Injector context.
            /// </summary>
            public WormholeLoader()
            {
                // Is this the parser context?
                if (!Injector.IsInPrefab)
                {
                    throw new InvalidOperationException("Must be executed in Injector context.");
                }
                
                Value = generatedBody.celestialBody.gameObject.AddComponent<WormholeComponent>();
            }

            /// <summary>
            /// Creates a new Wormhole Loader from a spawned CelestialBody.
            /// </summary>
            [KittopiaConstructor(KittopiaConstructor.Parameter.CelestialBody)]
            public WormholeLoader(CelestialBody body)
            {
                // Is this a spawned body?
                if (body?.scaledBody == null || Injector.IsInPrefab)
                {
                    throw new InvalidOperationException("The body must be already spawned by the PSystemManager.");
                }

                // Store values
                Value = body.GetComponent<WormholeComponent>();
                if (Value == null)
                {
                    Value = body.gameObject.AddComponent<WormholeComponent>();
                }
            }
        }
    }
}
