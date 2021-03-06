﻿Shader "Custom/VertexAnimation" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Diffuse (RGB)", 2D) = "white" {}
		_AnimTex ("Animation Texture", 2D) = "white" {}
		_AnimSpeed("Speed in Seconds to Complete", Float) = 1
		_Amount("Amount to displace", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		pass {
			CGPROGRAM
			#pragma glsl
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

			struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;				
			};

			struct v2f {
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

            float4 _MainTex_ST;
			sampler2D _MainTex;
			sampler2D _AnimTex;
			half _AnimSpeed;
			half _Amount;
			fixed4 _Color;

			v2f vert (appdata v)
            {
				v2f o;
				
				float3 vertexAnim =  tex2Dlod(_AnimTex, half4(1,0.5,0,0));
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex + vertexAnim);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
			ENDCG
		}
	}
	FallBack "Diffuse"
}
