using System;
using Kopernicus;
using Kopernicus.ConfigParser.Attributes;
using Kopernicus.ConfigParser.BuiltinTypeParsers;
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
                get { return Mod.deformity; }
                set { Mod.deformity = value; }
            }

            // Whether the deformity should get multiplied by the sphere radius
            [ParserTarget("scaleDeformityByRadius")]
            public NumericParser<Boolean> scaleDeformityByRadius
            {
                get { return Mod.scaleDeformityByRadius; }
                set { Mod.scaleDeformityByRadius = value; }
            }
        }
    }
}