Shader "LightBrigade/PortalZone Entry Exit" {
	Properties {
		[Header(Portal Base RED VERTEX COLOR)] [HDR] _AlbedoCenter ("Portal Center", Vector) = (1,1,1,1)
		[HDR] _AlbedoEdge ("Portal Edge", Vector) = (1,1,1,1)
		[Header(Effects Texture)] [NoScaleOffset] _EffectsRGBTex ("Texture (RGB)", 2D) = "white" {}
		[Header(Layer Caustic)] [HDR] _CausticTint ("Tint", Vector) = (1,1,1,1)
		_Parallax ("Parallax Height", Range(-5, 5)) = 0
		_TilingX ("Tiling X", Range(-2, 2)) = 1
		_TilingY ("Tiling Y", Range(-2, 2)) = 1
		_CausticSpeed ("Scroll Speed (Y)", Range(-10, 10)) = 2
		[Header(Layer 1 Oil)] _ShinyTextureScale ("Texture ScaleOffset", Vector) = (2,2,0,0)
		_ShinyParallax ("Parallax Height", Range(-5, 5)) = 0
		_ShinySpeed ("Scroll Speed (Y)", Range(-5, 5)) = 1
		_ShinyCausticDistortion ("Caustic Distortion", Range(0, 1)) = 0.1
		[HDR] _ShinySparklesTint ("Tint", Vector) = (1,1,1,1)
		[Header(Layer 2 Stars)] _StarsTextureScale ("Texture ScaleOffset", Vector) = (2,2,0,0)
		_StarsParallax ("Parallax Height", Range(-5, 5)) = 0
		_StarsSpeed ("Scroll Speed (Y)", Range(-5, 5)) = 1
		_StarsCausticDistortion ("Caustic Distortion", Range(0, 1)) = 0.1
		[HDR] _StarsSparklesTint ("Tint", Vector) = (1,1,1,1)
		[Header(Fade)] _FadeDepthZ ("Fade Depth Z", Range(0, 5)) = 0.5
		_FadeCausticStrength ("Fade Caustic Strength", Range(0, 1)) = 1
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 2
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 0
		[Header(Write Mask)] [Enum(Off,0,On,1)] _ZWrite ("_ZWrite", Float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("_ZTest", Float) = 4
		[Header(Debug)] [Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Show Vertex Colors", Float) = 0
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