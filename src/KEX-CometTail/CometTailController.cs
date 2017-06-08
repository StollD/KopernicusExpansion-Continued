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
            private const float MaxTime = 10000;

            public CometTailType type;
            public Orbit orbit;
            public string targetBodyName;
            public Color color;

            public FloatCurve opacityCurve;
            public FloatCurve brightnessCurve;

            private Transform target;

            private float currentTime = 0f;
            public int seed = 0;
            public float speed = 0.05f;
            public bool doAnimate = true;

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
                if (HighLogic.LoadedScene != GameScenes.FLIGHT && HighLogic.LoadedScene != GameScenes.SPACECENTER && HighLogic.LoadedScene != GameScenes.TRACKSTATION)
                    return;

                if (target != null)
                {
                    // look at target
                    var rot = Quaternion.LookRotation((target.position - transform.position).normalized);
                    transform.rotation = rot;

                    // TODO: make dust trails deflect from solar wind
                }
                else
                    GetTarget();
            }

            void LateUpdate()
            {
                if (HighLogic.LoadedScene != GameScenes.FLIGHT && HighLogic.LoadedScene != GameScenes.SPACECENTER && HighLogic.LoadedScene != GameScenes.TRACKSTATION)
                    return;

                if (target != null)
                {
                    // getting data
                    float distance = Vector3.Distance(target.position, transform.position);
                    float opacity = opacityCurve.Evaluate(distance * ScaledSpace.ScaleFactor);
                    float brightness = brightnessCurve.Evaluate(distance * ScaledSpace.ScaleFactor);

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
                    GetTarget();
            }

            /// <summary>
            /// Get the nearest star
            /// </summary>
            void GetTarget()
            {
                if (HighLogic.LoadedScene == GameScenes.PSYSTEM)
                    return;
                CelestialBody body = PSystemManager.Instance.localBodies.Find(b => b.transform.name == transform.name);
                target = KopernicusStar.GetNearest(body).sun.scaledBody.transform;
            }
        }
    }
}