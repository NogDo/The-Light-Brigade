Shader "Hidden/LB_PortalMask" {
	Properties {
		_Cull ("Cull", Float) = 2
		_WriteMask ("_WriteMask", Float) = 2
		[Enum(Off,0,On,1)] _ZWrite ("_ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("_ZTest", Float) = 8
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