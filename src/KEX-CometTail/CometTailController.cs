using System;
using Kopernicus.Components;
using KopernicusExpansion.Noise;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace CometTail
    {
        /// <summary>
        /// A component controlling the behaviour of a comet tail
        /// </summary>
        public class CometTailController : MonoBehaviour
        {
            private const Single MaxTime = 10000;

            public CometTailType type;
            public CelestialBody body;
            public String targetBodyName;
            public Color color;

            public FloatCurve opacityCurve;
            public FloatCurve brightnessCurve;

            private Transform target;

            private Single currentTime = 0f;
            public Int32 seed = 0;
            public Single speed = 0.05f;
            public Boolean doAnimate = true;

            private ImprovedPerlinNoise noise;

            void Start()
            {
                DontDestroyOnLoad(this);
                noise = new ImprovedPerlinNoise(seed);
                noise.LoadResourcesFor3DNoise();
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.SetTexture("_PermTable2D", noise.GetPermutationTable2D());
                renderer.material.SetTexture("_Gradient3D", noise.GetGradient3D());
                currentTime = 0;
                renderer.material.SetFloat("_Evolution", currentTime);
            }

            void Update()
            {
                if (HighLogic.LoadedScene != GameScenes.FLIGHT && HighLogic.LoadedScene != GameScenes.SPACECENTER &&
                    HighLogic.LoadedScene != GameScenes.TRACKSTATION)
                    return;

                if (target != null)
                {
                    // look at target
                    // if (type == CometTailType.Ion)
                    // {
                        Quaternion rot = Quaternion.LookRotation((target.position - transform.position).normalized);
                        transform.localRotation = rot;
                    // }
                    // else
                    // {
                    //     Vector3 orbitVector = (body.getPositionAtUT(Planetarium.GetUniversalTime() + 1) -
                    //                            body.getPositionAtUT(Planetarium.GetUniversalTime())) * 0.00001f;
                    //     Vector3 lookVector =
                    //         Vector3.Normalize(
                    //             orbitVector - (Vector3.Normalize(transform.position - target.position) * 0.5f));
                    //     transform.LookAt(transform.position + lookVector * 100);
                    // }
                }
                else
                {
                    GetTarget();
                }
            }

            void LateUpdate()
            {
                if (HighLogic.LoadedScene != GameScenes.FLIGHT && HighLogic.LoadedScene != GameScenes.SPACECENTER &&
                    HighLogic.LoadedScene != GameScenes.TRACKSTATION)
                    return;

                if (target != null)
                {
                    // getting data
                    Single distance = Vector3.Distance(target.position, transform.position);
                    Single opacity = opacityCurve.Evaluate(distance * ScaledSpace.ScaleFactor);
                    Single brightness = brightnessCurve.Evaluate(distance * ScaledSpace.ScaleFactor);

                    // clamping
                    if (opacity < 0.0075f)
                        opacity = 0f;
                    if (brightness < 0.0075f)
                        brightness = 0f;

                    //set color
                    Color calculatedColor = Color.Lerp(color, Color.white, brightness);
                    calculatedColor.a = opacity;
                    Renderer renderer = GetComponent<Renderer>();
                    renderer.material.SetColor("_TintColor", calculatedColor);
                    renderer.material.SetFloat("_AlphaDistortion", brightness * 0.6f);

                    if (doAnimate)
                    {
                        // will only speed animation up to 250x timewarp speed
                        currentTime += Time.deltaTime * Mathf.Min(TimeWarp.CurrentRate, 250f) * speed;
                        if (currentTime >= MaxTime)
                            currentTime = 0;
                        renderer.material.SetFloat("_Evolution", currentTime);
                    }
                }
                else
                {
                    GetTarget();
                }
            }

            void GetTarget()
            {
                body = PSystemManager.Instance.localBodies.Find(b => b.scaledBody.name == transform.parent.name);
                target = KopernicusStar.GetNearest(body).sun.scaledBody.transform;
            }
        }
    }
}