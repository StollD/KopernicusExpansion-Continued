using System;
using System.Collections.Generic;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EVAFootprints
    {
        public class EVAFootprint : MonoBehaviour
        {
            public Single FadeTime = 2f;

            void Start()
            {
                FootprintSpawner.AddFootprint(this);
            }

            public void DestroyFootprint()
            {
                StartCoroutine(DestructionRoutine());
            }

            private IEnumerator<YieldInstruction> DestructionRoutine()
            {
                if (FadeTime <= 0f)
                {
                    Destroy(gameObject);
                    yield break;
                }

                Renderer renderer = GetComponent<Renderer>();
                Single startingOpacity = renderer.material.GetFloat("_Opacity");
                Single time = FadeTime;
                while (time > 0f)
                {
                    time -= Time.deltaTime;
                    renderer.material.SetFloat("_Opacity", (time / FadeTime) * startingOpacity);
                    yield return null;
                }

                Destroy(gameObject);
            }

            void OnLevelWasLoaded(Int32 level)
            {
                Destroy(gameObject);
            }
        }
    }
}