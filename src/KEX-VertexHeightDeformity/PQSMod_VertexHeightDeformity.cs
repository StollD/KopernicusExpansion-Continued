using System;

namespace KopernicusExpansion
{
    namespace VertexHeightDeformity
    {
        /// <summary>
        /// A PQSMod that deformes the terrain by a given value
        /// </summary>
        public class PQSMod_VertexHeightDeformity : PQSMod
        {
            /// <summary>
            /// How much should the terrain get deformed?
            /// </summary>
            public Double deformity;

            /// <summary>
            /// Whether the deformity should get multiplied by the sphere radius
            /// </summary>
            public Boolean scaleDeformityByRadius;

            public override void OnSetup()
            {
                if (scaleDeformityByRadius)
                {
                    deformity *= sphere.radius;
                }
            }

            public override void OnVertexBuildHeight(PQS.VertexBuildData data)
            {
                data.vertHeight *= deformity;
            }
        }
    }
}