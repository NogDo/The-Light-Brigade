Shader "LightBrigade/Skybox" {
	Properties {
		_SkyboxFogAlpha ("Fog Global Alpha", Range(0, 1)) = 1
		_SkyboxFogHeight ("Fog Vertical Fill", Range(-1, 1)) = 1
		_SkyboxBottomOffset ("Bottom Offset", Range(-1, 1)) = 0
		_SkyboxBottomTightness ("Bottom Tightness", Range(0, 2)) = 1
		_BottomColor ("Bottom Color", Vector) = (0,0,0,1)
		[Header(Skybox Texture)] [Toggle(EnableSkyboxTexture)] _EnableSkyboxTexture ("Enable Skybox Texture (Additive)", Float) = 0
		[NoScaleOffset] _MainTex ("Skybox Texture", Cube) = "black" {}
		[HDR] _Tint ("Tint", Vector) = (1,1,1,1)
		[Header(Rotation)] [Toggle(EnableSkyboxRotation)] _EnableSkyboxRotation ("Enable Skybox Rotation", Float) = 0
		_Rotation ("Rotation", Range(0, 360)) = 0
		_RotationSpeed ("Rotation Speed", Range(-5, 5)) = 0
		[Header(Portal)] [Toggle(EnablePortalChanges)] _EnablePortalChanges ("EnablePortalChanges", Float) = 0
		[Toggle(EnableMirrorBehavior)] _EnableMirrorBehavior ("EnableMirrorBehavior", Float) = 0
		_MirrorRotationY ("Mirror Rotation Y", Float) = 0
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass ("_StencilOpPass", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpFail ("_StencilOpFail", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 0
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