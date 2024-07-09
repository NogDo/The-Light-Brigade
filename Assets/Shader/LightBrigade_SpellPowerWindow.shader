Shader "LightBrigade/SpellPowerWindow" {
	Properties {
		[Header(Base)] [HDR] _EdgeColor ("Edge Color", Vector) = (1,1,1,0.1)
		[HDR] _BaseColor ("Base Color", Vector) = (1,1,0,0.05)
		[HDR] _FresnelColor ("Fresnel Color", Vector) = (1,1,0,1)
		[Header(Vertex Sway)] _VertexSwayFreq ("Vertex Sway Freq", Range(0, 50)) = 15
		_VertexSwaySpeed ("Vertex Sway Speed", Range(-10, 10)) = 1.5
		_VertexSwayDistance ("Vertex Sway Distance", Range(0, 0.1)) = 0.02
		[Header(Ring)] [HDR] _RingColor ("Ring Color", Vector) = (1,1,1,1)
		_RingFrequency ("Ring Frequency", Range(0, 50)) = 2
		_RingSpeed ("Ring Speed", Range(-10, 10)) = 2.5
		_RingParallaxOffset ("Ring Parallax Offset", Range(0, 2)) = 0.5
		_RingSharpness ("Ring Sharpness", Range(0, 1)) = 0.95
		_RingCenterFade ("Ring Center Fade", Range(0, 5)) = 0.5
		[Header(Caustic)] _MainTex ("Caustic Map (Red Green Mask)", 2D) = "white" {}
		_CausticParallaxOffset ("Caustic Parallax Offset", Range(0, 1)) = 0.1
		_CausticStrength ("Caustic Strength", Range(0, 5)) = 0.7
		_CausticTiling ("Caustic Tiling", Range(0, 5)) = 1
		[Header(Code)] [HDR] _DentColor ("Dent Color", Vector) = (1,1,1,1)
		_DentDataLocalPos ("Dent Pos (X,Y, Z-direction str, ring)", Vector) = (0,0,1,1)
		[HDR] _AimColor ("Aim Color", Vector) = (1,1,1,1)
		_AimDataLocalPos ("Aim Pos (X,Y, Z-direction str, falloff)", Vector) = (0,0,1,1)
		[Header(Debug)] [Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Show Vertex Colors", Float) = 0
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