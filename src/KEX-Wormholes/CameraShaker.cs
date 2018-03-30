using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KopernicusExpansion
{
    namespace Wormholes
    {
        public class CameraShake : MonoBehaviour
        {
            // Transform of the camera to shake. Grabs the gameObject's transform
            // if null.
            private Transform _camTransform;

            // Amplitude of the shake. A larger value shakes the camera harder.
            public Single ShakeAmount = 0.7f;

            private Vector3 _originalPos;

            void Awake()
            {
                _camTransform = FlightCamera.fetch.mainCamera.transform;
            }

            void Start()
            {
                _originalPos = _camTransform.localPosition;
            }

            void Update()
            {
                _camTransform.localPosition = _originalPos + Random.insideUnitSphere * ShakeAmount;
            }

            private void OnDestroy()
            {
                _camTransform.localPosition = _originalPos;
            }
        }

    }

}