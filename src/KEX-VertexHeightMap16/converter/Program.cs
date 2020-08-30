using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace KopernicusExpansion
{
    namespace VertexHeightMap16
    {
        internal class Program
        {
            public static void Main(String[] args)
            {
                Console.WriteLine("Please make sure the following conditions are met: ");
                Console.WriteLine("    * The texture is a greyscale .raw file, with only one channel saved.");
                Console.WriteLine("    * The texture is 2 x 1");
                Console.WriteLine("    * The texture is exported with Macintosh byte order");
                Console.WriteLine("Please select the bit-size of the input texture:");
                Console.WriteLine("(1) - 16-bit (for use with VertexHeightMap16)");
                Console.WriteLine("(2) - 32-bit (will be converted down to 24-bit for use with VertexHeightMap24)");
                Console.WriteLine("(Press either '1' or '2' on your keyboard)");
                char inK = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if(inK != '1' && inK != '2')
                {
                    Console.WriteLine("Not recognized: " + inK);
                    return;
                }
                bool bits24 = inK == '2';

                // Load the texture
                if (args.Length == 0 || !File.Exists(args[0]))
                {
                    Console.WriteLine("ERROR: No texture found!");
                    return;
                }

                String texturePath = args[0];
                Byte[] data = File.ReadAllBytes(texturePath);


                // Create a new texture
                Int32 height = (Int32) Math.Sqrt(data.Length / (bits24 ? 8d : 4d));
                Int32 width = 2 * height;
                Bitmap newImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                for (Int32 x = 0; x < width; x++)
                {
                    for (Int32 y = 0; y < height; y++)
                    {
                        Int32 index = (y * width + x) * 2;
                        if (bits24) index *= 2;
                        if (bits24)
                        {
                            newImage.SetPixel(x, y, Color.FromArgb(255, data[index], data[index + 1], data[index + 2]));
                        }
                        else
                        {
                            newImage.SetPixel(x, y, Color.FromArgb(255, 0, data[index], data[index + 1]));
                        }
                    }
                }

                // Save the new texture
                newImage.Save(texturePath + ".png", ImageFormat.Png);

                // We are done
                Console.WriteLine("Export finished! Path: " + texturePath + ".png");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}