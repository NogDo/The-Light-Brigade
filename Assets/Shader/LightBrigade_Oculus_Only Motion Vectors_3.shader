Shader "LightBrigade/Oculus/Only Motion Vectors" {
	Properties {
		[Header(Blend)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("_Cull", Float) = 2
		[Enum(Off,0,On,1)] _ZWrite ("_ZWrite", Float) = 1
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass ("_StencilOpPass", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpFail ("_StencilOpFail", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 0
		[Header(MotionVectors)] [Toggle(DisableMotionVectors)] _DisableMotionVectors ("Disable Motion Vectors", Float) = 0
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