Shader "Unlit/Overlay"
{
	Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}
SubShader {
    Tags {"Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Overlay"}
    LOD 200

    // extra pass that renders to depth buffer only
    Pass {
    	//Definindo ZTest Always para que o shader sempre sobrescreva o pixel.
        ZTest Always
        ColorMask 0
    }

    // paste in forward rendering passes from Transparent/Diffuse
    UsePass "Transparent/Diffuse/FORWARD"
}
Fallback "Transparent/VertexLit"
}
