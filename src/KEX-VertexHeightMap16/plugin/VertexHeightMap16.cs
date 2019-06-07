using System;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
using Kopernicus.Configuration;
using Kopernicus.Configuration.ModLoader;
using Kopernicus.Configuration.Parsing;

namespace KopernicusExpansion
{
    namespace VertexHeightMap32
    {
        public class VertexHeightMap16 : ModLoader<PQSMod_VertexHeightMap16>
        {    
            // The map texture for the planet
            [ParserTarget("map")]
            public MapSOParserGreyScale<MapSO> heightMap
            {
                get { return Mod.heightMap; }
                set { Mod.heightMap = value; }
            }

            // Height map offset
            [ParserTarget("offset")]
            public NumericParser<Double> heightMapOffset 
            {
                get { return Mod.heightMapOffset; }
                set { Mod.heightMapOffset = value; }
            }

            // Height map offset
            [ParserTarget("deformity")]
            public NumericParser<Double> heightMapDeformity
            {
                get { return Mod.heightMapDeformity; }
                set { Mod.heightMapDeformity = value; }
            }

            // Height map offset
            [ParserTarget("scaleDeformityByRadius")]
            public NumericParser<Boolean> scaleDeformityByRadius
            {
                get { return Mod.scaleDeformityByRadius; }
                set { Mod.scaleDeformityByRadius = value; }
            }
        }
    }
}