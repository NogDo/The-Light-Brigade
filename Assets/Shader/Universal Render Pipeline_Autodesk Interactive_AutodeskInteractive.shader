Shader "Universal Render Pipeline/Autodesk Interactive/AutodeskInteractive" {
	Properties {
		[ToggleUI] _UseColorMap ("UseColorMap", Float) = 0
		_Color ("BaseColor", Vector) = (0,0,0,0)
		[NoScaleOffset] _MainTex ("ColorMap", 2D) = "white" {}
		[ToggleUI] _UseNormalMap ("UseNormalMap", Float) = 0
		[NoScaleOffset] _BumpMap ("NormalMap", 2D) = "white" {}
		[ToggleUI] _UseMetallicMap ("UseMetallicMap", Float) = 0
		_Metallic ("Metallic", Float) = 0
		[NoScaleOffset] _MetallicGlossMap ("MetallicMap", 2D) = "white" {}
		[ToggleUI] _UseRoughnessMap ("UseRoughnessMap", Float) = 0
		_Glossiness ("Roughness", Float) = 0
		[NoScaleOffset] _SpecGlossMap ("RoughnessMap", 2D) = "white" {}
		[ToggleUI] _UseEmissiveMap ("UseEmissiveMap", Float) = 0
		[HDR] _EmissionColor ("Emissive", Vector) = (0,0,0,0)
		[NoScaleOffset] _EmissionMap ("EmissiveMap", 2D) = "white" {}
		[ToggleUI] _UseAoMap ("UseAoMap", Float) = 0
		[NoScaleOffset] _OcclusionMap ("AoMap", 2D) = "white" {}
		_UvOffset ("UVOffset", Vector) = (0,0,0,0)
		_UvTiling ("UVScale", Vector) = (1,1,0,0)
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
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
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}