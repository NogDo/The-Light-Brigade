Shader "LightBrigade/TouchRing" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_RingInnerRadius ("Ring Inner Radius", Range(0, 5)) = 1
		_RingFlatness ("Ring Flatness", Range(0.1, 1.5)) = 0.15
		_Progress ("Progress", Range(0, 1)) = 0.5
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