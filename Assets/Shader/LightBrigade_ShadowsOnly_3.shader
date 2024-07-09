Shader "LightBrigade/ShadowsOnly" {
	Properties {
		_MainTex ("Abledo Map", 2D) = "white" {}
		_AlbedoTint ("Abledo Tint", Vector) = (1,1,1,1)
		_IgnoreLighting ("Ignore Lighting", Range(0, 1)) = 0
		[Header(Specular)] [Toggle(EnableSpecular)] _EnableSpecular ("Enable Specular", Float) = 0
		[HDR] _SpecularTint ("Specular Tint", Vector) = (1,1,1,1)
		_SpecularSmoothness ("Specular Smoothness", Range(0, 1)) = 0.9
		_SpecularStep ("Specular Step", Range(0, 1)) = 0.5
		_SpecularNoise ("Specular Noise (Distortion)", Range(0, 1)) = 0
		[Toggle(EnableSpecularMap)] _EnableSpecularMap ("Enable Specular Map", Float) = 0
		_SpecularMap ("Specular Map (R=strength, G=smoothness, B=caustics intensity)", 2D) = "white" {}
		[Header(Fresnel)] [Toggle(EnableFresnel)] _EnableFresnel ("Enable Fresnel", Float) = 0
		[Enum(Off,0,On,1)] _EnableFresnelLightAtten ("Attenuate By Light", Float) = 0
		[HDR] _FresnelTint ("Fresnel Tint", Vector) = (1,1,1,1)
		_FresnelTightness ("Fresnel Thickness", Range(0, 1)) = 0.4
		_FresnelFalloff ("Fresnel Falloff", Range(0, 1)) = 0.2
		_FresnelAlbedoBlend ("Fresnel Albedo Blend", Range(0, 1)) = 0.6
		[Header(Emission)] [Toggle(EnableEmission)] _EnableEmission ("Enable Emission", Float) = 0
		[HDR] _EmissionColor ("Emission Color", Vector) = (0.5,0.5,0.5,1)
		[Toggle(EnableEmissionMap)] _EnableEmissionMap ("Enable Emission Map", Float) = 0
		_EmissionMap ("Emission Map", 2D) = "white" {}
		[Header(Toon Lighting)] _ShadowStepSettings ("Step Settings (Shadow Brightness, Midtone Brightness, Highlight Brightness, Threshold)", Vector) = (0,0.75,1,0.8)
		[Header(Toon Noise Distortion)] [Toggle(EnableNoiseDistortion)] _EnableNoiseDistortion ("Enable Distortion (Uses NoiseMap's RED Channel)", Float) = 0
		_ShadowOffsetMap ("Noise Map (R=Noise, G=TopCover)", 2D) = "white" {}
		_ShadowStepDistortionSettings ("Noise Settings (Frequency, Strength, Biplanar-K, Noise Softness)", Vector) = (1,0.1,2,0.1)
		[Header(Top Cover)] [Toggle(EnableTopCover)] _EnableTopCover ("Enable Top Cover (Uses NoiseMap's GREEN Channel)", Float) = 0
		[Toggle(EnableTopCoverVertexColorBlue)] _EnableTopCoverVertexColorBlue ("Enable Vertex-Color Blue Mask (0=OFF, 1=ON)", Float) = 0
		_TopCoverTint ("Tint", Vector) = (1,1,1,1)
		_TopCoverThreshold ("Cover Threshold", Range(0, 1)) = 0.5
		_TopCoverDistortionAmount ("Noise Distortion Amount", Range(0, 1)) = 0.1
		[Header(Wind)] [Toggle(EnableWindSway)] _EnableWindSway ("Enable Wind Sway (RED Mask)", Float) = 0
		[Toggle(BakedWindSwayData)] _BakedWindSwayData ("BAKED Wind Sway Data (set by editor, BakeStaticMeshGroup.cs)", Float) = 0
		_WindSwayStrength ("Strength", Range(0, 50)) = 0.5
		_WindSwayFrequency ("Vertex Frequency", Range(0, 20)) = 1
		_WindSwaySpeed ("Speed", Range(0, 10)) = 0.1
		[Toggle(EnableWindSwayFreqVertexColorMask)] _EnableWindSwayFreqVertexColorMask ("Enable Freq Mask (Freq * GREEN Channel)", Float) = 0
		[Header(Obsidian)] [Toggle(EnableObsidian)] _EnableObsidian ("Enable Obsidian (Vertex-Color Blue Mask)", Float) = 0
		[HDR] _ObsidianFresnelTint ("Fresnel Tint", Vector) = (1,1,1,1)
		_ObsidianFresnelTightness ("Fresnel Tightness", Range(0, 1)) = 0.6
		[Header(Vertex Mask Color Tint)] [Toggle(EnableVertexMaskColorTint)] _EnableVertexMaskColorTint ("Enable Vertex-Color Mask Tint", Float) = 0
		[HDR] _VertexMaskColorTintRed ("Vertex Color - Red Channel (w/ Alpha)", Vector) = (1,1,1,0)
		[HDR] _VertexMaskColorTintGreen ("Vertex Color - Green Channel (w/ Alpha)", Vector) = (1,1,1,0)
		[HDR] _VertexMaskColorTintBlue ("Vertex Color - Blue Channel (w/ Alpha)", Vector) = (1,1,1,0)
		[Header(Point Light Receiver)] _SubsurfaceStrength ("Sub-surface Strength (Lambertian)", Range(0, 2)) = 1
		_LightAbsorption ("Light Absorption Scale (Scales Point Light Brightness)", Range(-2, 2)) = 1
		[Header(Extra Self Light)] [Toggle(EnableExtraSelfLight)] _EnableExtraSelfLight ("Enable Extra Light (Self Only)", Float) = 0
		[Toggle(EnableExtraSelfLightUseLocalPos)] _EnableExtraSelfLightUseLocalPos ("Use Local Position", Float) = 0
		[HDR] _ExtraSelfLightColor ("Self Light Color", Vector) = (1,1,1,1)
		_ExtraSelfLightData ("Pos (World XYZ) and Distance Atten (W)", Vector) = (0,0,0,1)
		[Header(Collectible Sheen)] [Toggle(EnableCollectibleSheen)] _EnableCollectibleSheen ("Enable Collectible Sheen", Float) = 0
		[Toggle(EnableCollectibleSheenUseVertexColor)] _EnableCollectibleSheenUseVertexColor ("Use Vertex Color (GREEN Channel, 0=OFF, 1=ON)", Float) = 0
		[Toggle(EnableCollectibleSheenUseLocalSpace)] _EnableCollectibleSheenUseLocalSpace ("Use Local instead of World", Float) = 0
		_CollectibleSheenTightness ("Sheen Tightness", Range(0, 3)) = 1
		[HDR] _CollectibleSheenRim ("Rim Tint", Vector) = (1,1,1,1)
		[HDR] _CollectibleSheenSweep ("Sweep Tint", Vector) = (1,1,1,1)
		[Toggle(EnableDebugVertexColors)] _EnableDebugVertexColors ("Show Vertex Colors", Float) = 0
		[Toggle(DisablePostProcess)] _DisablePostProcess ("Disable Post-Process", Float) = 0
		[Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 2
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 4
		[Enum(Off,0,On,1)] _ZWrite ("ZWrite", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("_SrcBlend", Float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("_DstBlend", Float) = 0
		[Header(Dissolve)] [Toggle] _DISSOLVE_EFFECT ("_DISSOLVE_EFFECT toggle", Float) = 0
		[Toggle] _DISSOLVE_EFFECT_AXIS_Z ("_DISSOLVE_EFFECT_AXIS_Z toggle", Float) = 0
		_DissoveAway ("_DissoveAway", Range(0, 1)) = 0
		_DissolveOffsetY ("_DissolveOffsetY", Float) = 0
		_DissolveColor0 ("_DissolveColor0", Vector) = (2,1.1,1.1,1)
		_DissolveColor1 ("_DissolveColor1", Vector) = (1.5,0.5,0,1)
		_DissolveColor2 ("_DissolveColor2", Vector) = (1.25,0.25,0.15,1)
		_DissolveColor3 ("_DissolveColor3", Vector) = (0.5,0.01,0.01,1)
		[Header(DeployablePreview)] [Toggle] _DEPLOYABLE_PREVIEW ("_DEPLOYABLE_PREVIEW toggle", Float) = 0
		[Header(Scroll)] [Toggle] _UV_SCROLL ("_UV_SCROLL_ON toggle", Float) = 0
		_UVScrollSettings ("xy = uv scroll speed, zw = uv offset", Vector) = (0,0,0,0)
		[Header(Caustics)] [Toggle] _LIGHT_CAUSTICS ("_LIGHT_CAUSTICS_ON toggle (v.color.b = intensity, uv1 = texcoord)", Float) = 0
		_LightCausticCenterPosition ("Light Caustics Center Position = xyz, fade distance = 1/w", Vector) = (20,10,45,12)
		_LightCausticsNoiseSettings ("Light Caustics noise (xy = uv scroll speed, z = scale, w = influence)", Vector) = (0.1,0.1,2,0.05)
		_LightCausticsScrollSettings ("Light Caustics Settings A (xy = uv scroll speed, w = scale)", Vector) = (0.125,0.125,0,0.5)
		_LightCausticsScrollSettings2 ("Light Caustics Settings B (x = power, y = multiply, z = hue shift count, w = hue shifted influence)", Vector) = (1,1,2,1)
		[HDR] _LightCausticsColor ("Light Caustics Color Tint", Vector) = (1,1,1,1)
		[Header(SteppingStones)] [Toggle(SteppingStones)] _SteppingStones ("SteppingStones toggle", Float) = 0
		_SteppingStoneHoverData ("SteppingStones Hover Data", Vector) = (1,1,1,1)
		[Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("_StencilComp", Float) = 0
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