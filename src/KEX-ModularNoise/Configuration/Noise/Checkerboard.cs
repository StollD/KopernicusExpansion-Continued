using Kopernicus;

namespace KopernicusExpansion
{
    namespace ModularNoise
    {
        namespace Configuration
        {
            [RequireConfigType(ConfigType.Node)]
            public class Checkerboard : NoiseLoader<LibNoise.Checkerboard>
            {
                
            }
        }
    }
}