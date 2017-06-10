

Shader "KopernicusExpansion/Footprint" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (0.0, 0.0, 0.0)
		_Opacity ("Opacity", Range(0.0, 1.0)) = 1.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		fixed _Opacity;
		fixed3 _Color;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = _Color;
			o.Alpha = c.a * _Opacity;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
