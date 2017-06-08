

Shader "KopernicusExpansion/CometTail" {
	Properties {
		_TintColor ("Color", Color) = (0.5,0.5,0.5,1)
		_Evolution ("Time", Float) = 0
		_RimPower ("Rim Power", Range(0, 4)) = 0.01
		_Distortion ("Distortion", Range(0, 0.5)) = 0.2
		_ZDistortion ("Z Distortion", Range(0, 1)) = 0.1
		_AlphaDistortion ("Alpha Distortion", Range(0, 1)) = 0.2
		//TODO: make alpha distortion base value configurable, or just make alpha distortion not affected by altitude (in C#)
		_Frequency ("Frequency", Float) = 1
		_Lacunarity ("Lacunarity", Float) = 1.3
		_Gain ("Gain", Float) = 0.9

		_PermTable2D ("2D Perm Table", 2D) = "white"{}
		_Gradient3D ("3D Gradient", 2D) = "white"{}
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" "ForceNoShadowCasting"="True" }
		LOD 200
		Cull Off
	
		CGPROGRAM
		#pragma target 3.0
		#include "UnityCG.cginc"
		#include "../CommonUtils/ImprovedPerlinNoise.cginc"
		#pragma surface surf Unlit alpha vertex:vert

		fixed4 _TintColor;
		float _Evolution;
		half _RimPower;
		half _Distortion;
		half _ZDistortion;
		half _AlphaDistortion;

		half4 LightingUnlit(SurfaceOutput s, half3 lightDir, half atten)
		{
			return half4(s.Albedo, s.Alpha);
		}

		struct Input {
			float3 vertPos;
			float3 viewDir;
		};

		void vert (inout appdata_full v, out Input i)
		{
			UNITY_INITIALIZE_OUTPUT(Input, i);
			i.vertPos = v.vertex.xyz;
		}

		void surf (Input i, inout SurfaceOutput o)
		{
			//calculate noise
			i.vertPos.z *= _ZDistortion;
			float n = fBm(i.vertPos + float3(0, 0, _Evolution), 5);
			half3 color = lerp(_TintColor.rgb, fixed3(n, n, n), _Distortion);

			float3 viewDir = WorldSpaceViewDir(float4(i.vertPos, 0));
			half rim1 = saturate(dot(normalize(viewDir), o.Normal));
			half rim2 = saturate(dot(normalize(viewDir), -o.Normal));
			half rim = max(rim1, rim2);

			o.Albedo = color;
			o.Alpha = lerp(_TintColor.a, n, _AlphaDistortion) * pow(rim, _RimPower);
		}
		ENDCG
	}
	FallBack "Particles/Alpha Blended"
}
