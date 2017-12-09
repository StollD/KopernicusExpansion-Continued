using System;
using Kopernicus;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        public class VertexHeightMap32 : ModLoader<PQSMod_VertexHeightMap32>
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
            
            // Which channels of the map should be used?
            [ParserTarget("depth")]
            public EnumParser<MapSO.MapDepth> depth
            {
                get { return mod.depth; }
                set { mod.depth = value; }
            }
            
            // How large should the difference between the single levels of detail be
            [ParserTarget("exponent")]
            public NumericParser<Double> exponent
            {
                get { return mod.exponent; }
                set { mod.exponent = value; }
            }
        }
    }
}