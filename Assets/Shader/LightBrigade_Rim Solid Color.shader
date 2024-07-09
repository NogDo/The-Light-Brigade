Shader "LightBrigade/Rim Solid Color" {
	Properties {
		[HDR] _Color ("Color", Vector) = (1,1,1,1)
		[HDR] _ColorInside ("Color Inside", Vector) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0.1, 8)) = 0.5
		[Header(Wobble)] [Toggle(EnableWobble)] _EnableWobble ("Enable Wobble", Float) = 0
		_WobbleDistance ("Wobble Distance", Range(0, 1)) = 0.05
		_WobbleFreq ("Wobble Freq", Range(0, 300)) = 300
		_WobbleSpeed ("Wobble Speed", Range(0, 2)) = 1
		[Toggle(EnablePostProcess)] _EnablePostProcess ("Enable Post-Process", Float) = 1
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass ("_StencilOpPass", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpFail ("_StencilOpFail", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 0
		[Header(MotionVectors)] [Toggle(DisableMotionVectors)] _DisableMotionVectors ("Disable Motion Vectors", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}