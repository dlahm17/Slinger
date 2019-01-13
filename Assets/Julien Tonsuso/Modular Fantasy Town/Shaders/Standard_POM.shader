// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JTonsuso/Standard_POM"
{
	Properties
	{
		_MainTex("_MainTex", 2D) = "white" {}
		_EmissionMap("_EmissionMap", 2D) = "white" {}
		_EmissionColor("_EmissionColor", Color) = (0,0,0,0)
		_BumpMap("_BumpMap", 2D) = "bump" {}
		_BumpScale("_BumpScale", Float) = 0.5
		_MetallicGlossMap("_MetallicGlossMap", 2D) = "white" {}
		_Glossiness("_Glossiness", Float) = 0.5
		_MetallicScale("_MetallicScale", Float) = 0.5
		_Occlusion("Occlusion", 2D) = "white" {}
		_AOstrength("AO strength", Range( 0 , 1)) = 0.5
		_HeightMap("HeightMap", 2D) = "white" {}
		[Toggle]_ParallaxSwitch("Parallax Switch", Float) = 0
		_RefPlane("Ref Plane", Range( 0 , 1)) = 0.4247461
		_ParallaxScale("Parallax Scale", Range( 0 , 1)) = 0.4247461
		_CurvatureU("Curvature U", Range( 0 , 100)) = 0
		_CurvatureV("Curvature V", Range( 0 , 30)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[Header(Parallax Occlusion Mapping)]
		_CurvFix("Curvature Bias", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
			float3 worldNormal;
			float3 worldPos;
		};

		uniform float _BumpScale;
		uniform sampler2D _BumpMap;
		uniform float _ParallaxSwitch;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _HeightMap;
		uniform float _ParallaxScale;
		uniform float _RefPlane;
		uniform float _CurvFix;
		uniform float _CurvatureU;
		uniform float _CurvatureV;
		uniform float4 _HeightMap_ST;
		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionColor;
		uniform float _MetallicScale;
		uniform sampler2D _MetallicGlossMap;
		uniform float _Glossiness;
		uniform sampler2D _Occlusion;
		uniform float _AOstrength;


		inline float2 POM( sampler2D heightMap, float2 uvs, float2 dx, float2 dy, float3 normalWorld, float3 viewWorld, float3 viewDirTan, int minSamples, int maxSamples, float parallax, float refPlane, float2 tilling, float2 curv, int index )
		{
			float3 result = 0;
			int stepIndex = 0;
			int numSteps = ( int )lerp( (float)maxSamples, (float)minSamples, (float)dot( normalWorld, viewWorld ) );
			float layerHeight = 1.0 / numSteps;
			float2 plane = parallax * ( viewDirTan.xy / viewDirTan.z );
			uvs += refPlane * plane;
			float2 deltaTex = -plane * layerHeight;
			float2 prevTexOffset = 0;
			float prevRayZ = 1.0f;
			float prevHeight = 0.0f;
			float2 currTexOffset = deltaTex;
			float currRayZ = 1.0f - layerHeight;
			float currHeight = 0.0f;
			float intersection = 0;
			float2 finalTexOffset = 0;
			while ( stepIndex < numSteps + 1 )
			{
				result.z = dot( curv, currTexOffset * currTexOffset );
				currHeight = tex2Dgrad( heightMap, uvs + currTexOffset, dx, dy ).r * ( 1 - result.z );
				if ( currHeight > currRayZ )
				{
					stepIndex = numSteps + 1;
				}
				else
				{
					stepIndex++;
					prevTexOffset = currTexOffset;
					prevRayZ = currRayZ;
					prevHeight = currHeight;
					currTexOffset += deltaTex;
					currRayZ -= layerHeight * ( 1 - result.z ) * (1+_CurvFix);
				}
			}
			int sectionSteps = 10;
			int sectionIndex = 0;
			float newZ = 0;
			float newHeight = 0;
			while ( sectionIndex < sectionSteps )
			{
				intersection = ( prevHeight - prevRayZ ) / ( prevHeight - currHeight + currRayZ - prevRayZ );
				finalTexOffset = prevTexOffset + intersection * deltaTex;
				newZ = prevRayZ - intersection * layerHeight;
				newHeight = tex2Dgrad( heightMap, uvs + finalTexOffset, dx, dy ).r;
				if ( newHeight > newZ )
				{
					currTexOffset = finalTexOffset;
					currHeight = newHeight;
					currRayZ = newZ;
					deltaTex = intersection * deltaTex;
					layerHeight = intersection * layerHeight;
				}
				else
				{
					prevTexOffset = finalTexOffset;
					prevHeight = newHeight;
					prevRayZ = newZ;
					deltaTex = ( 1 - intersection ) * deltaTex;
					layerHeight = ( 1 - intersection ) * layerHeight;
				}
				sectionIndex++;
			}
			#ifdef UNITY_PASS_SHADOWCASTER
			if ( unity_LightShadowBias.z == 0.0 )
			{
			#endif
				if ( result.z > 1 )
					clip( -1 );
			#ifdef UNITY_PASS_SHADOWCASTER
			}
			#endif
			return uvs + finalTexOffset;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float2 appendResult47 = (float2(_CurvatureU , _CurvatureV));
			float2 OffsetPOM8 = POM( _HeightMap, uv_MainTex, ddx(uv_MainTex), ddy(uv_MainTex), ase_worldNormal, ase_worldViewDir, i.viewDir, 128, 128, _ParallaxScale, _RefPlane, _HeightMap_ST.xy, appendResult47, 0 );
			float2 customUVs39 = OffsetPOM8;
			float2 temp_output_40_0 = ddx( uv_MainTex );
			float2 temp_output_41_0 = ddy( uv_MainTex );
			o.Normal = UnpackScaleNormal( tex2D( _BumpMap, lerp(customUVs39,uv_MainTex,_ParallaxSwitch), temp_output_40_0, temp_output_41_0 ) ,_BumpScale );
			o.Albedo = tex2D( _MainTex, lerp(customUVs39,uv_MainTex,_ParallaxSwitch), temp_output_40_0, temp_output_41_0 ).rgb;
			o.Emission = ( tex2D( _EmissionMap, lerp(customUVs39,uv_MainTex,_ParallaxSwitch), temp_output_40_0, temp_output_41_0 ).r * _EmissionColor ).rgb;
			float4 tex2DNode23 = tex2D( _MetallicGlossMap, lerp(customUVs39,uv_MainTex,_ParallaxSwitch), temp_output_40_0, temp_output_41_0 );
			o.Metallic = ( _MetallicScale * tex2DNode23.r );
			o.Smoothness = ( _Glossiness * tex2DNode23.a );
			float4 tex2DNode21 = tex2D( _Occlusion, lerp(customUVs39,uv_MainTex,_ParallaxSwitch), temp_output_40_0, temp_output_41_0 );
			float temp_output_52_0 = ( 1.0 - _AOstrength );
			float clampResult51 = clamp( ( tex2DNode21.r + temp_output_52_0 ) , 0.0 , 1.0 );
			o.Occlusion = clampResult51;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
141;272;1281;1121;124.56;663.4063;1.001783;True;False
Node;AmplifyShaderEditor.RangedFloatNode;45;-1735.403,495.1998;Float;False;Property;_CurvatureV;Curvature V;16;0;Create;True;0;0;False;0;0;0;0;30;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-1735.403,415.1998;Float;False;Property;_CurvatureU;Curvature U;15;0;Create;True;0;0;False;0;0;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;47;-1399.403,399.1998;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;9;-1731.6,-121.7001;Float;True;Property;_HeightMap;HeightMap;11;0;Create;True;0;0;False;0;None;a9c34a48d7a1e854ea77bccbde9b337e;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1718.403,133.7001;Float;False;Property;_ParallaxScale;Parallax Scale;14;0;Create;True;0;0;False;0;0.4247461;0.014;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-1794.316,310.2488;Float;False;Property;_RefPlane;Ref Plane;13;0;Create;True;0;0;False;0;0.4247461;0.31;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;15;-1527.403,223.2001;Float;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1523.683,-422.8272;Float;False;0;11;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;8;-1227.803,-6.000015;Float;False;0;128;128;10;0.02;0;False;1,1;True;10,0;Texture2D;7;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT2;0,0;False;6;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-937.8985,-282.8998;Float;False;customUVs;1;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DdxOpNode;40;-656,-160;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ToggleSwitchNode;58;-708.7263,-423.9943;Float;False;Property;_ParallaxSwitch;Parallax Switch;12;0;Create;True;0;0;False;0;0;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;49;73.0966,997.1884;Float;False;Property;_AOstrength;AO strength;10;0;Create;True;0;0;False;0;0.5;0.386;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DdyOpNode;41;-656,-96;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;21;41.80207,749.9794;Float;True;Property;_Occlusion;Occlusion;9;0;Create;True;0;0;False;0;None;667e95d6ee3140744a46e14bad8529d0;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;52;368.3436,991.7853;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;539.9799,762.4821;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;55;145.9011,366.4338;Float;False;Property;_EmissionColor;_EmissionColor;2;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;54;57.50075,166.2335;Float;True;Property;_EmissionMap;_EmissionMap;1;0;Create;True;0;0;False;0;None;732a2582f30f8374792060b86dd09fb5;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-262.2585,-623.9753;Float;False;Property;_BumpScale;_BumpScale;4;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;112.4619,38.91283;Float;False;Property;_Glossiness;_Glossiness;7;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;74.40002,-161.5311;Float;True;Property;_MetallicGlossMap;_MetallicGlossMap;5;0;Create;True;0;0;False;0;None;a23b10135590b0141b8b5ab00dc5652c;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;60;366.351,-273.2179;Float;False;Property;_MetallicScale;_MetallicScale;8;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;57;-654.1276,-668.9924;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;524.8311,694.1029;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;459.201,244.2335;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;20;41.80207,557.9795;Float;True;Property;_Roughness;Roughness;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Derivative;ProceduralTexture;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;94.26987,-600.5016;Float;True;Property;_MainTex;_MainTex;0;0;Create;True;0;0;False;0;None;048ce3558b81e8d4fb87d3f9dcc57c61;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;402.162,23.21284;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;51;671.8763,536.6329;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;94.26987,-408.5013;Float;True;Property;_BumpMap;_BumpMap;3;0;Create;True;0;0;False;0;None;29cb524fb8b566b44bb168d34e8c8976;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;541.8513,-175.7179;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;802.9525,-221.0693;Float;False;True;4;Float;ASEMaterialInspector;0;0;Standard;JTonsuso/Standard_POM;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;47;0;43;0
WireConnection;47;1;45;0
WireConnection;8;0;10;0
WireConnection;8;1;9;0
WireConnection;8;2;13;0
WireConnection;8;3;15;0
WireConnection;8;4;48;0
WireConnection;8;5;47;0
WireConnection;39;0;8;0
WireConnection;40;0;10;0
WireConnection;58;0;39;0
WireConnection;58;1;10;0
WireConnection;41;0;10;0
WireConnection;21;1;58;0
WireConnection;21;3;40;0
WireConnection;21;4;41;0
WireConnection;52;0;49;0
WireConnection;53;0;21;1
WireConnection;53;1;52;0
WireConnection;54;1;58;0
WireConnection;54;3;40;0
WireConnection;54;4;41;0
WireConnection;23;1;58;0
WireConnection;23;3;40;0
WireConnection;23;4;41;0
WireConnection;50;0;21;1
WireConnection;50;1;52;0
WireConnection;56;0;54;1
WireConnection;56;1;55;0
WireConnection;20;1;58;0
WireConnection;20;3;40;0
WireConnection;20;4;41;0
WireConnection;11;1;58;0
WireConnection;11;3;40;0
WireConnection;11;4;41;0
WireConnection;26;0;25;0
WireConnection;26;1;23;4
WireConnection;51;0;53;0
WireConnection;14;1;58;0
WireConnection;14;3;40;0
WireConnection;14;4;41;0
WireConnection;14;5;24;0
WireConnection;59;0;60;0
WireConnection;59;1;23;1
WireConnection;0;0;11;0
WireConnection;0;1;14;0
WireConnection;0;2;56;0
WireConnection;0;3;59;0
WireConnection;0;4;26;0
WireConnection;0;5;51;0
ASEEND*/
//CHKSM=0AE4979FD6418DFC15B4DEC878A9FA04127FE4EF