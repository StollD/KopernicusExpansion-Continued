using System;
using UnityEngine;

namespace KopernicusExpansion
{
    namespace Geometry
    {
        public class UVSphere
        {
            private Mesh mesh;

            //constructor
            public UVSphere(Single radius, Int32 latitudeLines, Int32 longitudeLines)
            {
                //Code taken from - http://wiki.unity3d.com/index.php/ProceduralPrimitives#C.23_-_Sphere

                //Radius
                Single rad = radius;
                //Longitude
                Int32 nbLong = longitudeLines;
                //Latitude
                Int32 nbLat = latitudeLines;

                mesh = new Mesh();

                #region Vertices
                Vector3[] vertices = new Vector3[(nbLong + 1) * nbLat + 2];
                Single _pi = Mathf.PI;
                Single _2pi = _pi * 2f;

                vertices[0] = Vector3.up * rad;
                for (Int32 lat = 0; lat < nbLat; lat++)
                {
                    Single a1 = _pi * (Single)(lat + 1) / (nbLat + 1);
                    Single sin1 = Mathf.Sin(a1);
                    Single cos1 = Mathf.Cos(a1);

                    for (Int32 lon = 0; lon <= nbLong; lon++)
                    {
                        Single a2 = _2pi * (Single)(lon == nbLong ? 0 : lon) / nbLong;
                        Single sin2 = Mathf.Sin(a2);
                        Single cos2 = Mathf.Cos(a2);

                        vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * rad;
                    }
                }
                vertices[vertices.Length - 1] = Vector3.up * -rad;
                #endregion

                #region Normales		
                Vector3[] normales = new Vector3[vertices.Length];
                for (Int32 n = 0; n < vertices.Length; n++)
                    normales[n] = vertices[n].normalized;
                #endregion

                #region UVs
                Vector2[] uvs = new Vector2[vertices.Length];
                uvs[0] = Vector2.up;
                uvs[uvs.Length - 1] = Vector2.zero;
                for (Int32 lat = 0; lat < nbLat; lat++)
                    for (Int32 lon = 0; lon <= nbLong; lon++)
                        uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((Single)lon / nbLong, 1f - (Single)(lat + 1) / (nbLat + 1));
                #endregion

                #region Triangles
                Int32 nbFaces = vertices.Length;
                Int32 nbTriangles = nbFaces * 2;
                Int32 nbIndexes = nbTriangles * 3;
                Int32[] triangles = new Int32[nbIndexes];

                //Top Cap
                Int32 i = 0;
                for (Int32 lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = lon + 2;
                    triangles[i++] = lon + 1;
                    triangles[i++] = 0;
                }

                //Middle
                for (Int32 lat = 0; lat < nbLat - 1; lat++)
                {
                    for (Int32 lon = 0; lon < nbLong; lon++)
                    {
                        Int32 current = lon + lat * (nbLong + 1) + 1;
                        Int32 next = current + nbLong + 1;

                        triangles[i++] = current;
                        triangles[i++] = current + 1;
                        triangles[i++] = next + 1;

                        triangles[i++] = current;
                        triangles[i++] = next + 1;
                        triangles[i++] = next;
                    }
                }

                //Bottom Cap
                for (Int32 lon = 0; lon < nbLong; lon++)
                {
                    triangles[i++] = vertices.Length - 1;
                    triangles[i++] = vertices.Length - (lon + 2) - 1;
                    triangles[i++] = vertices.Length - (lon + 1) - 1;
                }
                #endregion

                mesh.vertices = vertices;
                mesh.normals = normales;
                mesh.uv = uvs;
                mesh.triangles = triangles;

                mesh.RecalculateBounds();
            }

            public static implicit operator Mesh(UVSphere sphere)
            {
                return sphere.mesh;
            }
        }
    }
}

