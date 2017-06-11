

Shader "KopernicusExpansion/ProceduralGasGiant" {
	Properties {
		_MainTex ("Ramp Texture", 2D) = "white"{}
		_Evolution ("Time", Vector) = (0.0, 0.0, 0.0, 0.0)
		_StormMap ("Storm Map", 2D) = "white"{}
		_StormFrequency ("Storm Frequency", Float) = 5
		_StormDistortion ("Storm Distortion", Range(0, 0.1)) = 0.05
		_Distortion ("Distortion", Range(0, 0.1)) = 0.02
		_MainFrequency ("Frequency", Float) = 25
		_Lacunarity ("Lacunarity", Float) = 1.3
		_Gain ("Gain", Float) = 0.9

		_PermTable2D ("2D Perm Table", 2D) = "white"{}
		_Gradient3D ("3D Gradient", 2D) = "white"{}

	}
	SubShader {
		Tags { "RenderType"="Opaque" "IgnoreProjector"="True" "ForceNoShadowCasting"="True" }
		LOD 200

		//renders to depth buffer
		Pass {
			ZWrite On
		}
		
		CGPROGRAM
		#pragma target 3.0
		#include "UnityCG.cginc"
		#include "../CommonUtils/ImprovedPerlinNoise.cginc"
		#pragma surface surf Lambert alpha vertex:vert

		//main
		sampler2D _MainTex;
		float _MainFrequency;
		half _Distortion;

		//storm
		sampler2D _StormMap;
		float _StormFrequency;
		half _StormDistortion;

		float3 _Evolution;

		struct Input {
			float3 vertPos;
			float2 uv_StormMap;
		};

		void vert(inout appdata_full v, out Input i)
		{
			UNITY_INITIALIZE_OUTPUT(Input, i);
			i.vertPos = normalize(v.vertex.xyz);
		}

		void surf (Input i, inout SurfaceOutput o)
		{
			//noise functions are fBm, turbulence, and ridgemf. use i.uv.xyz as the first argument, second argument is octaves
			fixed3 c;

			_Frequency = _MainFrequency;

			//main pattern
			float n = ridgedmf(i.vertPos + _Evolution.xyz, 4, inoise(i.vertPos + _Evolution.xyz)) * _Distortion;

			_Frequency = _StormFrequency;

			//storm pattern
			float stormStrength = tex2D(_StormMap, i.uv_StormMap).a;
			float sn4 = fBm(i.vertPos + _Evolution.xyz, 3) * stormStrength * _StormDistortion;

			//color
			float2 mainUV = float2(((i.vertPos.y + n + sn4 + stormStrength) * 0.5) - 0.5, 0);
			c.rgb = tex2D(_MainTex, mainUV).rgb;

			o.Albedo = c;
			o.Alpha = 1;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
