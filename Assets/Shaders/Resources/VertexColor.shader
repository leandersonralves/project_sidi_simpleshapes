Shader "Custom/VertexColor" {
	Properties {
		_Color ("Sum Color", Color) = (0,0,0,1)
	}
	SubShader {
	    Pass {
	        Fog { Mode Off }
	        CGPROGRAM

	        #pragma vertex vert
	        #pragma fragment frag

	        // vertex input: position, normal, tangent
	        struct appdata {
	            half4 vertex : POSITION;
	            fixed4 color : COLOR;
	        };

	        struct v2f {
	            half4 pos : SV_POSITION;
	            fixed4 color : COLOR;
	        };

	        fixed4 _Color;

	        v2f vert (appdata v) {
	            v2f o;
	            o.pos = mul( UNITY_MATRIX_MVP, v.vertex );
	            o.color = v.color + _Color;
	            return o;
	        }
	        
	        fixed4 frag (v2f i) : COLOR0 { return i.color; }
	        ENDCG
	    }
	}
}
