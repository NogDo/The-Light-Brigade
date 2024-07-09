Shader "LightBrigade/MagicWater" {
	Properties {
		[Header(Water Base)] [HDR] _AlbedoCenter ("Water Center", Vector) = (1,1,1,1)
		[HDR] _AlbedoEdge ("Water Edge", Vector) = (1,1,1,1)
		[Header(Parallax Layer 1)] [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
		[HDR] _CausticTint ("Tint", Vector) = (1,1,1,1)
		_Parallax ("Parallax Height", Range(-1, 1)) = 0
		_CausticSpeed ("Move Speed", Range(-10, 10)) = 2
		_SwirlSpeed ("Swirl Speed", Range(-5, 5)) = 1
		_SwirlFreq ("Swirl Freq", Range(0, 1)) = 1
		[Header(Parallax Layer 2)] [Toggle(EnableSecondLayer)] _EnableSecondLayer ("Enable", Float) = 0
		_SecondaryTex ("Texture", 2D) = "white" {}
		[HDR] _CausticTint2 ("Tint", Vector) = (1,1,1,1)
		_Parallax2 ("Parallax Height", Range(-1, 1)) = -0.5
		_CausticSpeed2 ("Move  Speed", Range(-10, 10)) = 2
		_SwirlSpeed2 ("Swirl Speed", Range(-5, 5)) = 0
		_SwirlFreq2 ("Swirl Freq", Range(0, 1)) = 0
		[Header(Shiny)] _ParticleTex ("Particle Map", 2D) = "white" {}
		_ShinyParallax ("Parallax", Range(-1, 1)) = 0
		[HDR] _ShinySparklesTint ("Shiny Sparkle Tint", Vector) = (1,1,1,1)
		[Header(Ripple)] _RippleFalloff ("Ripple Fall Off", Range(0, 1)) = 0.8
		_RippleHeight ("Ripple Height", Range(0, 1)) = 0.2
		_RippleFreq ("Ripple Frequency", Range(0, 10)) = 4
		_RippleSpeed ("Ripple Speed", Range(0, 10)) = 1
		_RippleSwirlFreq ("Ripple Swirl Freq", Range(-10, 10)) = 1
		[HDR] _RippleTint ("Ripple Tint", Vector) = (1,1,1,1)
		[Header(Crystal Glow)] [HDR] _Inner ("Glow Tint (Vertex Green Mask)", Vector) = (1,1,1,1)
		[Header(Code)] _WorldCenter ("WorldPos", Vector) = (0,0,0,0)
		[Header(Debug)] [Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Show Vertex Colors", Float) = 0
		[Header(MotionVectors)] [Toggle(DisableMotionVectors)] _DisableMotionVectors ("Disable Motion Vectors", Float) = 0
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