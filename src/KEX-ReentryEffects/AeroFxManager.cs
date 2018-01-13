using System;
using Kopernicus;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ReentryEffects
    {
        [KSPAddon(KSPAddon.Startup.Flight, false)]
        public class AeroFxManager : MonoBehaviour
        {
            /// <summary>
            /// The KSP effect controller we are editing
            /// </summary>
            public AerodynamicsFX aeroFX;

            /// <summary>
            /// The default FX state for reentry.
            /// </summary>
            public static AeroFXState ReentryHeat;

            /// <summary>
            /// The default FX state for flying inside of the atmosphere
            /// </summary>
            public static AeroFXState Condensation;
            
            void Awake()
            {
                aeroFX = Resources.FindObjectsOfTypeAll<AerodynamicsFX>()[0];
                GameEvents.onDominantBodyChange.Add(OnDominantBodyChange);
                
                // Check if we already stored the default values
                if (ReentryHeat == null)
                {
                    ReentryHeat = aeroFX.ReentryHeat;
                }
                if (Condensation == null)
                {
                    Condensation = aeroFX.Condensation;
                }
            }

            void OnDestroy()
            {
                GameEvents.onDominantBodyChange.Remove(OnDominantBodyChange);
            }

            /// <summary>
            /// This method gets called when the vessel enters the SOI of a new body. Here we edit the FX controller, so 
            /// it uses the effects defined for this body.
            /// </summary>
            void OnDominantBodyChange(GameEvents.FromToAction<CelestialBody, CelestialBody> data)
            {
                // The body the vessel switched to
                CelestialBody body = data.to;

                if (body.Has("reentryHeat"))
                {
                    aeroFX.ReentryHeat = body.Get<AeroFXState>("reentryHeat");
                }
                else
                {
                    aeroFX.ReentryHeat = ReentryHeat;
                }
                
                if (body.Has("condensation"))
                {
                    aeroFX.Condensation = body.Get<AeroFXState>("condensation");
                }
                else
                {
                    aeroFX.Condensation = Condensation;
                }
            }
        }
    }
}