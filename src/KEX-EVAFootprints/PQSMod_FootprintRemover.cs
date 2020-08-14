using System;
using System.Collections.Generic;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace EVAFootprints
    {
        // a PQSMod to remove all footprints when the quad is recycled
        public class PQSMod_FootprintRemover : PQSMod
        {
            public override void OnQuadDestroy(PQ quad)
            {
                foreach (EVAFootprint footprint in quad.GetComponentsInChildren<EVAFootprint>(true))
                {
                    Destroy(footprint.gameObject);
                }
            }
        }
    }
}