Shader "LightBrigade/LB_XRFade" {
	Properties {
		_Color_Circle ("Color (Inner)", Vector) = (1,1,1,0)
		_ColorOuter_Circle ("Color (Outer)", Vector) = (1,1,1,1)
		_InnerDistance_Circle ("Inner Radius", Range(0, 1)) = 0
		_OuterDistance_Circle ("Outer Radius", Range(0, 2)) = 1
		_Color_Fullscreen ("Color (Fullscreen)", Vector) = (0,0,0,0)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}