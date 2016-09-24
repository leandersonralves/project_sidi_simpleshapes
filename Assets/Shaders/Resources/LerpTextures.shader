Shader "Custom/LerpTextures" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SecondTex ("Second Albedo (RGB)", 2D) = "white" {}
		_LerpTextures ("Interpolation value between Texs", Range(0,1)) = 0.5
		_NormalMap ("Normal Map ", 2D) = "bump" {}
		_SpecularMap ("Specular Map ", 2D) = "specular" {}
		_Specular ("Specular", Float) = 1
		_Smoothness ("Gloss", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecondTex;
		sampler2D _NormalMap;
		sampler2D _SpecularMap;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
			float2 uv_NormalMap;
			float2 uv_SpecularMap;
		};

		half _Smoothness;
		half _Metallic;
		half _LerpTextures;
		half _Specular;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {

			// Albedo comes from a texture tinted by color
			fixed4 c = lerp(tex2D (_MainTex, IN.uv_MainTex), tex2D (_SecondTex, IN.uv_SecondTex), _SinTime.w * .5 + .5) * _Color;
			o.Albedo = c.rgb;
			o.Specular = _Specular * tex2D(_SpecularMap, IN.uv_SpecularMap).a;
			o.Normal = UnpackNormal (tex2D(_NormalMap, IN.uv_NormalMap));
			o.Emission = 0;
			o.Smoothness = _Smoothness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
