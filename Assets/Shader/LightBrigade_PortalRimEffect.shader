Shader "LightBrigade/PortalRimEffect" {
	Properties {
		[Header(MotionVectors)] [Toggle(DisableMotionVectors)] _DisableMotionVectors ("Disable Motion Vectors", Float) = 0
		[HDR] _BaseTintInner ("Base Color (Inner)", Vector) = (0,0,0,1)
		[HDR] _BaseTintOuter ("Base Color (Outer)", Vector) = (0,0,0,1)
		[Header(Caustic)] [HDR] _CausticTint ("Caustic Color", Vector) = (1,1,1,1)
		_Parallax ("Parallax Strength", Range(-5, 5)) = 1
		_MainTex ("Caustic Texture", 2D) = "black" {}
		_CausticFrequency ("Freq Inner", Range(-2, 2)) = 0.75
		_CausticDensity ("Caustic Density", Range(0, 1)) = 0.1
		_CausticSpeed ("Speed", Range(-5, 5)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}