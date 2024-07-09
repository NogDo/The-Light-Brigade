Shader "LightBrigade/ControllerTutorialGhost" {
	Properties {
		_MainTex ("Abledo Map", 2D) = "white" {}
		_MaskTex ("Mask Texture", 2D) = "white" {}
		[HDR] _CausticTint ("Caustic Tint", Vector) = (1,1,1,1)
		[HDR] _FresnelTint ("Fresnel Tint Outer", Vector) = (1,1,1,1)
		[HDR] _FresnelTintInner ("Fresnel Tint Inner", Vector) = (1,1,1,1)
		[HDR] _HeartTint ("Heart  Tint", Vector) = (1,1,0,1)
		_FresnelTightness ("Fresnel Tightness", Float) = 1
		_HeartY ("Heart Y", Float) = 1.37
		_FeetY ("Feet Y", Float) = 0.7
		_FresnelFalloff ("Fresnel Falloff", Float) = 0.5
		_Parallax ("Parallax", Range(0, 1)) = 0
		_FadeInAnim ("FadeInAnim", Range(0, 1)) = 1
		_PrayerCharge ("Prayer Charge", Range(0, 1)) = 0
		_HeartFadeAtten ("Heart Fade Atten", Range(0, 1)) = 0.9
		_ButtonIndex ("Button Index", Float) = -1
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
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