using KopernicusExpansion.Noise;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace ProceduralGasGiants
    {
        namespace Effects
        {
            public class ProceduralGasGiant : MonoBehaviour
            {
                private const float MaxTime = 10000;

                public int seed = 0;
                public float speed = 0.002f;
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

                private float currentTime = 0f;
                void LateUpdate()
                {
                    if (doAnimate)
                    {
                        // will only speed animation up to 5000x timewarp speed
                        currentTime += Time.deltaTime * Mathf.Min(TimeWarp.CurrentRate, 5000f) * speed;
                        if (currentTime >= MaxTime)
                            currentTime = 0;

                        Renderer renderer = GetComponent<Renderer>();
                        renderer.material.SetFloat("_Evolution", currentTime);
                    }
                }
            }
        }
    }
}
