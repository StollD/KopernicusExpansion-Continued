using System;
using Kopernicus;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        public class VertexHeightMap16 : ModLoader<PQSMod_VertexHeightMap16>
        {    
            // The map texture for the planet
            [ParserTarget("map")]
            public MapSOParser_GreyScale<MapSO> heightMap
            {
                get { return mod.heightMap; }
                set { mod.heightMap = value; }
            }

            // Height map offset
            [ParserTarget("offset")]
            public NumericParser<Double> heightMapOffset 
            {
                get { return mod.heightMapOffset; }
                set { mod.heightMapOffset = value; }
            }

            // Height map offset
            [ParserTarget("deformity")]
            public NumericParser<Double> heightMapDeformity
            {
                get { return mod.heightMapDeformity; }
                set { mod.heightMapDeformity = value; }
            }

            // Height map offset
            [ParserTarget("scaleDeformityByRadius")]
            public NumericParser<Boolean> scaleDeformityByRadius
            {
                get { return mod.scaleDeformityByRadius; }
                set { mod.scaleDeformityByRadius = value; }
            }
        }
    }
}