Shader "Unlit/LB_Portal" {
	Properties {
		_NoiseTex ("_NoiseTex", 2D) = "white" {}
		_ViewCube ("_ViewCube", Cube) = "black" {}
		[HDR] _BorderColor ("Border Color", Vector) = (1,1,1,1)
		_Color ("Color", Vector) = (1,1,1,1)
		_Intensity ("Intensity", Range(0, 1)) = 1
		_Wobble ("Wobble", Range(0, 1)) = 1
		_Blur ("Blur", Range(0, 1)) = 0
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 3
		_StencilReadMask ("Stencil Read Mask", Float) = 2
		[Header(BoxProjection)] [Toggle(UseBoxProjection)] _BoxProjection ("Use Box Projection", Float) = 0
		_BoxProjSize ("Box Projection Size", Vector) = (0,0,0,0)
		_BoxProjOffset ("Box Projection Offset", Vector) = (0,0,0,0)
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