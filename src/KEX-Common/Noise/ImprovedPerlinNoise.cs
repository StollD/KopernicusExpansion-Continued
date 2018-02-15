using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KopernicusExpansion
{
    namespace Noise
    {
        public class ImprovedPerlinNoise
        {
            const Int32 SIZE = 256;
            Int32[] m_perm = new Int32[SIZE + SIZE];
            Texture2D m_permTable1D, m_permTable2D, m_gradient2D, m_gradient3D, m_gradient4D;

            public Texture2D GetPermutationTable1D() { return m_permTable1D; }
            public Texture2D GetPermutationTable2D() { return m_permTable2D; }
            public Texture2D GetGradient2D() { return m_gradient2D; }
            public Texture2D GetGradient3D() { return m_gradient3D; }
            public Texture2D GetGradient4D() { return m_gradient4D; }

            public ImprovedPerlinNoise(Int32 seed)
            {
                Random.InitState(seed);

                Int32 i, j, k;
                for (i = 0; i < SIZE; i++)
                {
                    m_perm[i] = i;
                }

                while (--i != 0)
                {
                    k = m_perm[i];
                    j = Random.Range(0, SIZE);
                    m_perm[i] = m_perm[j];
                    m_perm[j] = k;
                }

                for (i = 0; i < SIZE; i++)
                {
                    m_perm[SIZE + i] = m_perm[i];
                }

            }

            public void LoadResourcesFor2DNoise()
            {
                LoadPermTable1D();
                LoadGradient2D();
            }

            public void LoadResourcesFor3DNoise()
            {
                LoadPermTable2D();
                LoadGradient3D();
            }

            public void LoadResourcesFor4DNoise()
            {
                LoadPermTable1D();
                LoadPermTable2D();
                LoadGradient4D();
            }

            void LoadPermTable1D()
            {
                if (m_permTable1D) return;

                m_permTable1D = new Texture2D(SIZE, 1, TextureFormat.Alpha8, false, true);
                m_permTable1D.filterMode = FilterMode.Point;
                m_permTable1D.wrapMode = TextureWrapMode.Repeat;

                for (Int32 x = 0; x < SIZE; x++)
                {
                    m_permTable1D.SetPixel(x, 1, new Color(0, 0, 0, (Single)m_perm[x] / (Single)(SIZE - 1)));
                }

                m_permTable1D.Apply();
            }

            //This is special table that has been optimesed for 3D noise. It can also be use in 4D noise for some optimisation but the 1D perm table is still needed 
            void LoadPermTable2D()
            {
                if (m_permTable2D) return;

                m_permTable2D = new Texture2D(SIZE, SIZE, TextureFormat.ARGB32, false, true);
                m_permTable2D.filterMode = FilterMode.Point;
                m_permTable2D.wrapMode = TextureWrapMode.Repeat;

                for (Int32 x = 0; x < SIZE; x++)
                {
                    for (Int32 y = 0; y < SIZE; y++)
                    {
                        Int32 A = m_perm[x] + y;
                        Int32 AA = m_perm[A];
                        Int32 AB = m_perm[A + 1];

                        Int32 B = m_perm[x + 1] + y;
                        Int32 BA = m_perm[B];
                        Int32 BB = m_perm[B + 1];

                        m_permTable2D.SetPixel(x, y, new Color((Single)AA / 255.0f, (Single)AB / 255.0f, (Single)BA / 255.0f, (Single)BB / 255.0f));
                    }
                }

                m_permTable2D.Apply();
            }

            void LoadGradient2D()
            {
                if (m_gradient2D) return;

                m_gradient2D = new Texture2D(8, 1, TextureFormat.RGB24, false, true);
                m_gradient2D.filterMode = FilterMode.Point;
                m_gradient2D.wrapMode = TextureWrapMode.Repeat;

                for (Int32 i = 0; i < 8; i++)
                {
                    Single R = (GRADIENT2[i * 2 + 0] + 1.0f) * 0.5f;
                    Single G = (GRADIENT2[i * 2 + 1] + 1.0f) * 0.5f;

                    m_gradient2D.SetPixel(i, 0, new Color(R, G, 0, 1));
                }

                m_gradient2D.Apply();

            }

            void LoadGradient3D()
            {
                if (m_gradient3D) return;

                m_gradient3D = new Texture2D(SIZE, 1, TextureFormat.RGB24, false, true);
                m_gradient3D.filterMode = FilterMode.Point;
                m_gradient3D.wrapMode = TextureWrapMode.Repeat;

                for (Int32 i = 0; i < SIZE; i++)
                {
                    Int32 idx = m_perm[i] % 16;

                    Single R = (GRADIENT3[idx * 3 + 0] + 1.0f) * 0.5f;
                    Single G = (GRADIENT3[idx * 3 + 1] + 1.0f) * 0.5f;
                    Single B = (GRADIENT3[idx * 3 + 2] + 1.0f) * 0.5f;

                    m_gradient3D.SetPixel(i, 0, new Color(R, G, B, 1));
                }

                m_gradient3D.Apply();

            }

            void LoadGradient4D()
            {
                if (m_gradient4D) return;

                m_gradient4D = new Texture2D(SIZE, 1, TextureFormat.ARGB32, false, true);
                m_gradient4D.filterMode = FilterMode.Point;
                m_gradient4D.wrapMode = TextureWrapMode.Repeat;

                for (Int32 i = 0; i < SIZE; i++)
                {
                    Int32 idx = m_perm[i] % 32;

                    Single R = (GRADIENT4[idx * 4 + 0] + 1.0f) * 0.5f;
                    Single G = (GRADIENT4[idx * 4 + 1] + 1.0f) * 0.5f;
                    Single B = (GRADIENT4[idx * 4 + 2] + 1.0f) * 0.5f;
                    Single A = (GRADIENT4[idx * 4 + 3] + 1.0f) * 0.5f;

                    m_gradient4D.SetPixel(i, 0, new Color(R, G, B, A));
                }

                m_gradient4D.Apply();

            }

            static Single[] GRADIENT2 = new Single[]
            {
            0, 1,
            1, 1,
            1, 0,
            1, -1,
            0, -1,
            -1, -1,
            -1, 0,
            -1, 1,
            };

            static Single[] GRADIENT3 = new Single[]
            {
            1,1,0,
            -1,1,0,
            1,-1,0,
            -1,-1,0,
            1,0,1,
            -1,0,1,
            1,0,-1,
            -1,0,-1,
            0,1,1,
            0,-1,1,
            0,1,-1,
            0,-1,-1,
            1,1,0,
            0,-1,1,
            -1,1,0,
            0,-1,-1,
            };

            static Single[] GRADIENT4 = new Single[]
            {
            0, -1, -1, -1,
            0, -1, -1, 1,
            0, -1, 1, -1,
            0, -1, 1, 1,
            0, 1, -1, -1,
            0, 1, -1, 1,
            0, 1, 1, -1,
            0, 1, 1, 1,
            -1, -1, 0, -1,
            -1, 1, 0, -1,
            1, -1, 0, -1,
            1, 1, 0, -1,
            -1, -1, 0, 1,
            -1, 1, 0, 1,
            1, -1, 0, 1,
            1, 1, 0, 1,

            -1, 0, -1, -1,
            1, 0, -1, -1,
            -1, 0, -1, 1,
            1, 0, -1, 1,
            -1, 0, 1, -1,
            1, 0, 1, -1,
            -1, 0, 1, 1,
            1, 0, 1, 1,
            0, -1, -1, 0,
            0, -1, -1, 0,
            0, -1, 1, 0,
            0, -1, 1, 0,
            0, 1, -1, 0,
            0, 1, -1, 0,
            0, 1, 1, 0,
            0, 1, 1, 0,
            };

        }
    }
}