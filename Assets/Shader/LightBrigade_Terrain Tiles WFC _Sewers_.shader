Shader "LightBrigade/Terrain Tiles WFC [Sewers]" {
	Properties {
		[Header(Toon Noise Distortion)] [Toggle(EnableNoiseDistortion)] _EnableNoiseDistortion ("Enable Distortion (Uses NoiseMap's RED Channel)", Float) = 0
		[NoScaleOffset] _ShadowOffsetMap ("Noise Map (R=Toon Noise, G=Snow, B=Dirt, A=Perlin)", 2D) = "white" {}
		[Header(Toon Lighting)] _ShadowStepDistortionSettings ("Noise Settings (Frequency, Strength, Biplanar-K, Noise Softness)", Vector) = (1,0.1,2,0.1)
		_ShadowStepSettings ("Step Settings (Shadow Brightness, Midtone Brightness, Highlight Brightness, Threshold)", Vector) = (0,0.75,1,0.8)
		_IgnoreLighting ("Ignore Lighting", Range(0, 1)) = 0
		[Header(Primary Layer RED Channel CAVE)] [HDR] _PrimaryTint0 ("Tint Color 0", Vector) = (1,1,1,1)
		[HDR] _PrimaryTint1 ("Tint Color 1", Vector) = (1,1,1,1)
		[Header(Secondary Layer BLUE Chanel BRICK)] [HDR] _SecondaryTint0 ("Tint Color 0", Vector) = (1,1,1,1)
		[HDR] _SecondaryTint1 ("Tint Color 1", Vector) = (1,1,1,1)
		[Header(Water Damage)] [NoScaleOffset] _WaterDamageMap ("Water Damage Map (Modulated by Noise map GREEN)", 2D) = "white" {}
		_WaterDamageFreq ("Water Damage Frequency", Range(0, 10)) = 0.2
		_WaterDamageFade ("Water Damage Fade", Range(0, 10)) = 2
		[Header(Specular)] [HDR] _SpecularTint ("Specular Tint", Vector) = (1,1,1,1)
		_SpecularSmoothness ("Specular Smoothness", Range(0, 1)) = 0.9
		_SpecularNoise ("Specular Noise (Distortion)", Range(0, 1)) = 0
		_SpecularStep ("Specular Step (toon)", Range(0, 1)) = 0.5
		[Toggle(EnableSpecularTexture)] _EnableSpecularTexture ("Enable Specular Texture (Uses NoiseMap's ALPHA Channel)", Float) = 0
		[Header(Vertex Noise)] _VertexNoiseFreq ("Vertex Noise Freq", Range(0, 10)) = 0.15
		_VertexNoiseStrength ("Vertex Noise Strength (strength-xyz, normal strength W)", Vector) = (0,0.1,0,1)
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