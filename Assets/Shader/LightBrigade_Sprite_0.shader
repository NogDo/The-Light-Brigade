Shader "LightBrigade/Sprite" {
	Properties {
		[NoScaleOffset] _MainTex ("Diffuse", 2D) = "white" {}
		[HideInInspector] _Color ("Tint", Vector) = (1,1,1,1)
		[HideInInspector] _RendererColor ("RendererColor", Vector) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[Header(Note Instancing Disables Per sprite color)] _FogPassThru ("Fog Passthru", Range(0, 1)) = 0
		[Header(Rendering)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest Mode", Float) = 2
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 0
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
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Sprites/Default"
}