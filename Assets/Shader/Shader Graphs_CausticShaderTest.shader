Shader "Shader Graphs/CausticShaderTest" {
	Properties {
		Color_2f46d36e89c14c95b303bcba7a393c44 ("AlbedoTint", Vector) = (0.490566,0.490566,0.490566,0)
		[NoScaleOffset] Texture2D_c7d8f5dbfa25435aabc9b198ce522629 ("AlbedoTexture", 2D) = "white" {}
		Vector2_166297c371d54049853492ab3632966d ("AlbedoTiling", Vector) = (1,1,0,0)
		[HDR] Color_0a408783ca4643af8c716d10f4d5751d ("CausticColor", Vector) = (0,0,0,0)
		Vector1_51ccdede2bde4ab7bcb7fbe097982559 ("CausticTile", Float) = 1
		Vector1_dcd153365d0341e2be63cfef7abd0e2d ("CausticRange", Float) = 0
		Vector1_dee412765d4f4d989b3fc2e99abaa17c ("CausticFalloff", Float) = 0
		[NoScaleOffset] Texture2D_4efc87027dd2408284905cc802192e94 ("CausticTexture(Blue)", 2D) = "white" {}
		Vector1_c100fbe7e4b844e19831fc762cc3031c ("Panning", Float) = 0.02
		Vector1_223fbccfbcf44c88b751ddcfd60a4220 ("DistortionSpeed", Float) = 0.5
		Vector1_2fb6b1eacdf14e0d91b8382f300411e8 ("DistortionScale", Float) = 10
		Vector1_c5b3103778a74a77a497b64088890d98 ("DistortionIntensity", Float) = 0.5
		[HideInInspector] _QueueOffset ("_QueueOffset", Float) = 0
		[HideInInspector] _QueueControl ("_QueueControl", Float) = -1
		[HideInInspector] [NoScaleOffset] unity_Lightmaps ("unity_Lightmaps", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_LightmapsInd ("unity_LightmapsInd", 2DArray) = "" {}
		[HideInInspector] [NoScaleOffset] unity_ShadowMasks ("unity_ShadowMasks", 2DArray) = "" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Hidden/Shader Graph/FallbackError"
	//CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
}