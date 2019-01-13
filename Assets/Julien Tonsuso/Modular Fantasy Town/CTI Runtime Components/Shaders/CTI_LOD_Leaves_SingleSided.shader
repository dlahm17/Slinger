// Upgrade NOTE: removed variant '__' where variant LOD_FADE_PERCENTAGE is used.

Shader "CTI/LOD Leaves Single Sided" {
Properties {

	_Color ("Color Variation", Color) = (0.9,0.5,0.0,0.1)
	[Space(5)]
	_MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.3
	[Space(5)]
	[NoScaleOffset] _BumpSpecMap("Normalmap (GA) Specular (B)", 2D) = "bump" {} // Shadow Offset (B)
	[NoScaleOffset] _TranslucencyMap("AO (G) Translucency (B) Smoothness (A)", 2D) = "white" {}
	
	[Space(3)]
	_TranslucencyStrength ("Translucency Strength", Range(0,1)) = 0.5
	
	[Header(Wind)]
	[Space(3)]
	_TumbleStrength("Tumble Strength", Range(0,1)) = 0.1
	_TumbleFrequency("Tumble Frequency", Range(0,4)) = 1
	_TimeOffset("Time Offset", Range(0,2)) = 0.25
	[Space(3)]
	[Toggle(_DETAIL_MULX2)] _LEAFTURBULENCE("Enable Leaf Turbulence", Float) = 0.0
	_LeafTurbulence("Leaf Turbulence", Range(0,1)) = 0.2
	_EdgeFlutterInfluence("Edge Flutter Influence", Range(0,1)) = 0.25
	[Space(5)]
	[Toggle(_METALLICGLOSSMAP)] _LODTerrain ("Use Wind for LODGroups on Terrain", Float) = 0.0
	[Space(10)]

	[Header(Options for lowest LOD)]
	[Space(3)]
//	[Toggle] _FadeOutAllLeaves("Fade out all Leaf Planes", Float) = 0.0
	[Toggle] _FadeOutWind("Fade out Wind", Float) = 0.0
}

SubShader { 
	Tags {
		"IgnoreProjector"="True"
		"RenderType"="CTI-TreeLeaf"
	}
	LOD 200
	Cull Off
	CGPROGRAM
		#pragma surface surf StandardTranslucent alphatest:_Cutoff vertex:CTI_TreeVertLeaf nolightmap keepalpha
		#pragma target 3.0
		#pragma multi_compile __ LOD_FADE_CROSSFADE
		#pragma multi_compile __ _METALLICGLOSSMAP
		#pragma shader_feature _DETAIL_MULX2

		#include "UnityBuiltin3xTreeLibrary.cginc"

		#define USE_VFACE
		#define LEAFTUMBLING
		#define IS_LODTREE

		#include "Includes/CTI_TranslucentLighting.cginc"
		#include "Includes/CTI_Builtin4xTreeLibraryTumbling.cginc"

		sampler2D _MainTex;
		#if !defined(UNITY_PASS_SHADOWCASTER)
			sampler2D _BumpSpecMap;
			sampler2D _TranslucencyMap;
			half _TranslucencyStrength;
		#endif

		/* moved to include
		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR; // color.a = AO
			UNITY_DITHER_CROSSFADE_COORDS
		};
		*/

		//void surf (Input IN, inout LeafSurfaceOutput o) {
		void surf (Input IN, inout SurfaceOutputStandardTranslucent o) {

			UNITY_APPLY_DITHER_CROSSFADE(IN)
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Alpha = c.a;
			
			//	Add Color Variation
			o.Albedo = lerp(c.rgb, (c.rgb + _Color.rgb) * 0.5, IN.color.r * _Color.a);
			fixed4 trngls = tex2D(_TranslucencyMap, IN.uv_MainTex);
			o.Translucency = trngls.b * _TranslucencyStrength;
			// fade out translucency
		//	#if defined (LOD_FADE_CROSSFADE)
		//		o.Translucency *= unity_LODFade.x;
		//	#endif
			o.Smoothness = trngls.a;
			o.Occlusion = trngls.g * IN.color.a;

			half4 norspc = tex2D(_BumpSpecMap, IN.uv_MainTex);
			o.Specular = norspc.b;
			o.Normal = UnpackNormalDXT5nm(norspc) * IN.FacingSign;
		}
	ENDCG

	// Pass to render object as a shadow caster
	Pass {
		Name "ShadowCaster"
		Tags { "LightMode" = "ShadowCaster"}

		Cull Off
		
		CGPROGRAM
		#pragma vertex vert_surf
		#pragma fragment frag_surf
		#pragma target 3.0
		#pragma multi_compile_shadowcaster
		#pragma multi_compile  LOD_FADE_PERCENTAGE LOD_FADE_CROSSFADE
		#pragma multi_compile __ _METALLICGLOSSMAP
		#pragma shader_feature _DETAIL_MULX2
		#include "HLSLSupport.cginc"
		#include "UnityCG.cginc"
		#include "Lighting.cginc"

		#define UNITY_PASS_SHADOWCASTER
		#include "UnityBuiltin3xTreeLibrary.cginc"
		#define LEAFTUMBLING
		#define DEPTH_NORMAL
		#define IS_LODTREE
		#include "Includes/CTI_Builtin4xTreeLibraryTumbling.cginc"

		sampler2D _MainTex;

	//  Already defined in include
	//	struct Input {
	//		float2 uv_MainTex;
	//	};

		struct v2f_surf {
			V2F_SHADOW_CASTER;
			float2 hip_pack0 : TEXCOORD1;
			UNITY_DITHER_CROSSFADE_COORDS_IDX(2)
		};

		float4 _MainTex_ST;
		
		v2f_surf vert_surf (appdata_full v) {
			v2f_surf o;
			CTI_TreeVertLeaf(v);
			o.hip_pack0.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
			TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
			UNITY_TRANSFER_DITHER_CROSSFADE_HPOS(o, o.pos)
			return o;
		}
		fixed _Cutoff;
		
		float4 frag_surf (v2f_surf IN) : SV_Target {
			UNITY_APPLY_DITHER_CROSSFADE(IN)
			half alpha = tex2D(_MainTex, IN.hip_pack0.xy).a;
		//	alpha = (unity_LODFade.x > 0) ? alpha * unity_LODFade.x : alpha;
			clip (alpha - _Cutoff);
			SHADOW_CASTER_FRAGMENT(IN)
		}
		ENDCG
	}
///

}
}
