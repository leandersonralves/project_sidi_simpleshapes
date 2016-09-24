Shader "Custom/NormalSpecular" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_NormalMap ("Normal Map", 2D) = "white" {}
		_NormalAmplify ("Color", Float) = 1
		_Specular ("Specular", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf StandardSpecular fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		fixed _NormalAmplify;
		fixed4 _Color;
		sampler2D _NormalMap;
		sampler2D _Specular;

		struct Input {
			float2 uv_Textures;
		};

		void surf (Input IN, inout SurfaceOutputStandardSpecular o) {
			// Albedo comes from a texture tinted by color
			o.Albedo = _Color;
			o.Normal = UnpackNormal (tex2D (_NormalMap, IN.uv_Textures)) * _NormalAmplify;

			//Gloss é o mesmo efeito que o specular.
			o.Specular = tex2D (_Specular, IN.uv_Textures);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
