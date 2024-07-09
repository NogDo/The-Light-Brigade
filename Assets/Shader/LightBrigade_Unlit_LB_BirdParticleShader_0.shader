Shader "LightBrigade/Unlit/LB_BirdParticleShader" {
	Properties {
		_BaseColor ("Base Color", Vector) = (1,1,1,1)
		_FlapSpeed ("Flap Speed", Float) = 1
		_FlapAmount ("Flap Amount", Float) = 1
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