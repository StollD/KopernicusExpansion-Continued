using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace KopernicusExpansion
{
    namespace Wormholes
    {
        public class WormholeComponent : MonoBehaviour
        {
            /// <summary>
            /// The upper altitude where the jump is possible
            /// </summary>
            public Double jumpMaxAltitude;

            /// <summary>
            /// The lower altitude where the jump is possible
            /// </summary>
            public Double jumpMinAltitude;

            /// <summary>
            /// If the ship is below this altitude is becomes uncontrollable
            /// </summary>
            public Double influenceAltitude;

            /// <summary>
            /// How much temperature should get added to the vessel every frame while passing the wormhole
            /// </summary>
            public Double heatRate;

            /// <summary>
            /// The wormhole that serves as the output of this one
            /// </summary>
            public String partnerBody;

            /// <summary>
            /// A message that is displayed when getting below the influence altitude
            /// </summary>
            public String entryMessage;

            /// <summary>
            /// A message that is displayed when getting over the influence altitude
            /// </summary>
            public String exitMessage;
            
            /// <summary>
            /// The amount of time in seconds that the entry message is displayed
            /// </summary>
            public float entryMsgDuration = 2f;

            /// <summary>
            /// The amount of time in seconds that the exit message is displayed
            /// </summary>
            public float exitMsgDuration = 2f;
            
            /// <summary>
            /// The body we are attached to
            /// </summary>
            private CelestialBody _body;

            /// <summary>
            /// The body we are jumping to
            /// </summary>
            private CelestialBody _partner;

            /// <summary>
            /// Random component
            /// </summary>
            private Random _random = new Random();

            /// <summary>
            /// The time it took for the vessel to arrive at the periapsis from the last frame
            /// </summary>
            private Double _lastAlt;
            
            void Start()
            {
                _body = GetComponent<CelestialBody>();
                _partner = PSystemManager.Instance.localBodies.Find(b => b.transform.name == partnerBody);
            }

            void Update()
            {
                if (FlightGlobals.ActiveVessel == null)
                {
                    return;
                }

                if (FlightGlobals.currentMainBody != _body)
                {
                    return;
                }
                
                // Is the vessel within the sphere of influence?
                if (FlightGlobals.ship_altitude < influenceAltitude)
                {
                    if (MapView.MapIsEnabled)
                    {
                        MapView.ExitMapView();
                    }
                    
                    if (FlightGlobals.ActiveVessel.GetComponent<CameraShake>() == null)
                    {
                        FlightGlobals.ActiveVessel.gameObject.AddComponent<CameraShake>().ShakeAmount = 2;
                        if (!String.IsNullOrEmpty(entryMessage))
                        {
                            ScreenMessages.PostScreenMessage(entryMessage, entryMsgDuration);
                        }
                    }
                }
                else
                {
                    CameraShake shake = FlightGlobals.ActiveVessel.GetComponent<CameraShake>();
                    if (shake != null)
                    {
                        Destroy(shake);
                        if (!String.IsNullOrEmpty(exitMessage))
                        {
                            ScreenMessages.PostScreenMessage(exitMessage, exitMsgDuration);
                        }
                    }

                    return;
                }

                // Are we within the jump range?
                if (FlightGlobals.ship_altitude < jumpMaxAltitude && FlightGlobals.ship_altitude > jumpMinAltitude)
                {
                    //NOTE: Reimpliment in different manner (Change to sound?)

                    /*for (Int32 i = 0; i < 5; i++)
                    {
                        Vector3 random = UnityEngine.Random.onUnitSphere;
                        Debug.Log("1");
                        GameObject effect = FXMonger.Splash(FlightGlobals.ship_position + random * 50, 1000).effectObj;
                        Debug.Log("2");
                        effect.transform.localScale = Vector3.one * 10;
                        Debug.Log("3");
                        effect.transform.up = -random;
                        Debug.Log("4");
                    }

                    // Update the FXMonger
                    FXMonger[] mongers = FindObjectsOfType<FXMonger>();
                    for (Int32 i = 0; i < mongers.Length; i++)
                    {
                        mongers[i].SendMessage("LateUpdate");
                    }*/

                    // If the Pe is within the jump range, jump
                    if (FlightGlobals.ship_altitude - _lastAlt > 0)
                    {
                        // Move the vessel to the new orbit
                        if (FlightGlobals.ActiveVessel.GetComponent<JumpMarker>() == null)
                        {
                            MakeOrbit(FlightGlobals.ActiveVessel.orbitDriver, _partner);
                            FlightGlobals.ActiveVessel.gameObject.AddComponent<JumpMarker>();
                        }
                    }
                }
                else
                {
                    // Heat up the vessel
                    for (Int32 i = 0; i < FlightGlobals.ActiveVessel.Parts.Count; i++)
                    {
                        FlightGlobals.ActiveVessel[i].temperature += heatRate;
                    }
                    
                    // Remove the jump marker if neccessary
                    JumpMarker jump = FlightGlobals.ActiveVessel.GetComponent<JumpMarker>();
                    if (jump != null)
                    {
                        Destroy(jump);
                    }
                }

                _lastAlt = FlightGlobals.ship_altitude;
            }

            private void MakeOrbit(OrbitDriver driver, CelestialBody reference)
            {
                Debug.Log("Jumping to " + partnerBody);
                
                CelestialBody oldBody = driver.referenceBody;
                FlightGlobals.overrideOrbit = true;
                FlightGlobals.fetch.Invoke("disableOverride", 2f);
                driver.vessel.Landed = false;
                driver.vessel.Splashed = false;
                driver.vessel.SetLandedAt("");
                driver.vessel.KillPermanentGroundContact();
                driver.vessel.ResetGroundContact();
                FlightGlobals.currentMainBody = reference;
                OrbitPhysicsManager.SetDominantBody(reference);

                // Pack vessels
                foreach (Vessel vessel in FlightGlobals.Vessels)
                {
                    if (!vessel.packed)
                    {
                        vessel.GoOnRails();
                    }
                }

                // Disable inverse rotation
                foreach (CelestialBody body in PSystemManager.Instance.localBodies)
                {
                    body.inverseRotation = false;
                }

                driver.orbit.referenceBody = reference;
                driver.updateFromParameters();

                // Finalize Vessel Movement
                CollisionEnhancer.bypass = true;
                FloatingOrigin.SetOffset(driver.vessel.transform.position);
                OrbitPhysicsManager.CheckReferenceFrame();
                OrbitPhysicsManager.HoldVesselUnpack(10);
                if (reference != oldBody)
                {
                    GameEvents.onVesselSOIChanged.Fire(
                        new GameEvents.HostedFromToAction<Vessel, CelestialBody>(driver.vessel, oldBody,
                            reference));
                }

                driver.vessel.IgnoreGForces(20);
            }
            
            public class JumpMarker : MonoBehaviour {}
        }
    }
}
