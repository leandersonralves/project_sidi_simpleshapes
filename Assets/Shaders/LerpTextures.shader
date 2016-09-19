Shader "Custom/LerpTextures" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecondTex ("Second Albedo (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map ", 2D) = "bump" {}
		_LerpTextures ("Interpolation value between Texs", Range(0,1)) = 0.5
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondTex;
		sampler2D _NormalMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
			float2 uv_NormalMap;
		};

		half _Glossiness;
		half _Metallic;
		half _LerpTextures;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// Albedo comes from a texture tinted by color
			fixed4 c = lerp(tex2D (_MainTex, IN.uv_MainTex), tex2D (_SecondTex, IN.uv_SecondTex), _LerpTextures) * _Color;
			o.Albedo = c.rgb;
			o.Normal = UnpackNormal (tex2D(_NormalMap, IN.uv_NormalMap));

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
