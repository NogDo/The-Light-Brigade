Shader "LightBrigade/Fire" {
	Properties {
		[Header(Inner)] [HDR] _ColorInnerUp ("Inner Up", Vector) = (1,1,1,1)
		[HDR] _ColorInnerBottom ("Inner Bottom", Vector) = (1,1,1,1)
		_InnerScale ("Inner Scale", Range(0, 2)) = 1
		_FireMovementDistanceHorizontalInner ("Inner Distance Horizontal", Range(0, 1)) = 0.7
		_FireMovementDistanceVerticalInner ("Inner Distance Vertical", Range(0, 5)) = 0.5
		_FadeCurveInner ("Fade Curve Inner", Range(0, 1)) = 0.5
		[Header(Outer)] [HDR] _ColorOuterUp ("Outer Up", Vector) = (1,1,1,1)
		[HDR] _ColorOuterBottom ("Outer Bottom", Vector) = (1,1,1,1)
		_FireMovementDistanceHorizontalOuter ("Outer Distance Horizontal", Range(0, 1)) = 0.7
		_FireMovementDistanceVerticalOuter ("Outer Distance Vertical", Range(0, 5)) = 0.5
		_FadeCurveOuter ("Fade Curve Outer", Range(0, 1)) = 0.5
		[Header(Movement)] _NoiseTex ("Noise Texture", 2D) = "black" {}
		_Noise0Speed ("Speed 0", Range(-10, 10)) = 0.5
		_Noise0Freq ("Freq 0", Range(-10, 10)) = 0.5
		_Noise1Speed ("Speed 1", Range(-10, 10)) = 0.5
		_Noise1Freq ("Freq 1", Range(-10, 10)) = 0.5
		[Header(Wind)] [Toggle] _EnableWindSway ("Enable Wind Sway (RED Mask)", Float) = 0
		_WindSwayStrength ("Strength", Range(0, 1)) = 1
		_WindSwayFrequency ("Frequency", Range(0, 10)) = 1
		_WindSwaySpeed ("Speed", Range(0, 10)) = 1
		_WindSwayHeightDistance ("WindSwayHeightDistance", Range(0, 10)) = 1
		[Header(Fresnel Inner)] [HDR] _FresnelInnerColor ("Inner Color", Vector) = (0,0,0,1)
		_FresnelInnerTightness ("Inner Tightness", Range(0, 10)) = 1
		[Header(Fresnel Outer)] [HDR] _FresnelOuterColor ("Outer Color", Vector) = (0,0,0,1)
		_FresnelOuterTightness ("Outer Tigthtness", Range(0, 10)) = 1
		[Header(Impossible Color)] [Toggle] _EnableImpossibleColor ("Enable Impossible Color", Float) = 0
		_ImpossibleColorOffsetRadians ("Hue Offset (Radians)", Range(0, 6.28318)) = 1
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