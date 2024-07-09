Shader "LightBrigade/Terrain Tiles WFC" {
	Properties {
		[Header(Toon Noise Distortion)] [Toggle(EnableNoiseDistortion)] _EnableNoiseDistortion ("Enable Distortion (Uses NoiseMap's RED Channel)", Float) = 0
		[NoScaleOffset] _ShadowOffsetMap ("Noise Map (R=Toon Noise, G=Snow, B=Dirt, A=Perlin)", 2D) = "white" {}
		_ShadowStepDistortionSettings ("Noise Settings (Frequency, Strength, Biplanar-K, Noise Softness)", Vector) = (1,0.1,2,0.1)
		[Header(Toon Lighting)] _ShadowStepSettings ("Step Settings (Shadow Brightness, Midtone Brightness, Highlight Brightness, Threshold)", Vector) = (0,0.75,1,0.8)
		[Header(Primary Layer GREEN Channel)] [HDR] _PrimaryTint0 ("Tint Color 0", Vector) = (1,1,1,1)
		[HDR] _PrimaryTint1 ("Tint Color 1", Vector) = (1,1,1,1)
		[HDR] _PrimaryFresnelStrength ("Primary Fresnel Strength", Range(0, 1)) = 0.2
		[HDR] _PrimarySpecularStrength ("Primary Specular Strength", Range(0, 1)) = 0.2
		_SpecularSmoothness ("Specular Smoothness", Range(0, 1)) = 0.9
		_FresnelTightness ("Overall Fresnel Tightness", Range(0, 10)) = 8
		[Header(Secondary Layer BLUE Chanel)] [HDR] _SecondaryTint0 ("Tint Color 0", Vector) = (1,1,1,1)
		[HDR] _SecondaryTint1 ("Tint Color 1", Vector) = (1,1,1,1)
		_SecondaryNoiseSettings ("Secondary Noise Settings (x=Step Blend, y=Inner Threshold, z=Outer Threshold, w=perlin strength)", Vector) = (0.7,1.2,1.35,0.2)
		[Header(Snow Shiny Sparkles)] [Toggle(EnableShinySparkles)] _EnableShinySparkles ("Enable Shiny Sparkles", Float) = 1
		_SnowShiniesMap ("Snow Shinies (RGB)", 2D) = "black" {}
		_SnowShiniesStrength ("Snow Shinies Strength", Range(0, 5000)) = 1000
		_SnowShiniesSpeed ("Snow Shinies Speed", Range(0, 2)) = 0.1
		[Header(Vertex Noise)] _VertexNoiseFreq ("Vertex Noise Freq", Range(0, 10)) = 0.15
		_VertexNoiseStrength ("Vertex Noise Strength (strength-xyz, normal strength W)", Vector) = (0,0.1,0,1)
		[Header(Debug)] [Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Show Vertex Colors", Float) = 0
		[Toggle(DisableMotionVectors)] _DisableMotionVectors ("Disable Motion Vectors", Float) = 0
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