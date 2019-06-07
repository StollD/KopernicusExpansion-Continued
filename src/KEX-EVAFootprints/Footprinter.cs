using Kopernicus.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EVAFootprints
    {
        public class Footprinter : MonoBehaviour
        {
            public KerbalEVA eva;
            public Part part;

            private static readonly String[] WalkingStates = new String[]{
                "Walk (Arcade)",
                "Walk (FPS)",
            };
            private static readonly String[] RunningStates = new String[]{
                "Run (Arcade)",
                "Run (FPS)"
            };
            private static readonly String[] BoundingStates = new String[]{
                "Low G Bound (Grounded - Arcade)",
                "Low G Bound (Grounded - FPS)"
            };

            private void Start()
            {
                StartCoroutine(UpdateCoroutine());
            }

            private IEnumerator<YieldInstruction> UpdateCoroutine()
            {
                while (true)
                {
                    // don't footprint non-footprintable bodies
                    if (!FootprintSpawner.FootprintsAllowed.Contains(FlightGlobals.currentMainBody.transform.name))
                        yield return null;

                    String state = eva.fsm.CurrentState.name;

                    if (WalkingStates.Contains(state))
                    {
                        SpawnFootprint();
                        leftOrRight *= -1f;
                        yield return new WaitForSeconds(0.4f);
                    }
                    else if (RunningStates.Contains(state))
                    {
                        SpawnFootprint();
                        leftOrRight *= -1f;
                        yield return new WaitForSeconds(0.15f);
                    }
                    else if (BoundingStates.Contains(state))
                    {
                        SpawnFootprint();
                        leftOrRight *= -1f;
                        yield return new WaitForSeconds(0.4f);
                    }

                    yield return null;
                }
            }

            private Single leftOrRight = 1f;

            private void SpawnFootprint()
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, FlightGlobals.getGeeForceAtPosition(part.vessel.GetWorldPos3D()).normalized);
                if (Physics.Raycast(ray, out hit, 2f, (1 << GameLayers.LOCAL_SPACE)))
                {
                    // don't add footprints to non-terrain features
                    if (hit.transform.GetComponent<PQ>() == null)
                        return;

                    GameObject obj = Instantiate(FootprintSpawner.footprintPrefab);
                    obj.SetActive(true);

                    Vector3 hitNormal = hit.normal;
                    Vector3 right = transform.right;
                    Vector3 cross = Vector3.Cross(right, hitNormal);

                    obj.transform.position = hit.point + (hitNormal * 0.015f);
                    obj.transform.rotation = Quaternion.LookRotation(cross, hitNormal); // vector math is fun :D
                    obj.transform.Translate(Vector3.right * 0.1f * leftOrRight);
                    obj.transform.localScale = new Vector3(leftOrRight, 1f, 1f);

                    // parent to PQS so that we can avoid the Krakensbane/FloatingOrigin
                    obj.transform.parent = obj.transform;
                }
            }
        }
    }
}