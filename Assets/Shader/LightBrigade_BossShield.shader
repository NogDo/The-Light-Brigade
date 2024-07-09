Shader "LightBrigade/BossShield" {
	Properties {
		[HDR] _ColorBottom ("Color Bottom", Vector) = (1,1,1,1)
		[HDR] _ColorTop ("Color Top", Vector) = (1,1,1,1)
		[HDR] _ColorInside ("Color Inside", Vector) = (1,1,1,1)
		_RimPower ("Rim Power", Range(0.1, 8)) = 0.5
		_EmissionFogOffset ("_EmissionFogOffset", Float) = 0
		[Header(Extrusion)] _ExtrudeDistance ("Extrude distance (normals)", Range(0, 1)) = 0.25
		[Header(Wobble)] _WobbleDistance ("Wobble Distance", Range(0, 1)) = 0.05
		_WobbleFreq ("Wobble Freq", Range(0, 300)) = 300
		_WobbleSpeed ("Wobble Speed", Range(0, 2)) = 1
		[Header(Dissolve)] [Toggle] _DISSOLVE_EFFECT ("_DISSOLVE_EFFECT toggle", Float) = 0
		[Toggle] _DISSOLVE_EFFECT_AXIS_Z ("_DISSOLVE_EFFECT_AXIS_Z toggle", Float) = 0
		_DissoveAway ("_DissoveAway", Range(0, 1)) = 0
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("_SrcBlend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("_DstBlend", Float) = 0
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