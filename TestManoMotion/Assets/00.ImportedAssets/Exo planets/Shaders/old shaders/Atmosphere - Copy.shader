// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:1,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:2,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1,x:32701,y:32821,varname:node_1,prsc:2|emission-1494-OUT,custl-1494-OUT,voffset-8312-OUT;n:type:ShaderForge.SFN_ViewVector,id:1486,x:31147,y:32972,varname:node_1486,prsc:2;n:type:ShaderForge.SFN_Color,id:1488,x:31749,y:32534,ptovrint:False,ptlb:Atmosphere Color,ptin:_AtmosphereColor,varname:node_2430,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.006704159,c2:0.9117647,c3:0.9117647,c4:1;n:type:ShaderForge.SFN_NormalVector,id:1489,x:31147,y:32822,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:1490,x:31429,y:32903,varname:node_1490,prsc:2,dt:0|A-1489-OUT,B-1486-OUT;n:type:ShaderForge.SFN_Clamp01,id:1491,x:31670,y:32903,varname:node_1491,prsc:2|IN-1490-OUT;n:type:ShaderForge.SFN_Power,id:1492,x:31884,y:32890,varname:node_1492,prsc:2|VAL-1491-OUT,EXP-1497-OUT;n:type:ShaderForge.SFN_Multiply,id:1494,x:32144,y:32672,varname:node_1494,prsc:2|A-1488-RGB,B-5402-OUT,C-1492-OUT;n:type:ShaderForge.SFN_Vector1,id:1497,x:31643,y:33074,varname:node_1497,prsc:2,v1:3;n:type:ShaderForge.SFN_Vector1,id:5402,x:31560,y:32670,varname:node_5402,prsc:2,v1:10;n:type:ShaderForge.SFN_NormalVector,id:7440,x:32308,y:33445,prsc:2,pt:False;n:type:ShaderForge.SFN_RemapRange,id:3865,x:32308,y:33282,varname:node_3865,prsc:2,frmn:0,frmx:1,tomn:0,tomx:1|IN-1089-OUT;n:type:ShaderForge.SFN_Multiply,id:8312,x:32507,y:33373,varname:node_8312,prsc:2|A-3865-OUT,B-7440-OUT;n:type:ShaderForge.SFN_Slider,id:1089,x:31906,y:33129,ptovrint:False,ptlb:Atmosphere size,ptin:_Atmospheresize,varname:node_1089,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:5;proporder:1488-1089;pass:END;sub:END;*/

Shader "Exo-Planets/AtmosphereSun" {
    Properties {
        _AtmosphereColor ("Atmosphere Color", Color) = (0.006704159,0.9117647,0.9117647,1)
        _Atmospheresize ("Atmosphere size", Range(-1, 5)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+2"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Front
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float4 _AtmosphereColor;
            uniform float _Atmospheresize;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
                v.vertex.xyz += ((_Atmospheresize*1.0+0.0)*v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float3 node_1494 = (_AtmosphereColor.rgb*10.0*pow(saturate(dot(i.normalDir,viewDirection)),3.0));
                float3 emissive = node_1494;
                float3 finalColor = emissive + node_1494;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers xbox360 ps3 
            #pragma target 3.0
            uniform float _Atmospheresize;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
                v.vertex.xyz += ((_Atmospheresize*1.0+0.0)*v.normal);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
