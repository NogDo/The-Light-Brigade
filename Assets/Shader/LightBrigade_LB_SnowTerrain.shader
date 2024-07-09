Shader "LightBrigade/LB_SnowTerrain" {
	Properties {
		[Header(Snow)] _SnowTint ("Snow Tint Color", Vector) = (1,1,1,1)
		_SnowSettings ("Snow Threshold (Min, Max, NoiseFreq, NoiseStrength)", Vector) = (0.5,1,0,0)
		_SnowSpecularColor ("Snow Specular Color", Vector) = (1,1,1,1)
		[Header(Snow Shiny Sparkles)] [Toggle(EnableShinySparkles)] _EnableShinySparkles ("Enable Shiny Sparkles", Float) = 1
		_SnowShiniesMap ("Snow Shinies (RGB)", 2D) = "black" {}
		_SnowShiniesStrength ("Snow Shinies Strength", Range(0, 5000)) = 1000
		[Header(Terrain Noise Layers)] _PerlinMap ("Noise Map (RGB)", 2D) = "white" {}
		_NoiseSampleFreq ("Sample Freq", Range(0, 5000)) = 300
		_NoiseSampleStrength ("Sample Strength", Range(0, 1)) = 0.5
		_NoiseLayersStepSize ("Layers Step Size", Range(0, 1)) = 0.33
		[Header(Fresnel)] [Toggle(EnableFresnel)] _EnableFresnel ("Enable Fresnel", Float) = 0
		[HDR] _FresnelTint ("Fresnel Tint", Vector) = (1,1,1,1)
		_FresnelTightness ("Fresnel Tightness", Range(0, 10)) = 1
		[Header(Lighting)] _LightingNoiseStrength ("Lighting Noise Strength (uses Noise Map R-Channel)", Range(0, 10)) = 1
		[Header(Light Map)] [Toggle(EnableLightMap)] _EnableLightMap ("Enable Light Map", Float) = 0
		[Toggle(EnableLightMapDebug)] _EnableLightMapDebug ("Show Light Map Only (Debug)", Float) = 0
		_LightMapTint ("Light Map Tint", Vector) = (1,1,1,1)
		_LightMapStrength ("Light Map Strength", Range(0, 1)) = 1
		[Header(Shadow)] _ShadowStrength ("Shadow Strength", Range(0, 1)) = 0.25
		[Toggle(EnableShadowMask)] _EnableShadowMask ("Enable Shadow Mask (Need Light Map enabled too)", Float) = 0
		[Toggle(EnableShadowMaskDebug)] _EnableShadowMaskDebug ("Show Shadow Mask Only (Debug)", Float) = 0
		_ShadowMaskSettings ("Threshold (Outer Edge Threshold, Inner Threshold, Strength)", Vector) = (0.4,0.5,1,1)
		[Header(Debug)] [Toggle(EnableDebugShowMaskColors)] _EnableDebugShowMaskColors ("Show Mask Colors", Float) = 0
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