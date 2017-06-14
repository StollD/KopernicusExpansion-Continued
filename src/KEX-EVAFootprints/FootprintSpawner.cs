using Kopernicus.Components;
using Kopernicus.Configuration;
using Kopernicus.Constants;
using KopernicusExpansion.Geometry;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace KopernicusExpansion
{
    namespace EVAFootprints
    {
        [KSPAddon(KSPAddon.Startup.Flight, false)]
        public class FootprintSpawner : MonoBehaviour
        {
            // A list of all bodies where kerbals leave footprints
            public static List<String> FootprintsAllowed = new List<String>();

            public static GameObject footprintPrefab { get; private set; }
            private static void SetupFootprintPrefab()
            {
                footprintPrefab = new GameObject("KerbalEVAFootprint");
                footprintPrefab.layer = GameLayers.LocalSpace;
                footprintPrefab.SetActive(false);

                MeshFilter mf = footprintPrefab.AddComponent<MeshFilter>();
                MeshRenderer mr = footprintPrefab.AddComponent<MeshRenderer>();

                mf.mesh = new Quad(0.15f, 0.3f, true);

                Material material = new Material(ShaderLoader.GetShader("KopernicusExpansion/Footprint"));
                Texture2DParser footprintMask = new Texture2DParser();
                footprintMask.SetFromString("KopernicusExpansion/Textures/KerbalEVAFootprintMask");
                material.SetTexture("_MainTex", footprintMask);
                material.SetFloat("_Opacity", 0.8f);
                material.SetColor("_Color", Color.black);
                mr.material = material;
                mr.shadowCastingMode = ShadowCastingMode.Off;
                footprintPrefab.AddComponent<EVAFootprint>();
                Debug.Log("[KopernicusExpansion] Footprint prefab created");
            }

            public static int TotalFootprints
            {
                get
                {
                    if (footprints == null)
                        return 0;
                    return footprints.Count;
                }
            }

            // All footprints that are currently active
            private static Queue<EVAFootprint> footprints;

            public static void AddFootprint(EVAFootprint footprint)
            {
                if (TotalFootprints >= 1024)
                {
                    EVAFootprint footprintToDie = footprints.Dequeue();
                    footprintToDie.DestroyFootprint();
                }

                if (footprints != null)
                    footprints.Enqueue(footprint);
                else
                    Debug.LogError("[KopernicusExpansion] Footprints queue is null");
            }

            void Start()
            {
                if (footprintPrefab == null)
                {
                    SetupFootprintPrefab();
                }

                footprints = new Queue<EVAFootprint>(1024);

                GameEvents.onPartUnpack.Add(AddFootprinter);
                GameEvents.onCrewOnEva.Add(OnCrewOnEVA);
            }

            void OnDestroy()
            {
                GameEvents.onPartUnpack.Remove(AddFootprinter);
                GameEvents.onCrewOnEva.Remove(OnCrewOnEVA);
            }

            void OnCrewOnEVA(GameEvents.FromToAction<Part, Part> fromto)
            {
                AddFootprinter(fromto.to);
            }

            void AddFootprinter(Part part)
            {
                if (part.GetComponent<KerbalEVA>() != null)
                {
                    var footprinter = part.gameObject.AddComponent<Footprinter>();
                    footprinter.eva = part.GetComponent<KerbalEVA>();
                    footprinter.part = part;

                    Debug.Log("[KopernicusExpansion] Footprinter added to " + part.name + " of vessel " + part.vessel.vesselName);
                }
            }
        }
    }
}