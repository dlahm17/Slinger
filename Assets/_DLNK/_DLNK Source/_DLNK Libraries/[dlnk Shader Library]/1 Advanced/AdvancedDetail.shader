// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:DLNK Shaders/Advanced/PBRAdvanced,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2865,x:33492,y:32641,varname:node_2865,prsc:2|diff-915-OUT,spec-1857-OUT,gloss-8259-OUT,normal-1487-OUT,emission-4297-OUT,difocc-5831-OUT,spcocc-5831-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:32231,y:32662,varname:node_6343,prsc:2|A-6665-RGB,B-7736-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31932,y:32822,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31921,y:32620,ptovrint:True,ptlb:Albedo,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7427-OUT;n:type:ShaderForge.SFN_Tex2d,id:5964,x:32336,y:32791,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-7427-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:32500,y:32222,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:_Metallic,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Slider,id:1813,x:32197,y:32191,ptovrint:True,ptlb:Gloss,ptin:_Glossiness,varname:_Glossiness,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8,max:2;n:type:ShaderForge.SFN_Multiply,id:328,x:32578,y:32526,varname:node_328,prsc:2|A-982-A,B-1813-OUT;n:type:ShaderForge.SFN_Multiply,id:688,x:32591,y:32365,varname:node_688,prsc:2|A-982-RGB,B-358-OUT;n:type:ShaderForge.SFN_Vector3,id:8143,x:32336,y:32946,varname:node_8143,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Lerp,id:7874,x:32520,y:32808,varname:node_7874,prsc:2|A-8143-OUT,B-5964-RGB,T-287-OUT;n:type:ShaderForge.SFN_ValueProperty,id:287,x:32337,y:33050,ptovrint:False,ptlb:Normal Smooth,ptin:_NormalSmooth,varname:_NormalSmooth,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_TexCoord,id:9982,x:30372,y:32388,varname:node_9982,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:7427,x:30393,y:32248,varname:node_7427,prsc:2|A-9982-UVOUT,B-6981-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6981,x:30122,y:32296,ptovrint:False,ptlb:UV Master Tiling,ptin:_UVMasterTiling,varname:_UVMasterTiling,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:982,x:32308,y:32362,ptovrint:True,ptlb:Metalness,ptin:_MetallicGlossMap,varname:_MetallicGlossMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7427-OUT;n:type:ShaderForge.SFN_Multiply,id:4297,x:31932,y:33025,varname:node_4297,prsc:2|A-3007-RGB,B-5962-OUT;n:type:ShaderForge.SFN_Tex2d,id:3007,x:31765,y:33008,ptovrint:True,ptlb:Emission,ptin:_EmissionMap,varname:_EmissionMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ValueProperty,id:5962,x:31872,y:33190,ptovrint:False,ptlb:Emission LM,ptin:_EmissionLM,varname:_EmissionLM,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:6982,x:31555,y:32144,ptovrint:True,ptlb:Occlusion,ptin:_OcclusionMap,varname:_OcclusionMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7427-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6010,x:31665,y:32368,ptovrint:True,ptlb:Occlusion Mult,ptin:_OcclusionStrenght,varname:_OcclusionStrenght,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:4269,x:32297,y:33442,ptovrint:True,ptlb:Detail Albedo,ptin:_DetailAlbedoMap,varname:_DetailAlbedoMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9251-OUT;n:type:ShaderForge.SFN_Tex2d,id:1649,x:32724,y:33464,ptovrint:True,ptlb:Detail Bump,ptin:_DetailNormalMap,varname:_DetailNormalMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True|UVIN-9251-OUT;n:type:ShaderForge.SFN_Lerp,id:2921,x:32938,y:33414,varname:node_2921,prsc:2|A-1649-RGB,B-8143-OUT,T-6074-OUT;n:type:ShaderForge.SFN_ValueProperty,id:590,x:32724,y:33694,ptovrint:True,ptlb:Detail Bump Smooth,ptin:_DetailNormalMapScale,varname:_DetailNormalMapScale,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:1950,x:32520,y:33022,ptovrint:False,ptlb:Detail Mask,ptin:_DetailMask,varname:_DetailMask,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-7427-OUT,MIP-53-OUT;n:type:ShaderForge.SFN_Lerp,id:1487,x:32832,y:32752,varname:node_1487,prsc:2|A-7874-OUT,B-2921-OUT,T-8121-OUT;n:type:ShaderForge.SFN_Multiply,id:9828,x:32766,y:32932,varname:node_9828,prsc:2|A-5328-OUT,B-2573-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2573,x:32753,y:33083,ptovrint:False,ptlb:Detail Mask Mult,ptin:_DetailMaskMult,varname:_DetailMaskMult,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:915,x:32156,y:32847,varname:node_915,prsc:2|A-6343-OUT,B-3235-OUT,T-8121-OUT;n:type:ShaderForge.SFN_OneMinus,id:6074,x:32894,y:33616,varname:node_6074,prsc:2|IN-590-OUT;n:type:ShaderForge.SFN_Clamp01,id:8121,x:32924,y:32961,varname:node_8121,prsc:2|IN-9828-OUT;n:type:ShaderForge.SFN_Multiply,id:1224,x:31865,y:32282,varname:node_1224,prsc:2|A-4702-VOUT,B-6010-OUT;n:type:ShaderForge.SFN_OneMinus,id:7361,x:31865,y:32411,varname:node_7361,prsc:2|IN-6010-OUT;n:type:ShaderForge.SFN_Clamp01,id:8159,x:32158,y:32282,varname:node_8159,prsc:2|IN-8758-OUT;n:type:ShaderForge.SFN_Add,id:8758,x:31973,y:32282,varname:node_8758,prsc:2|A-7361-OUT,B-1224-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2674,x:30712,y:32091,ptovrint:False,ptlb:UV Secondary Tiling,ptin:_UVSecondaryTiling,varname:_UVMasterTiling_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:9251,x:30712,y:32154,varname:node_9251,prsc:2|A-7427-OUT,B-2674-OUT;n:type:ShaderForge.SFN_Lerp,id:9423,x:32861,y:32373,varname:node_9423,prsc:2|A-688-OUT,B-1989-OUT,T-8121-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6109,x:33048,y:33563,ptovrint:False,ptlb:Detail Gloss,ptin:_DetailGloss,varname:node_6109,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6042,x:33077,y:33464,ptovrint:False,ptlb:Detail Metal,ptin:_DetailMetal,varname:node_6042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:7228,x:33318,y:33527,ptovrint:False,ptlb:Detail Metalness,ptin:_DetailMetalness,varname:node_7228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9251-OUT;n:type:ShaderForge.SFN_Multiply,id:1989,x:33135,y:33235,varname:node_1989,prsc:2|A-7228-RGB,B-6042-OUT;n:type:ShaderForge.SFN_Multiply,id:4812,x:33486,y:33340,varname:node_4812,prsc:2|A-7228-A,B-6109-OUT;n:type:ShaderForge.SFN_Lerp,id:8259,x:32861,y:32616,varname:node_8259,prsc:2|A-328-OUT,B-4812-OUT,T-8121-OUT;n:type:ShaderForge.SFN_Desaturate,id:1857,x:33020,y:32373,varname:node_1857,prsc:2|COL-9423-OUT;n:type:ShaderForge.SFN_Lerp,id:5831,x:33184,y:32707,varname:node_5831,prsc:2|A-8159-OUT,B-3880-OUT,T-8121-OUT;n:type:ShaderForge.SFN_Color,id:9148,x:32116,y:33216,ptovrint:False,ptlb:Detail Color,ptin:_DetailColor,varname:node_9148,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8308824,c2:0.8308824,c3:0.8308824,c4:1;n:type:ShaderForge.SFN_Multiply,id:3235,x:32337,y:33259,varname:node_3235,prsc:2|A-9148-RGB,B-4269-RGB;n:type:ShaderForge.SFN_Multiply,id:9317,x:33232,y:33715,varname:node_9317,prsc:2|A-9094-VOUT,B-3422-OUT;n:type:ShaderForge.SFN_OneMinus,id:3011,x:33088,y:33877,varname:node_3011,prsc:2|IN-3422-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3422,x:33232,y:33908,ptovrint:False,ptlb:Detail Occlusion Mult,ptin:_DetailOcclusionMult,varname:node_3422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:9316,x:32687,y:33831,ptovrint:False,ptlb:Detail Occlusion,ptin:_DetailOcclusion,varname:node_9316,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9251-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:4702,x:31783,y:32032,varname:node_4702,prsc:2|IN-6982-RGB;n:type:ShaderForge.SFN_RgbToHsv,id:9094,x:32916,y:33791,varname:node_9094,prsc:2|IN-9316-RGB;n:type:ShaderForge.SFN_Add,id:9555,x:33414,y:33730,varname:node_9555,prsc:2|A-9317-OUT,B-3011-OUT;n:type:ShaderForge.SFN_Clamp01,id:3880,x:33582,y:33730,varname:node_3880,prsc:2|IN-9555-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:8199,x:32543,y:33207,varname:node_8199,prsc:2|IN-1950-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:5328,x:32854,y:33154,ptovrint:False,ptlb:Invert Mask,ptin:_InvertMask,varname:node_5328,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-8199-VOUT,B-9421-OUT;n:type:ShaderForge.SFN_OneMinus,id:9421,x:32842,y:33293,varname:node_9421,prsc:2|IN-8199-VOUT;n:type:ShaderForge.SFN_Slider,id:53,x:32233,y:33133,ptovrint:False,ptlb:Mask Smooth,ptin:_MaskSmooth,varname:node_53,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;proporder:6665-7736-5964-287-358-1813-6981-982-3007-5962-6982-6010-1950-53-5328-2573-2674-9148-4269-1649-590-7228-6042-6109-9316-3422;pass:END;sub:END;*/

Shader "DLNK Shaders/Advanced/AdvancedMix" {
    Properties {
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Albedo", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _NormalSmooth ("Normal Smooth", Float ) = 0
        _Metallic ("Metallic", Range(0, 2)) = 1
        _Glossiness ("Gloss", Range(0, 2)) = 0.8
        _UVMasterTiling ("UV Master Tiling", Float ) = 1
        _MetallicGlossMap ("Metalness", 2D) = "white" {}
        _EmissionMap ("Emission", 2D) = "white" {}
        _EmissionLM ("Emission LM", Float ) = 0
        _OcclusionMap ("Occlusion", 2D) = "white" {}
        _OcclusionStrenght ("Occlusion Mult", Float ) = 1
        _DetailMask ("Detail Mask", 2D) = "white" {}
        _MaskSmooth ("Mask Smooth", Range(0, 5)) = 0
        [MaterialToggle] _InvertMask ("Invert Mask", Float ) = 0
        _DetailMaskMult ("Detail Mask Mult", Float ) = 0
        _UVSecondaryTiling ("UV Secondary Tiling", Float ) = 2
        _DetailColor ("Detail Color", Color) = (0.8308824,0.8308824,0.8308824,1)
        _DetailAlbedoMap ("Detail Albedo", 2D) = "white" {}
        _DetailNormalMap ("Detail Bump", 2D) = "bump" {}
        _DetailNormalMapScale ("Detail Bump Smooth", Float ) = 1
        _DetailMetalness ("Detail Metalness", 2D) = "white" {}
        _DetailMetal ("Detail Metal", Float ) = 0
        _DetailGloss ("Detail Gloss", Float ) = 0
        _DetailOcclusion ("Detail Occlusion", 2D) = "white" {}
        _DetailOcclusionMult ("Detail Occlusion Mult", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Metallic;
            uniform float _Glossiness;
            uniform float _NormalSmooth;
            uniform float _UVMasterTiling;
            uniform sampler2D _MetallicGlossMap; uniform float4 _MetallicGlossMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _EmissionLM;
            uniform sampler2D _OcclusionMap; uniform float4 _OcclusionMap_ST;
            uniform float _OcclusionStrenght;
            uniform sampler2D _DetailAlbedoMap; uniform float4 _DetailAlbedoMap_ST;
            uniform sampler2D _DetailNormalMap; uniform float4 _DetailNormalMap_ST;
            uniform float _DetailNormalMapScale;
            uniform sampler2D _DetailMask; uniform float4 _DetailMask_ST;
            uniform float _DetailMaskMult;
            uniform float _UVSecondaryTiling;
            uniform float _DetailGloss;
            uniform float _DetailMetal;
            uniform sampler2D _DetailMetalness; uniform float4 _DetailMetalness_ST;
            uniform float4 _DetailColor;
            uniform float _DetailOcclusionMult;
            uniform sampler2D _DetailOcclusion; uniform float4 _DetailOcclusion_ST;
            uniform fixed _InvertMask;
            uniform float _MaskSmooth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_8143 = float3(0,0,1);
                float2 node_7427 = (i.uv0*_UVMasterTiling);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(node_7427, _BumpMap)));
                float2 node_9251 = (node_7427*_UVSecondaryTiling);
                float3 _DetailNormalMap_var = UnpackNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(node_9251, _DetailNormalMap)));
                float4 _DetailMask_var = tex2Dlod(_DetailMask,float4(TRANSFORM_TEX(node_7427, _DetailMask),0.0,_MaskSmooth));
                float4 node_8199_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_8199_p = lerp(float4(float4(_DetailMask_var.rgb,0.0).zy, node_8199_k.wz), float4(float4(_DetailMask_var.rgb,0.0).yz, node_8199_k.xy), step(float4(_DetailMask_var.rgb,0.0).z, float4(_DetailMask_var.rgb,0.0).y));
                float4 node_8199_q = lerp(float4(node_8199_p.xyw, float4(_DetailMask_var.rgb,0.0).x), float4(float4(_DetailMask_var.rgb,0.0).x, node_8199_p.yzx), step(node_8199_p.x, float4(_DetailMask_var.rgb,0.0).x));
                float node_8199_d = node_8199_q.x - min(node_8199_q.w, node_8199_q.y);
                float node_8199_e = 1.0e-10;
                float3 node_8199 = float3(abs(node_8199_q.z + (node_8199_q.w - node_8199_q.y) / (6.0 * node_8199_d + node_8199_e)), node_8199_d / (node_8199_q.x + node_8199_e), node_8199_q.x);;
                float node_8121 = saturate((lerp( node_8199.b, (1.0 - node_8199.b), _InvertMask )*_DetailMaskMult));
                float3 normalLocal = lerp(lerp(node_8143,_BumpMap_var.rgb,_NormalSmooth),lerp(_DetailNormalMap_var.rgb,node_8143,(1.0 - _DetailNormalMapScale)),node_8121);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MetallicGlossMap_var = tex2D(_MetallicGlossMap,TRANSFORM_TEX(node_7427, _MetallicGlossMap));
                float4 _DetailMetalness_var = tex2D(_DetailMetalness,TRANSFORM_TEX(node_9251, _DetailMetalness));
                float gloss = lerp((_MetallicGlossMap_var.a*_Glossiness),(_DetailMetalness_var.a*_DetailGloss),node_8121);
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _OcclusionMap_var = tex2D(_OcclusionMap,TRANSFORM_TEX(node_7427, _OcclusionMap));
                float4 node_4702_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_4702_p = lerp(float4(float4(_OcclusionMap_var.rgb,0.0).zy, node_4702_k.wz), float4(float4(_OcclusionMap_var.rgb,0.0).yz, node_4702_k.xy), step(float4(_OcclusionMap_var.rgb,0.0).z, float4(_OcclusionMap_var.rgb,0.0).y));
                float4 node_4702_q = lerp(float4(node_4702_p.xyw, float4(_OcclusionMap_var.rgb,0.0).x), float4(float4(_OcclusionMap_var.rgb,0.0).x, node_4702_p.yzx), step(node_4702_p.x, float4(_OcclusionMap_var.rgb,0.0).x));
                float node_4702_d = node_4702_q.x - min(node_4702_q.w, node_4702_q.y);
                float node_4702_e = 1.0e-10;
                float3 node_4702 = float3(abs(node_4702_q.z + (node_4702_q.w - node_4702_q.y) / (6.0 * node_4702_d + node_4702_e)), node_4702_d / (node_4702_q.x + node_4702_e), node_4702_q.x);;
                float4 _DetailOcclusion_var = tex2D(_DetailOcclusion,TRANSFORM_TEX(node_9251, _DetailOcclusion));
                float4 node_9094_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_9094_p = lerp(float4(float4(_DetailOcclusion_var.rgb,0.0).zy, node_9094_k.wz), float4(float4(_DetailOcclusion_var.rgb,0.0).yz, node_9094_k.xy), step(float4(_DetailOcclusion_var.rgb,0.0).z, float4(_DetailOcclusion_var.rgb,0.0).y));
                float4 node_9094_q = lerp(float4(node_9094_p.xyw, float4(_DetailOcclusion_var.rgb,0.0).x), float4(float4(_DetailOcclusion_var.rgb,0.0).x, node_9094_p.yzx), step(node_9094_p.x, float4(_DetailOcclusion_var.rgb,0.0).x));
                float node_9094_d = node_9094_q.x - min(node_9094_q.w, node_9094_q.y);
                float node_9094_e = 1.0e-10;
                float3 node_9094 = float3(abs(node_9094_q.z + (node_9094_q.w - node_9094_q.y) / (6.0 * node_9094_d + node_9094_e)), node_9094_d / (node_9094_q.x + node_9094_e), node_9094_q.x);;
                float node_5831 = lerp(saturate(((1.0 - _OcclusionStrenght)+(node_4702.b*_OcclusionStrenght))),saturate(((node_9094.b*_DetailOcclusionMult)+(1.0 - _DetailOcclusionMult))),node_8121);
                float3 specularAO = node_5831;
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = dot(lerp((_MetallicGlossMap_var.rgb*_Metallic),(_DetailMetalness_var.rgb*_DetailMetal),node_8121),float3(0.3,0.59,0.11));
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7427, _MainTex));
                float4 _DetailAlbedoMap_var = tex2D(_DetailAlbedoMap,TRANSFORM_TEX(node_9251, _DetailAlbedoMap));
                float3 diffuseColor = lerp((_Color.rgb*_MainTex_var.rgb),(_DetailColor.rgb*_DetailAlbedoMap_var.rgb),node_8121); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz)*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular) * specularAO;
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                indirectDiffuse *= node_5831; // Diffuse AO
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                float3 emissive = (_EmissionMap_var.rgb*_EmissionLM);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Metallic;
            uniform float _Glossiness;
            uniform float _NormalSmooth;
            uniform float _UVMasterTiling;
            uniform sampler2D _MetallicGlossMap; uniform float4 _MetallicGlossMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _EmissionLM;
            uniform sampler2D _DetailAlbedoMap; uniform float4 _DetailAlbedoMap_ST;
            uniform sampler2D _DetailNormalMap; uniform float4 _DetailNormalMap_ST;
            uniform float _DetailNormalMapScale;
            uniform sampler2D _DetailMask; uniform float4 _DetailMask_ST;
            uniform float _DetailMaskMult;
            uniform float _UVSecondaryTiling;
            uniform float _DetailGloss;
            uniform float _DetailMetal;
            uniform sampler2D _DetailMetalness; uniform float4 _DetailMetalness_ST;
            uniform float4 _DetailColor;
            uniform fixed _InvertMask;
            uniform float _MaskSmooth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 node_8143 = float3(0,0,1);
                float2 node_7427 = (i.uv0*_UVMasterTiling);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(node_7427, _BumpMap)));
                float2 node_9251 = (node_7427*_UVSecondaryTiling);
                float3 _DetailNormalMap_var = UnpackNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(node_9251, _DetailNormalMap)));
                float4 _DetailMask_var = tex2Dlod(_DetailMask,float4(TRANSFORM_TEX(node_7427, _DetailMask),0.0,_MaskSmooth));
                float4 node_8199_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_8199_p = lerp(float4(float4(_DetailMask_var.rgb,0.0).zy, node_8199_k.wz), float4(float4(_DetailMask_var.rgb,0.0).yz, node_8199_k.xy), step(float4(_DetailMask_var.rgb,0.0).z, float4(_DetailMask_var.rgb,0.0).y));
                float4 node_8199_q = lerp(float4(node_8199_p.xyw, float4(_DetailMask_var.rgb,0.0).x), float4(float4(_DetailMask_var.rgb,0.0).x, node_8199_p.yzx), step(node_8199_p.x, float4(_DetailMask_var.rgb,0.0).x));
                float node_8199_d = node_8199_q.x - min(node_8199_q.w, node_8199_q.y);
                float node_8199_e = 1.0e-10;
                float3 node_8199 = float3(abs(node_8199_q.z + (node_8199_q.w - node_8199_q.y) / (6.0 * node_8199_d + node_8199_e)), node_8199_d / (node_8199_q.x + node_8199_e), node_8199_q.x);;
                float node_8121 = saturate((lerp( node_8199.b, (1.0 - node_8199.b), _InvertMask )*_DetailMaskMult));
                float3 normalLocal = lerp(lerp(node_8143,_BumpMap_var.rgb,_NormalSmooth),lerp(_DetailNormalMap_var.rgb,node_8143,(1.0 - _DetailNormalMapScale)),node_8121);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _MetallicGlossMap_var = tex2D(_MetallicGlossMap,TRANSFORM_TEX(node_7427, _MetallicGlossMap));
                float4 _DetailMetalness_var = tex2D(_DetailMetalness,TRANSFORM_TEX(node_9251, _DetailMetalness));
                float gloss = lerp((_MetallicGlossMap_var.a*_Glossiness),(_DetailMetalness_var.a*_DetailGloss),node_8121);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = dot(lerp((_MetallicGlossMap_var.rgb*_Metallic),(_DetailMetalness_var.rgb*_DetailMetal),node_8121),float3(0.3,0.59,0.11));
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7427, _MainTex));
                float4 _DetailAlbedoMap_var = tex2D(_DetailAlbedoMap,TRANSFORM_TEX(node_9251, _DetailAlbedoMap));
                float3 diffuseColor = lerp((_Color.rgb*_MainTex_var.rgb),(_DetailColor.rgb*_DetailAlbedoMap_var.rgb),node_8121); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, GGXTerm(NdotH, 1.0-gloss));
                float specularPBL = (NdotL*visTerm*normTerm) * (UNITY_PI / 4);
                if (IsGammaSpace())
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                specularPBL = max(0, specularPBL * NdotL);
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 psp2 n3ds wiiu 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Metallic;
            uniform float _Glossiness;
            uniform float _UVMasterTiling;
            uniform sampler2D _MetallicGlossMap; uniform float4 _MetallicGlossMap_ST;
            uniform sampler2D _EmissionMap; uniform float4 _EmissionMap_ST;
            uniform float _EmissionLM;
            uniform sampler2D _DetailAlbedoMap; uniform float4 _DetailAlbedoMap_ST;
            uniform sampler2D _DetailMask; uniform float4 _DetailMask_ST;
            uniform float _DetailMaskMult;
            uniform float _UVSecondaryTiling;
            uniform float _DetailGloss;
            uniform float _DetailMetal;
            uniform sampler2D _DetailMetalness; uniform float4 _DetailMetalness_ST;
            uniform float4 _DetailColor;
            uniform fixed _InvertMask;
            uniform float _MaskSmooth;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _EmissionMap_var = tex2D(_EmissionMap,TRANSFORM_TEX(i.uv0, _EmissionMap));
                o.Emission = (_EmissionMap_var.rgb*_EmissionLM);
                
                float2 node_7427 = (i.uv0*_UVMasterTiling);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_7427, _MainTex));
                float2 node_9251 = (node_7427*_UVSecondaryTiling);
                float4 _DetailAlbedoMap_var = tex2D(_DetailAlbedoMap,TRANSFORM_TEX(node_9251, _DetailAlbedoMap));
                float4 _DetailMask_var = tex2Dlod(_DetailMask,float4(TRANSFORM_TEX(node_7427, _DetailMask),0.0,_MaskSmooth));
                float4 node_8199_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_8199_p = lerp(float4(float4(_DetailMask_var.rgb,0.0).zy, node_8199_k.wz), float4(float4(_DetailMask_var.rgb,0.0).yz, node_8199_k.xy), step(float4(_DetailMask_var.rgb,0.0).z, float4(_DetailMask_var.rgb,0.0).y));
                float4 node_8199_q = lerp(float4(node_8199_p.xyw, float4(_DetailMask_var.rgb,0.0).x), float4(float4(_DetailMask_var.rgb,0.0).x, node_8199_p.yzx), step(node_8199_p.x, float4(_DetailMask_var.rgb,0.0).x));
                float node_8199_d = node_8199_q.x - min(node_8199_q.w, node_8199_q.y);
                float node_8199_e = 1.0e-10;
                float3 node_8199 = float3(abs(node_8199_q.z + (node_8199_q.w - node_8199_q.y) / (6.0 * node_8199_d + node_8199_e)), node_8199_d / (node_8199_q.x + node_8199_e), node_8199_q.x);;
                float node_8121 = saturate((lerp( node_8199.b, (1.0 - node_8199.b), _InvertMask )*_DetailMaskMult));
                float3 diffColor = lerp((_Color.rgb*_MainTex_var.rgb),(_DetailColor.rgb*_DetailAlbedoMap_var.rgb),node_8121);
                float specularMonochrome;
                float3 specColor;
                float4 _MetallicGlossMap_var = tex2D(_MetallicGlossMap,TRANSFORM_TEX(node_7427, _MetallicGlossMap));
                float4 _DetailMetalness_var = tex2D(_DetailMetalness,TRANSFORM_TEX(node_9251, _DetailMetalness));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, dot(lerp((_MetallicGlossMap_var.rgb*_Metallic),(_DetailMetalness_var.rgb*_DetailMetal),node_8121),float3(0.3,0.59,0.11)), specColor, specularMonochrome );
                float roughness = 1.0 - lerp((_MetallicGlossMap_var.a*_Glossiness),(_DetailMetalness_var.a*_DetailGloss),node_8121);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "DLNK Shaders/Advanced/PBRAdvanced"
    CustomEditor "ShaderForgeMaterialInspector"
}
