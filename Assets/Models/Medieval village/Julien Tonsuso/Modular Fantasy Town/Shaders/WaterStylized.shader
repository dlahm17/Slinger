// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "JTonsuso/WaterStylized"
{
	Properties
	{
		_m("m", Range( 0 , 1)) = 0.292
		_TessValue( "Max Tessellation", Range( 1, 32 ) ) = 4
		_TessMin( "Tess Min Distance", Float ) = 10
		_TessMax( "Tess Max Distance", Float ) = 25
		_WaterNormal("Water Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Float) = 0
		_WavesTiling("Waves Tiling", Float) = 2
		_WaveGuide("Wave Guide", 2D) = "white" {}
		_Speed("Speed", Range( 0 , 5)) = 0
		_PatternSpeed("Pattern Speed", Range( 0 , 5)) = 0
		_DeepColor("Deep Color", Color) = (0,0,0,0)
		_WaveHeight("Wave Height", Range( 0 , 5)) = 0.6470588
		_ShalowColor("Shalow Color", Color) = (1,1,1,0)
		_WaterDepth("Water Depth", Float) = 0
		_WaterFalloff("Water Falloff", Float) = 0
		_Foam("Foam", 2D) = "white" {}
		_FoamDepth("Foam Depth", Float) = 0
		_FoamFalloff("Foam Falloff", Float) = 0
		_Fresnelpower("Fresnel power", Float) = 4.38
		_fresnel("fresnel", Float) = 1
		_Smoothness("Smoothness", Range( 0 , 1)) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Patterndarkalpha("Pattern dark alpha", Range( 0 , 1)) = 0
		_PatternAlpha("Pattern Alpha", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float4 screenPos;
		};

		uniform sampler2D _WaveGuide;
		uniform float _WavesTiling;
		uniform float _Speed;
		uniform float _WaveHeight;
		uniform float _NormalScale;
		uniform float _PatternSpeed;
		uniform sampler2D _WaterNormal;
		uniform float4 _WaterNormal_ST;
		uniform float _fresnel;
		uniform float _Fresnelpower;
		uniform float4 _DeepColor;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _PatternAlpha;
		uniform float _Patterndarkalpha;
		uniform float4 _ShalowColor;
		uniform sampler2D _CameraDepthTexture;
		uniform float _WaterDepth;
		uniform float _WaterFalloff;
		uniform float _FoamDepth;
		uniform float _FoamFalloff;
		uniform sampler2D _Foam;
		uniform float4 _Foam_ST;
		uniform sampler2D _GrabTexture;
		uniform float _m;
		uniform float _Smoothness;
		uniform float _TessValue;
		uniform float _TessMin;
		uniform float _TessMax;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityDistanceBasedTess( v0.vertex, v1.vertex, v2.vertex, _TessMin, _TessMax, _TessValue );
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 temp_cast_0 = (_WavesTiling).xx;
			float Speed170 = ( _Time.x * _Speed );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 temp_cast_1 = (( Speed170 + (ase_vertex3Pos).y )).xx;
			float2 uv_TexCoord178 = v.texcoord.xy * temp_cast_0 + temp_cast_1;
			float3 ase_vertexNormal = v.normal.xyz;
			float3 VertexAnim185 = ( ( tex2Dlod( _WaveGuide, float4( uv_TexCoord178, 0, 1.0) ).r - 0.5 ) * ( ase_vertexNormal * _WaveHeight ) );
			v.vertex.xyz += VertexAnim185;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float PatternSpeed225 = ( _Time.x * _PatternSpeed );
			float2 uv_WaterNormal = i.uv_texcoord * _WaterNormal_ST.xy + _WaterNormal_ST.zw;
			float2 panner22 = ( PatternSpeed225 * float2( -0.03,0 ) + uv_WaterNormal);
			float2 panner19 = ( PatternSpeed225 * float2( 0.04,0.04 ) + uv_WaterNormal);
			float3 temp_output_24_0 = BlendNormals( UnpackScaleNormal( tex2D( _WaterNormal, panner22 ) ,_NormalScale ) , UnpackScaleNormal( tex2D( _WaterNormal, panner19 ) ,_NormalScale ) );
			o.Normal = temp_output_24_0;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNdotV220 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode220 = ( 0.0 + _fresnel * pow( 1.0 - fresnelNdotV220, _Fresnelpower ) );
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float2 panner194 = ( PatternSpeed225 * float2( 0.1,0.1 ) + uv_TextureSample0);
			float cos196 = cos( PatternSpeed225 );
			float sin196 = sin( PatternSpeed225 );
			float2 rotator196 = mul( panner194 - float2( 0,0 ) , float2x2( cos196 , -sin196 , sin196 , cos196 )) + float2( 0,0 );
			float lerpResult199 = lerp( 0.0 , tex2D( _TextureSample0, rotator196 ).r , _PatternAlpha);
			float2 panner207 = ( PatternSpeed225 * float2( -0.1,0.1 ) + uv_TextureSample0);
			float cos208 = cos( PatternSpeed225 );
			float sin208 = sin( PatternSpeed225 );
			float2 rotator208 = mul( panner207 - float2( 5,0 ) , float2x2( cos208 , -sin208 , sin208 , cos208 )) + float2( 5,0 );
			float lerpResult204 = lerp( 0.0 , tex2D( _TextureSample0, rotator208 ).r , _Patterndarkalpha);
			float4 temp_cast_0 = (lerpResult204).xxxx;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float eyeDepth1 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(ase_screenPos))));
			float temp_output_89_0 = abs( ( eyeDepth1 - ase_screenPos.w ) );
			float temp_output_94_0 = saturate( pow( ( temp_output_89_0 + _WaterDepth ) , _WaterFalloff ) );
			float4 lerpResult13 = lerp( ( ( _DeepColor + lerpResult199 ) - temp_cast_0 ) , _ShalowColor , temp_output_94_0);
			o.Albedo = ( fresnelNode220 + lerpResult13 ).rgb;
			float2 uv_Foam = i.uv_texcoord * _Foam_ST.xy + _Foam_ST.zw;
			float2 panner116 = ( PatternSpeed225 * float2( -0.01,0.01 ) + uv_Foam);
			float cos197 = cos( PatternSpeed225 );
			float sin197 = sin( PatternSpeed225 );
			float2 rotator197 = mul( panner116 - float2( 0,0 ) , float2x2( cos197 , -sin197 , sin197 , cos197 )) + float2( 0,0 );
			float temp_output_114_0 = ( saturate( pow( ( temp_output_89_0 + _FoamDepth ) , _FoamFalloff ) ) * ( 0.7 + tex2D( _Foam, rotator197 ).r ) );
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor240 = tex2Dproj( _GrabTexture, UNITY_PROJ_COORD( float4( ( (ase_grabScreenPosNorm).xy + (( temp_output_24_0 * _m )).xy ), 0.0 , 0.0 ) ) );
			o.Emission = ( ( temp_output_114_0 * ase_lightColor ) * screenColor240 ).rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
141;254;1281;1139;1696.927;-802.9998;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;224;-3096.099,-1528.477;Float;False;914.394;362.5317;Comment;4;228;227;226;225;Pattern Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;152;-2991.671,348.1415;Float;False;828.5967;315.5001;Screen depth difference to get intersection and fading effect with terrain and objects;4;89;2;1;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;228;-3061.381,-1290.409;Float;False;Property;_PatternSpeed;Pattern Speed;11;0;Create;True;0;0;False;0;0;0.017;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;226;-2975.2,-1478.476;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenPosInputsNode;2;-2941.67,451.6415;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;166;-3061.636,-1026.997;Float;False;914.394;362.5317;Comment;4;170;169;168;167;Wave Speed;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;151;-935.9057,-1082.484;Float;False;1281.603;457.1994;Blend panning normals to fake noving ripples;7;19;23;21;22;17;48;172;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;227;-2696.873,-1379.187;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenDepthNode;1;-2719.671,449.1414;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;168;-2940.737,-976.9966;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;173;-1960.303,1192.176;Float;False;2321.461;426.9865;Comment;13;185;184;183;182;181;180;179;178;177;176;175;174;212;Vertex Animation;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;167;-3026.918,-788.9296;Float;False;Property;_Speed;Speed;10;0;Create;True;0;0;False;0;0;3.4;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;225;-2424.696,-1435.801;Float;False;PatternSpeed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;193;-2676.473,-102.6195;Float;False;225;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;172;-880,-832;Float;False;225;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;153;-1061.858,615.8428;Float;False;1514.873;503.4238;Foam controls and texture;13;114;113;110;115;105;112;111;197;116;191;106;215;216;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-880,-1008;Float;False;0;17;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;192;-2583.579,-240.5374;Float;False;0;187;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;206;-2594.127,-526.377;Float;False;0;187;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;205;-2704.548,-381.4484;Float;False;225;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;3;-2512.271,494.4416;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;174;-1905.86,1439.382;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;169;-2662.41,-877.7075;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;106;-980.3371,862.5285;Float;False;0;105;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;194;-2298.063,-192.7393;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;19;-608,-928;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.04,0.04;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;22;-610.2062,-1043.484;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.03,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-580.7061,-753.7862;Float;False;Property;_NormalScale;Normal Scale;7;0;Create;True;0;0;False;0;0;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;191;-1040.924,1000.456;Float;False;225;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-258.2061,-1030.185;Float;True;Property;_Normal2;Normal2;6;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Instance;17;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;159;-1096.537,-417.0423;Float;False;1555.514;789.4329;Depths controls and colors;10;13;11;94;87;12;10;88;6;198;210;;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;175;-1582.239,1378.101;Float;False;170;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;170;-2390.233,-934.3207;Float;False;Speed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;176;-1667.096,1458.654;Float;False;False;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;196;-2080.34,-148.5581;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-757.7582,739.7433;Float;False;Property;_FoamDepth;Foam Depth;21;0;Create;True;0;0;False;0;0;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;116;-740.7935,877.6561;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.01,0.01;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.AbsOpNode;89;-2327.073,492.2576;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-256.3054,-814.2847;Float;True;Property;_WaterNormal;Water Normal;6;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;207;-2308.611,-479.5789;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;242;1242.15,524.2789;Float;False;Property;_m;m;0;0;Create;True;0;0;False;0;0.292;0.456;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-566.9584,801.6435;Float;False;Property;_FoamFalloff;Foam Falloff;22;0;Create;True;0;0;False;0;0;-4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;24;380.697,-877.6849;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;177;-1367.817,1420.698;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-1605.568,1265.434;Float;False;Property;_WavesTiling;Waves Tiling;8;0;Create;True;0;0;False;0;2;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;115;-577.5574,665.8428;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;237;1520.826,232.042;Float;False;864.9003;353.4998;Simple Refraction with normal perturbance;6;238;258;240;239;243;259;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;200;-1548.256,-204.4156;Float;False;Constant;_Float1;Float 1;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;187;-1838.5,-195.7105;Float;True;Property;_TextureSample0;Texture Sample 0;28;0;Create;True;0;0;False;0;f88780c519fc3754c8661ca3f5fb4919;f88780c519fc3754c8661ca3f5fb4919;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;201;-1879.884,76.92248;Float;False;Property;_PatternAlpha;Pattern Alpha;30;0;Create;True;0;0;False;0;0;0.078;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;197;-568.8939,947.2375;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-804.5447,181.7125;Float;False;Property;_WaterDepth;Water Depth;15;0;Create;True;0;0;False;0;0;1.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;208;-2090.888,-434.3977;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;5,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;202;-1848.878,-463.3118;Float;True;Property;_TextureSample1;Texture Sample 1;28;0;Create;True;0;0;False;0;f88780c519fc3754c8661ca3f5fb4919;f88780c519fc3754c8661ca3f5fb4919;True;0;False;white;Auto;False;Instance;187;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;243;1637.012,443.0481;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-627.0447,230.7119;Float;False;Property;_WaterFalloff;Water Falloff;16;0;Create;True;0;0;False;0;0;-6.36;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;178;-1213.838,1261.381;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;216;-277.5386,806.1537;Float;False;Constant;_Float3;Float 3;18;0;Create;True;0;0;False;0;0.7;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;110;-392.7578,674.7433;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;105;-374.6531,903.9451;Float;True;Property;_Foam;Foam;20;0;Create;True;0;0;False;0;9fbef4b79ca3b784ba023cb1331520d5;36dd0b22da8874ed38075789055ca664;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;203;-1543.203,-521.8447;Float;False;Constant;_Float2;Float 2;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;211;-1862.662,-567.5975;Float;False;Property;_Patterndarkalpha;Pattern dark alpha;29;0;Create;True;0;0;False;0;0;0.034;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;12;-868.4382,-246.5439;Float;False;Property;_DeepColor;Deep Color;12;0;Create;True;0;0;False;0;0,0,0,0;0.3088226,0.6567953,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;238;1531.825,282.0419;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-622.8499,105.3292;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;199;-1361.004,-196.0957;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;258;1846.939,364.944;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;113;-189.4207,701.3255;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;181;-562.5635,1348.352;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;180;-917.7855,1248.197;Float;True;Property;_WaveGuide;Wave Guide;9;0;Create;True;0;0;False;0;31890676c5b178840848afa665cb5a2f;31890676c5b178840848afa665cb5a2f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;179;-606.8795,1504.162;Float;False;Property;_WaveHeight;Wave Height;13;0;Create;True;0;0;False;0;0.6470588;1;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;198;-610.3566,-223.1138;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;204;-1352.552,-459.1518;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;87;-446.6499,191.7289;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;215;0.8974019,842.4462;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;259;1837.939,451.944;Float;False;True;True;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;182;-230.2042,1275.768;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;395.5293,694.1089;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;183;-235.5092,1429.433;Float;False;2;2;0;FLOAT3;1,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;239;2076.027,393.9418;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;219;1968.311,-933.0956;Float;False;Property;_fresnel;fresnel;26;0;Create;True;0;0;False;0;1;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;210;-330.4591,-303.9689;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LightColorNode;217;514.3193,291.2098;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.SaturateNode;94;-108.6347,234.7406;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;223;1901.467,-818.3865;Float;False;Property;_Fresnelpower;Fresnel power;25;0;Create;True;0;0;False;0;4.38;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-430.9323,17.96994;Float;False;Property;_ShalowColor;Shalow Color;14;0;Create;True;0;0;False;0;1,1,1,0;0.1790108,0.3098458,0.5661764,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;150;467.1957,-1501.783;Float;False;985.6011;418.6005;Get screen color for refraction and disturbe it with normals;7;96;97;98;65;149;164;165;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;13;160.0462,13.89008;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;184;-68.19994,1408.298;Float;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;218;763.9539,356.1018;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;240;2214.727,376.5417;Float;False;Global;_GrabScreen0;Grab Screen 0;1;0;Create;True;0;0;False;0;Object;-1;False;True;1;0;FLOAT4;0,0,0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;220;1932.168,-643.7488;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;149;487.4943,-1188.882;Float;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ScreenColorNode;65;1232.797,-1350.483;Float;False;Global;_WaterGrab;WaterGrab;-1;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;241;2878.213,-499.864;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ComponentMaskNode;165;814.6503,-1385.291;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DepthFade;246;-2770.377,793.2047;Float;False;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;1349.618,-48.33987;Float;False;Property;_WaterSmoothness;Water Smoothness;18;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;233;2223.45,-461.5407;Float;False;Property;_Smoothness;Smoothness;27;0;Create;True;0;0;False;0;1;0.919;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;104;1367.102,-379.3159;Float;False;Property;_WaterSpecular;Water Specular;17;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;132;1343.82,31.36561;Float;False;Property;_FoamSmoothness;Foam Smoothness;24;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;96;1041.296,-1346.683;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;133;1700.754,-9.222565;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;117;664.8759,-80.88965;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;185;85.15872,1400.32;Float;False;VertexAnim;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;131;1369.302,-281.0149;Float;False;Property;_FoamSpecular;Foam Specular;23;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SurfaceDepthNode;245;-2786.449,684.8179;Float;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;710.096,-1203.183;Float;False;Property;_Distortion;Distortion;19;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;98;888.1974,-1279.783;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;93;1688.937,-1065.468;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;186;3053.318,-82.64079;Float;False;185;0;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;108;100.0623,414.6281;Float;False;Constant;_Color0;Color 0;-1;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FaceVariableNode;247;-3049.053,691.9058;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;164;511.3026,-1442.425;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldReflectionVector;249;2062.558,-861.3702;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;222;2379.926,-554.637;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;130;1568.902,-279.7149;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3522.92,-553.5728;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;JTonsuso/WaterStylized;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;2;Translucent;0.5;True;False;0;False;Opaque;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;0;4;10;25;False;0.5;True;0;1;False;-1;10;False;-1;0;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;227;0;226;1
WireConnection;227;1;228;0
WireConnection;1;0;2;0
WireConnection;225;0;227;0
WireConnection;3;0;1;0
WireConnection;3;1;2;4
WireConnection;169;0;168;1
WireConnection;169;1;167;0
WireConnection;194;0;192;0
WireConnection;194;1;193;0
WireConnection;19;0;21;0
WireConnection;19;1;172;0
WireConnection;22;0;21;0
WireConnection;22;1;172;0
WireConnection;23;1;22;0
WireConnection;23;5;48;0
WireConnection;170;0;169;0
WireConnection;176;0;174;0
WireConnection;196;0;194;0
WireConnection;196;2;193;0
WireConnection;116;0;106;0
WireConnection;116;1;191;0
WireConnection;89;0;3;0
WireConnection;17;1;19;0
WireConnection;17;5;48;0
WireConnection;207;0;206;0
WireConnection;207;1;205;0
WireConnection;24;0;23;0
WireConnection;24;1;17;0
WireConnection;177;0;175;0
WireConnection;177;1;176;0
WireConnection;115;0;89;0
WireConnection;115;1;111;0
WireConnection;187;1;196;0
WireConnection;197;0;116;0
WireConnection;197;2;191;0
WireConnection;208;0;207;0
WireConnection;208;2;205;0
WireConnection;202;1;208;0
WireConnection;243;0;24;0
WireConnection;243;1;242;0
WireConnection;178;0;212;0
WireConnection;178;1;177;0
WireConnection;110;0;115;0
WireConnection;110;1;112;0
WireConnection;105;1;197;0
WireConnection;88;0;89;0
WireConnection;88;1;6;0
WireConnection;199;0;200;0
WireConnection;199;1;187;1
WireConnection;199;2;201;0
WireConnection;258;0;238;0
WireConnection;113;0;110;0
WireConnection;180;1;178;0
WireConnection;198;0;12;0
WireConnection;198;1;199;0
WireConnection;204;0;203;0
WireConnection;204;1;202;1
WireConnection;204;2;211;0
WireConnection;87;0;88;0
WireConnection;87;1;10;0
WireConnection;215;0;216;0
WireConnection;215;1;105;1
WireConnection;259;0;243;0
WireConnection;182;0;180;1
WireConnection;114;0;113;0
WireConnection;114;1;215;0
WireConnection;183;0;181;0
WireConnection;183;1;179;0
WireConnection;239;0;258;0
WireConnection;239;1;259;0
WireConnection;210;0;198;0
WireConnection;210;1;204;0
WireConnection;94;0;87;0
WireConnection;13;0;210;0
WireConnection;13;1;11;0
WireConnection;13;2;94;0
WireConnection;184;0;182;0
WireConnection;184;1;183;0
WireConnection;218;0;114;0
WireConnection;218;1;217;0
WireConnection;240;0;239;0
WireConnection;220;2;219;0
WireConnection;220;3;223;0
WireConnection;149;0;24;0
WireConnection;65;0;96;0
WireConnection;241;0;218;0
WireConnection;241;1;240;0
WireConnection;165;0;164;0
WireConnection;96;0;165;0
WireConnection;96;1;98;0
WireConnection;133;0;26;0
WireConnection;133;1;132;0
WireConnection;133;2;114;0
WireConnection;117;0;13;0
WireConnection;117;1;108;0
WireConnection;117;2;114;0
WireConnection;185;0;184;0
WireConnection;245;0;2;0
WireConnection;98;0;149;0
WireConnection;98;1;97;0
WireConnection;93;1;65;0
WireConnection;93;2;94;0
WireConnection;222;0;220;0
WireConnection;222;1;13;0
WireConnection;130;0;104;0
WireConnection;130;1;131;0
WireConnection;130;2;114;0
WireConnection;0;0;222;0
WireConnection;0;1;24;0
WireConnection;0;2;241;0
WireConnection;0;4;233;0
WireConnection;0;11;186;0
ASEEND*/
//CHKSM=61029BAFDAC76D5ED5FE6A4E367CAC108F8AB4F0