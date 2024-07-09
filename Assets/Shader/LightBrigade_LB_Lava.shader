Shader "LightBrigade/LB_Lava" {
	Properties {
		_AlbedoTint ("Abledo Color", Vector) = (1,1,1,1)
		_CausticTex ("Caustic Map", 2D) = "black" {}
		_CausticTexScale ("Caustic Map Scale", Float) = 0.1
		_CausticScrollSpeed ("Caustic Scroll Speed (xy)", Vector) = (0,1,0,0)
		_NoiseInfluenceScale ("Noise Influence Scale", Float) = 0.1
		[KeywordEnum(XZ, XY, YZ)] _CausticScrollDirection ("Caustic Scroll World Coordinates", Float) = 0
		[Header(Water Noise)] _WaterNoiseFreq ("Water Noise Frequency", Range(0, 5)) = 0.75
		_WaterNoiseSpeed ("Water Noise Speed", Range(0, 5)) = 0.5
		_WaterNoiseStrength ("Water Noise Strength", Range(0, 1)) = 0.03
		[Header(Shore Line)] [Toggle(EnableShoreLine)] _EnableShoreLine ("Enable Shore Line", Float) = 0
		_ShoreLineColor ("Shoreline Color", Vector) = (1,1,1,1)
		_ShoreLineSpeed ("Shoreline Speed", Range(-1, 1)) = 0.1
		_ShareLineFreq ("Shoreline Frequency", Range(0, 10)) = 3
		_ShoreLineNoiseAmount ("Shoreline Noise Amount", Range(0, 1)) = 0.1
		_ShoreLineThresholds ("Shoreline Threshold (min, max, fade)", Vector) = (0.4,1,0.8,0)
		[Header(Specular)] [Toggle(EnableSpecular)] _EnableSpecular ("Enable Specular", Float) = 0
		[HDR] _SpecularTint ("Specular Color Tint", Vector) = (1,1,1,1)
		_SpecularTightness ("Specular Smoothness", Range(0, 50)) = 0.9
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