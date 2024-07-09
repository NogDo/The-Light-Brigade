Shader "LightBrigade/Card Parallax" {
	Properties {
		[Header(Card Base)] [NoScaleOffset] _MainTex ("Abledo Map", 2D) = "white" {}
		[NoScaleOffset] _EffectsMasks ("Effects Masks", 2D) = "black" {}
		_AspectColor ("Aspect Color", Vector) = (1,1,1,1)
		[Header(Fresnel Inner)] [HDR] _FresnelColor ("Fresnel Color", Vector) = (0,0,0,1)
		_FresnelTightness ("Fresnel Tightness", Range(0, 10)) = 1
		[Header(Irridescent)] _IrridescentTightness ("Irridescent Tightness", Range(0, 10)) = 0.45
		[Header(Portrait Parallaxed)] [NoScaleOffset] _PortraitTex ("Portrait Map", 2D) = "black" {}
		_PortraitFGHeight ("Parallax - FG Offset", Range(0, 1)) = 0.3
		_PortraitCharacterHeight ("Parallax - Character Offset", Range(0, 1)) = 0.3
		_PortraitBGHeight ("Parallax - BG Offset", Range(0, 1)) = 0.3
		_FogOffset ("Fog Offset", Float) = 0
		[Header(Flash)] _Flash ("Flash", Range(0, 1)) = 0
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
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