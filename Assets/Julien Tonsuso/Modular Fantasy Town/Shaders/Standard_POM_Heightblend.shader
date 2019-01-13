// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JTonsuso/Standard_POM_Heightblend"
{
	Properties
	{
		_Texture_01("Texture_01", 2D) = "white" {}
		_Texture_01Height("Texture_01 Height", 2D) = "white" {}
		_Texture_01Normal("Texture_01 Normal", 2D) = "bump" {}
		_Texture_01NormalScale("Texture_01 Normal Scale", Range( 0 , 1)) = 0
		_Texture_01MetalSmooth("Texture_01 MetalSmooth", 2D) = "white" {}
		_Texture_01Metallic("Texture_01  Metallic", Range( 0 , 1)) = 0
		_Texture_01Smoothness("Texture_01 Smoothness", Range( 0 , 1)) = 0.5
		_Texture_02("Texture_02", 2D) = "white" {}
		_Texture_02Height("Texture_02 Height", 2D) = "white" {}
		_Texture_02Normal("Texture_02 Normal", 2D) = "bump" {}
		_Texture_02NormalScale("Texture_02 Normal Scale", Range( 0 , 1)) = 0
		_Texture_02MetalSmooth("Texture_02 MetalSmooth", 2D) = "white" {}
		_Texture_02Metallic("Texture_02 Metallic", Range( 0 , 1)) = 0
		_Texture_02Smoothness("Texture_02 Smoothness", Range( 0 , 1)) = 0.5
		_ParallaxScale("Parallax Scale", Range( 0 , 1)) = 0.4247461
		_RefPlane("Ref Plane", Range( 0 , 1)) = 0.4247461
		_Texture_01HeightOffset("Texture_01 Height Offset", Range( -10 , 10)) = 1
		_Texture_02HeightScale("Texture_02 Height Scale", Range( 0 , 10)) = 1
		_Texture_02HeightOffset("Texture_02 Height Offset", Range( -10 , 10)) = 1
		_Texture_02ColorScale("Texture_02 Color Scale", Range( 0 , 20)) = 1
		_Texture_02ColorOffset("Texture_02 Color Offset", Range( -20 , 20)) = 1
		_Curvature("Curvature", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[Header(Parallax Occlusion Mapping)]
		_CurvFix("Curvature Bias", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
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
			float4 vertexColor : COLOR;
		};

		uniform float _Texture_01NormalScale;
		uniform sampler2D _Texture_01Normal;
		uniform sampler2D _Texture_01;
		uniform float4 _Texture_01_ST;
		uniform sampler2D _Texture_02Height;
		uniform float _ParallaxScale;
		uniform float _RefPlane;
		uniform float _CurvFix;
		uniform float2 _Curvature;
		uniform float4 _Texture_02Height_ST;
		uniform float _Texture_02NormalScale;
		uniform sampler2D _Texture_02Normal;
		uniform sampler2D _Texture_01Height;
		uniform float _Texture_01HeightOffset;
		uniform float _Texture_02HeightScale;
		uniform float _Texture_02HeightOffset;
		uniform float _Texture_02ColorScale;
		uniform float _Texture_02ColorOffset;
		uniform sampler2D _Texture_02;
		uniform float _Texture_01Metallic;
		uniform sampler2D _Texture_01MetalSmooth;
		uniform float _Texture_02Metallic;
		uniform sampler2D _Texture_02MetalSmooth;
		uniform float _Texture_01Smoothness;
		uniform float _Texture_02Smoothness;


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
			float2 uv_Texture_01 = i.uv_texcoord * _Texture_01_ST.xy + _Texture_01_ST.zw;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float2 OffsetPOM175 = POM( _Texture_02Height, uv_Texture_01, ddx(uv_Texture_01), ddy(uv_Texture_01), ase_worldNormal, ase_worldViewDir, i.viewDir, 128, 128, _ParallaxScale, _RefPlane, _Texture_02Height_ST.xy, _Curvature, 0 );
			float2 temp_output_176_0 = ddx( uv_Texture_01 );
			float2 temp_output_177_0 = ddy( uv_Texture_01 );
			float4 appendResult135 = (float4(( UnpackNormal( tex2D( _Texture_01Height, uv_Texture_01, temp_output_176_0, temp_output_177_0 ) ).r * _Texture_01HeightOffset ) , UnpackNormal( tex2D( _Texture_02Height, uv_Texture_01, temp_output_176_0, temp_output_177_0 ) ).r , 0.0 , 0.0));
			float4 temp_output_101_0 = (( 1.0 - appendResult135 )*_Texture_02HeightScale + _Texture_02HeightOffset);
			float temp_output_97_0 = (i.vertexColor.r*_Texture_02ColorScale + _Texture_02ColorOffset);
			float4 Blender124 = ( 1.0 - saturate( ( temp_output_101_0 + temp_output_97_0 ) ) );
			float3 lerpResult72 = lerp( UnpackScaleNormal( tex2D( _Texture_01Normal, OffsetPOM175, temp_output_176_0, temp_output_177_0 ) ,_Texture_01NormalScale ) , UnpackScaleNormal( tex2D( _Texture_02Normal, OffsetPOM175, temp_output_176_0, temp_output_177_0 ) ,_Texture_02NormalScale ) , (Blender124).y);
			o.Normal = lerpResult72;
			float4 lerpResult29 = lerp( tex2D( _Texture_01, OffsetPOM175, temp_output_176_0, temp_output_177_0 ) , tex2D( _Texture_02, OffsetPOM175, temp_output_176_0, temp_output_177_0 ) , (Blender124).y);
			o.Albedo = lerpResult29.rgb;
			float4 tex2DNode205 = tex2D( _Texture_01MetalSmooth, OffsetPOM175, temp_output_176_0, temp_output_177_0 );
			float4 tex2DNode141 = tex2D( _Texture_02MetalSmooth, OffsetPOM175, temp_output_176_0, temp_output_177_0 );
			float temp_output_156_0 = (Blender124).y;
			float lerpResult143 = lerp( ( _Texture_01Metallic * tex2DNode205.r ) , ( _Texture_02Metallic * tex2DNode141.r ) , temp_output_156_0);
			o.Metallic = lerpResult143;
			float lerpResult144 = lerp( ( tex2DNode205.a * _Texture_01Smoothness ) , ( tex2DNode141.a * _Texture_02Smoothness ) , temp_output_156_0);
			o.Smoothness = lerpResult144;
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
				half4 color : COLOR0;
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
				o.color = v.color;
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
				surfIN.vertexColor = IN.color;
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
141;260;1281;1133;4407.074;-599.7485;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;133;-5141.585,257.4755;Float;False;0;28;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DdyOpNode;177;-4714.553,84.61127;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;197;-4728.077,1527.047;Float;True;Property;_Texture_01Height;Texture_01 Height;1;0;Create;True;0;0;False;0;None;da217482a79855a4eaf268b688384322;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.DdxOpNode;176;-4714.553,20.61095;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;209;-3643.029,1952.614;Float;False;Property;_Texture_01HeightOffset;Texture_01 Height Offset;17;0;Create;True;0;0;False;0;1;-0.45;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;180;-4052.924,1544.287;Float;True;Property;_Texture_01H;Texture_01 H;6;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;200;-4757.709,1773.627;Float;True;Property;_Texture_02Height;Texture_02 Height;8;0;Create;True;0;0;False;0;None;34360bab955ecb84da6e27b73853ef80;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;210;-3239.676,1621.059;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;178;-4064.025,1789.833;Float;True;Property;_Texture_02H;Texture_02 H;6;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;135;-1572.167,1617.15;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;102;-1305.323,1659.33;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;118;-1453.25,2194.306;Float;False;Property;_Texture_02ColorScale;Texture_02 Color Scale;20;0;Create;True;0;0;False;0;1;5.79;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;122;-1457.25,2288.306;Float;False;Property;_Texture_02ColorOffset;Texture_02 Color Offset;21;0;Create;True;0;0;False;0;1;-9.8;-20;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;94;-1649.535,2068.477;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;116;-1450.072,1944.123;Float;False;Property;_Texture_02HeightOffset;Texture_02 Height Offset;19;0;Create;True;0;0;False;0;1;5.13;-10;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;113;-1453.25,1842.305;Float;False;Property;_Texture_02HeightScale;Texture_02 Height Scale;18;0;Create;True;0;0;False;0;1;1.49;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;97;-1049.249,2145.306;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;101;-963.8195,1697.723;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.5;False;2;FLOAT;-0.5;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;107;-621.2489,1986.305;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SaturateNode;108;-477.6494,1982.305;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;183;-3896.291,981.1959;Float;False;Property;_ParallaxScale;Parallax Scale;14;0;Create;True;0;0;False;0;0.4247461;0.031;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;204;-3827.32,1073.243;Float;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;184;-3860.829,1227.756;Float;False;Property;_RefPlane;Ref Plane;15;0;Create;True;0;0;False;0;0.4247461;0.224;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;214;-3764.074,1375.749;Float;False;Property;_Curvature;Curvature;22;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.OneMinusNode;109;-333.6497,1982.305;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;175;-3310.11,1053.835;Float;False;0;128;128;10;0.02;0;False;1,1;True;10,0;Texture2D;7;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT2;0,0;False;6;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;141;-2642.284,1288.206;Float;True;Property;_Texture_02MetalSmooth;Texture_02 MetalSmooth;11;0;Create;True;0;0;False;0;None;31240771e7230764c914176516cdf011;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;137;-2642.284,856.2062;Float;False;Property;_Texture_01Metallic;Texture_01  Metallic;5;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;205;-2623.061,928.5052;Float;True;Property;_Texture_01MetalSmooth;Texture_01 MetalSmooth;4;0;Create;True;0;0;False;0;None;390010cbf316e95468c91966c2df50f6;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;145;-2645.967,1560.206;Float;False;124;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;211;-1970.719,762.9949;Float;False;Property;_Texture_02NormalScale;Texture_02 Normal Scale;10;0;Create;True;0;0;False;0;0;0.4;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;124;-189.6497,1982.305;Float;False;Blender;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-1933.719,541.9949;Float;False;Property;_Texture_01NormalScale;Texture_01 Normal Scale;3;0;Create;True;0;0;False;0;0;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;130;-798.1497,473.8265;Float;False;124;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;146;-2642.284,1208.206;Float;False;Property;_Texture_02Metallic;Texture_02 Metallic;12;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;154;-2636.623,1470.999;Float;False;Property;_Texture_02Smoothness;Texture_02 Smoothness;13;0;Create;True;0;0;False;0;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;126;-1551.568,835.4189;Float;False;124;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;147;-2643.584,1127.806;Float;False;Property;_Texture_01Smoothness;Texture_01 Smoothness;6;0;Create;True;0;0;False;0;0.5;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;68;-1559.276,443.6175;Float;True;Property;_Texture_01Normal;Texture_01 Normal;2;0;Create;True;0;0;False;0;None;5b574659d33bc90438dd85c960f64929;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;69;-1557.968,630.619;Float;True;Property;_Texture_02Normal;Texture_02 Normal;9;0;Create;True;0;0;False;0;None;af48209b5881a6f47b7edd9bc5f7698c;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-771.2105,268.8795;Float;True;Property;_Texture_02;Texture_02;7;0;Create;True;0;0;False;0;None;c82598a9d6b81d64db46d04cdd4e4ba0;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;125;-1359.568,835.4189;Float;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-792.9498,69.42689;Float;True;Property;_Texture_01;Texture_01;0;0;Create;True;0;0;False;0;None;1defe1f4b62d5ec47919071d684fdb75;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;156;-2466.283,1560.206;Float;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;142;-2230.606,1247.332;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;129;-606.1497,473.8265;Float;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;152;-2254.794,1364.991;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;-2210.282,949.2061;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-2182.206,1036.521;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;72;-591.075,624.2684;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;190;-4211.433,1073.008;Float;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;191;-4403.436,1073.008;Float;False;124;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;208;-2297.661,1834.183;Float;False;Property;_Texture_01HeightScale;Texture_01 Height Scale;16;0;Create;True;0;0;False;0;1;1.43;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;29;-335.1496,350.8265;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;144;-754.8679,950.6745;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;143;-717.5102,790.1998;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;105;-816.249,2071.306;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;106;-810.249,1981.305;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;207;-1910.644,1787.789;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;-0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;106.4669,580.0444;Float;False;True;4;Float;ASEMaterialInspector;0;0;Standard;JTonsuso/Standard_POM_Heightblend;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;177;0;133;0
WireConnection;176;0;133;0
WireConnection;180;0;197;0
WireConnection;180;1;133;0
WireConnection;180;3;176;0
WireConnection;180;4;177;0
WireConnection;210;0;180;1
WireConnection;210;1;209;0
WireConnection;178;0;200;0
WireConnection;178;1;133;0
WireConnection;178;3;176;0
WireConnection;178;4;177;0
WireConnection;135;0;210;0
WireConnection;135;1;178;1
WireConnection;102;0;135;0
WireConnection;97;0;94;1
WireConnection;97;1;118;0
WireConnection;97;2;122;0
WireConnection;101;0;102;0
WireConnection;101;1;113;0
WireConnection;101;2;116;0
WireConnection;107;0;101;0
WireConnection;107;1;97;0
WireConnection;108;0;107;0
WireConnection;109;0;108;0
WireConnection;175;0;133;0
WireConnection;175;1;200;0
WireConnection;175;2;183;0
WireConnection;175;3;204;0
WireConnection;175;4;184;0
WireConnection;175;5;214;0
WireConnection;141;1;175;0
WireConnection;141;3;176;0
WireConnection;141;4;177;0
WireConnection;205;1;175;0
WireConnection;205;3;176;0
WireConnection;205;4;177;0
WireConnection;124;0;109;0
WireConnection;68;1;175;0
WireConnection;68;3;176;0
WireConnection;68;4;177;0
WireConnection;68;5;212;0
WireConnection;69;1;175;0
WireConnection;69;3;176;0
WireConnection;69;4;177;0
WireConnection;69;5;211;0
WireConnection;2;1;175;0
WireConnection;2;3;176;0
WireConnection;2;4;177;0
WireConnection;125;0;126;0
WireConnection;28;1;175;0
WireConnection;28;3;176;0
WireConnection;28;4;177;0
WireConnection;156;0;145;0
WireConnection;142;0;146;0
WireConnection;142;1;141;1
WireConnection;129;0;130;0
WireConnection;152;0;141;4
WireConnection;152;1;154;0
WireConnection;138;0;137;0
WireConnection;138;1;205;1
WireConnection;139;0;205;4
WireConnection;139;1;147;0
WireConnection;72;0;68;0
WireConnection;72;1;69;0
WireConnection;72;2;125;0
WireConnection;190;0;191;0
WireConnection;29;0;28;0
WireConnection;29;1;2;0
WireConnection;29;2;129;0
WireConnection;144;0;139;0
WireConnection;144;1;152;0
WireConnection;144;2;156;0
WireConnection;143;0;138;0
WireConnection;143;1;142;0
WireConnection;143;2;156;0
WireConnection;105;0;97;0
WireConnection;106;0;101;0
WireConnection;207;1;208;0
WireConnection;207;2;209;0
WireConnection;0;0;29;0
WireConnection;0;1;72;0
WireConnection;0;3;143;0
WireConnection;0;4;144;0
ASEEND*/
//CHKSM=C392D862DEC00E2D2CFD9A627ED6A105717BC11A