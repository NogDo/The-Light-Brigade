Shader "LightBrigade/Unlit/LB Unlit (new)" {
	Properties {
		_BaseMap ("Base Map", 2D) = "white" {}
		_BaseColor ("Base Color", Vector) = (1,1,1,1)
		[HDR] _EmissionColor ("Emission Color", Vector) = (0,0,0,1)
		[Toggle(EnableFunkFog)] _ApplyFog ("Apply Fog", Float) = 0
		[Header(Blend)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("_Cull", Float) = 2
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("_SrcBlend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("_DstBlend", Float) = 0
		[Enum(Off,0,On,1)] _ZWrite ("_ZWrite", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
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