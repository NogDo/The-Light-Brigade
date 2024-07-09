Shader "LightBrigade/SoulGem" {
	Properties {
		_MainTex ("Caustic Map (Red Green Mask)", 2D) = "white" {}
		[Header(Outside RED CHANNEL VERTEX)] [HDR] _AbledoTop ("Albedo Top", Vector) = (1,1,1,1)
		[HDR] _AbledoBot ("Albedo Bottom", Vector) = (1,1,1,1)
		[HDR] _FresnelTint ("Fresnel Tint Top", Vector) = (1,1,1,1)
		[HDR] _FresnelTintBot ("Fresnel Tint Bottom", Vector) = (1,1,1,1)
		[Header(Caustic Outide RED CHANNEL TEXTURE)] _TextureDataOutside ("(TilingX, TilingY, Parallax Height, Scroll Speed)", Vector) = (1,1,0,0)
		_Parallax ("Parallax Height", Range(-1, 1)) = 0
		[HDR] _CausticTintOutside ("Caustic Tint", Vector) = (1,1,1,1)
		[Header(Inside GREEN CHANNEL VERTEX)] _TextureDataInside ("(TilingX, TilingY, Parallax Height, Scroll Speed)", Vector) = (1,1,0,0)
		[HDR] _Inner ("Fresnel Inner", Vector) = (1,1,1,1)
		[HDR] _InnerEdge ("Fresnel Outer Edge", Vector) = (1,1,1,1)
		_InnerFresnelTightness ("Fresnel Tightness", Range(0, 1)) = 0
		_InsideFresnelDance ("Fresnel Dance", Range(0, 10)) = 1
		[Header(Caustic Inside GREEN CHANNEL TEXTURE)] _ParallaxInside ("Parallax", Range(-1, 1)) = 0
		[HDR] _CausticTintInside ("Caustic Tint", Vector) = (1,1,1,1)
		[Header(Sway BLUE CHANNEL VERTEX)] _SwayFreqOutside ("Sway Frequency", Range(0, 2)) = 0
		_SwaySpeedOutside ("Sway Speed", Range(0, 1)) = 0
		_SwayAmpOutside ("(SwayX, SwayY, SwayZ) - Local Space", Vector) = (1,1,1,0)
		_SwayRainbow ("Sway Rainbow (Red Vertex Channel)", Range(-5, 5)) = 2
		[Header(Code)] _Flash ("Flash", Range(0, 1)) = 0
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