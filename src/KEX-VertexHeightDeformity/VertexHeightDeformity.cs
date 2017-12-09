using System;
using Kopernicus;
using Kopernicus.Configuration.ModLoader;

namespace KopernicusExpansion
{
    namespace VertexHeightDeformity
    {
        public class VertexHeightDeformity : ModLoader<PQSMod_VertexHeightDeformity>
        {    
            // How much should the terrain get deformed?
            [ParserTarget("deformity")]
            public NumericParser<Double> heightMapDeformity
            {
                get { return mod.deformity; }
                set { mod.deformity = value; }
            }

            // Whether the deformity should get multiplied by the sphere radius
            [ParserTarget("scaleDeformityByRadius")]
            public NumericParser<Boolean> scaleDeformityByRadius
            {
                get { return mod.scaleDeformityByRadius; }
                set { mod.scaleDeformityByRadius = value; }
            }
        }
    }
}