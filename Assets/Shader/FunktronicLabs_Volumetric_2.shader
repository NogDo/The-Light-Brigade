Shader "FunktronicLabs/Volumetric" {
	Properties {
		[Header(Surface Lighting)] [HDR] _Albedo ("Surface Albedo", Vector) = (1,1,1,1)
		_LBSubSurface ("LB Sub Surface", Range(0, 1)) = 0
		[Header(Combined Parallax Texture)] _MarbleTex ("Encoded RGB Texture (R=Layer1, G=Layer2, B=Heightmap)", 2D) = "black" {}
		[Header(Parallax Layer 1  RED CHANNEL)] [Toggle(EnableLayer0)] _EnableLayer0 ("Enable", Float) = 1
		_Layer0Tint ("Layer 1 Tint", Vector) = (1,1,1,1)
		_Layer0Tex_ST ("Texture (ScaleX, ScaleY, OffsetX, OffsetY)", Vector) = (1,1,0,0)
		_Layer0Data ("Settings (Scroll Speed X, Scroll Speed Y, Parallax Height, Emission)", Vector) = (0,0,0.1,0)
		[Header(Parallax Layer 1  GREEN CHANNEL)] [Toggle(EnableLayer1)] _EnableLayer1 ("Enable", Float) = 1
		_Layer1Tint ("Layer 2 Tint", Vector) = (1,1,1,1)
		_Layer1Tex_ST ("Texture (ScaleX, ScaleY, OffsetX, OffsetY)", Vector) = (1,1,0,0)
		_Layer1Data ("Settings (Scroll Speed X, Scroll Speed Y, Parallax Height, Emission)", Vector) = (0,0,0.1,0)
		[Header(Parallax Heightmap  BLUE CHANNEL)] _MarbleHeightScale ("Heightmap Strength", Range(0, 0.5)) = 0.1
		_ParallaxHeightmap_ST ("Texture (ScaleX, ScaleY, OffsetX, OffsetY)", Vector) = (1,1,0,0)
		[Header(Fresnel)] _FresnelTightness ("Fresnel Tightness", Range(0, 10)) = 4
		[HDR] _FresnelColorOutside ("Fresnel Color Outside", Vector) = (1,1,1,1)
		[Header(Specular)] [Toggle(EnableSpecular)] _EnableSpecular ("EnableSpecular", Float) = 0
		[HDR] _SpecularTint ("Specular Tint", Vector) = (1,1,1,1)
		_SpecularNoise ("Specular Noise (Distortion)", Range(0, 1)) = 0
		_SpecularTightness ("Specular Tightness", Range(0, 1)) = 0.5
		_SpecularBrightness ("Specular Brightness", Range(0, 5)) = 1
		[Header(Inner Light)] [Toggle(EnableInnerLight)] _EnableInnerLight ("Enable (Uses Vertex RED Channel)", Float) = 0
		[Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Debug Vertex Colors", Float) = 0
		[HDR] _InnerLightColorInside ("Inner Light Color - Center", Vector) = (1,1,1,1)
		[HDR] _InnerLightColorOutside ("Inner Light Color - Falloff", Vector) = (1,1,1,1)
		_InnerLightData ("Brightness (Layer 1, Layer 2)", Vector) = (1,1,0,0)
		[Header(Top Cover)] [Toggle(EnableTopCover)] _EnableTopCover ("Enable Top Cover (Uses NoiseMap's GREEN Channel)", Float) = 0
		[Toggle(EnableTopCoverVertexColorBlue)] _EnableTopCoverVertexColorBlue ("Enable Vertex-Color Blue Mask (0=OFF, 1=ON)", Float) = 0
		_TopCoverTint ("Tint", Vector) = (1,1,1,1)
		_TopCoverThreshold ("Cover Threshold", Range(0, 1)) = 0.5
		_TopCoverDistortionAmount ("Noise Distortion Amount", Range(0, 1)) = 0.1
		[Toggle(EnableNoiseDistortion)] _EnableNoiseDistortion ("Enable Distortion (Uses NoiseMap's RED Channel)", Float) = 0
		_ShadowOffsetMap ("Noise Map (R=Noise, G=TopCover)", 2D) = "white" {}
		_ShadowStepDistortionSettings ("Noise Settings (Frequency, Strength, Biplanar-K, Noise Softness)", Vector) = (1,0.1,2,0.1)
		[Header(Extra Self Light)] [Toggle(EnableExtraSelfLight)] _EnableExtraSelfLight ("Enable Extra Light (Self Only)", Float) = 0
		[HDR] _ExtraSelfLightColor ("Self Light Color", Vector) = (1,1,1,1)
		_ExtraSelfLightData ("Self Light World Pos (XYZ) and Distance Atten (W)", Vector) = (0,0,0,1)
		[Header(Dissolve)] [Toggle] _DISSOLVE_EFFECT ("_DISSOLVE_EFFECT toggle", Float) = 0
		[Toggle] _DISSOLVE_EFFECT_AXIS_Z ("_DISSOLVE_EFFECT_AXIS_Z toggle", Float) = 0
		_DissoveAway ("_DissoveAway", Range(0, 1)) = 0
		_DissolveOffsetY ("_DissolveOffsetY", Float) = 0
		_DissolveColor0 ("_DissolveColor0", Vector) = (2,1.1,1.1,1)
		_DissolveColor1 ("_DissolveColor1", Vector) = (1.5,0.5,0,1)
		_DissolveColor2 ("_DissolveColor2", Vector) = (1.25,0.25,0.15,1)
		_DissolveColor3 ("_DissolveColor3", Vector) = (0.5,0.01,0.01,1)
		[Header(Stencil)] [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpPass ("_StencilOpPass", Float) = 0
		[Enum(UnityEngine.Rendering.StencilOp)] _StencilOpFail ("_StencilOpFail", Float) = 0
		_StencilReadMask ("Stencil Read Mask", Float) = 0
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
	Fallback "VertexLit"
}