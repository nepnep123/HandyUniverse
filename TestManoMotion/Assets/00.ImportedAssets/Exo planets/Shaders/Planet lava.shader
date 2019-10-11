// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Planet lava"
{
	Properties
	{
		_Albedocolor("Albedo color", Color) = (1,1,1,0)
		[NoScaleOffset]_Albedo("Albedo", 2D) = "white" {}
		_Desaturation("Desaturation", Range( 0 , 1)) = 0
		_cloudscolor("clouds color", Color) = (1,1,1,0.547)
		[NoScaleOffset]_CloudsAlpha("Clouds (Alpha)", 2D) = "black" {}
		_Cloudspeed("Cloud speed", Float) = 1
		[NoScaleOffset]_Normalmap("Normal map", 2D) = "bump" {}
		_Normalintensity("Normal intensity", Range( 0 , 1)) = 0
		_Citiescolor("Cities color", Color) = (1,0.815662,0.559,0)
		[NoScaleOffset]_Emissivecities("Emissive (cities)", 2D) = "black" {}
		_Citiesoffset("Cities offset", Float) = 0
		[NoScaleOffset]_watermask("water mask", 2D) = "black" {}
		_Gloss("Gloss", Range( 0.01 , 1)) = 0.81
		[NoScaleOffset]_LookupSunset("Lookup Sunset", 2D) = "white" {}
		_Subatmospherecolor("Sub atmosphere color", Color) = (1,1,1,0)
		_Subatmosphereglobalintensity("Sub atmosphere global intensity", Range( 0 , 10)) = 1
		_Subatmospherepower("Sub atmosphere power", Range( 0.1 , 5)) = 5
		_AmbiantLightcontrol("Ambiant Light control", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite On
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 texcoord_1;
		};

		uniform float _Normalintensity;
		uniform sampler2D _Normalmap;
		uniform float4 _cloudscolor;
		uniform sampler2D _CloudsAlpha;
		uniform float _Cloudspeed;
		uniform sampler2D _LookupSunset;
		uniform sampler2D _Albedo;
		uniform float4 _Albedocolor;
		uniform float _Desaturation;
		uniform float _Subatmosphereglobalintensity;
		uniform float _Subatmospherepower;
		uniform float4 _Subatmospherecolor;
		uniform float4 _AmbiantLightcontrol;
		uniform sampler2D _Emissivecities;
		uniform float _Citiesoffset;
		uniform float4 _Citiescolor;
		uniform sampler2D _watermask;
		uniform float _Gloss;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
			float2 temp_cast_0 = (_Citiesoffset).xx;
			o.texcoord_1.xy = v.texcoord.xy * float2( 1,1 ) + temp_cast_0;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normalmap = i.uv_texcoord;
			float4 appendResult41 = (float4(( _Cloudspeed / 80.0 ) , 0.0 , 0.0 , 0.0));
			float4 tex2DNode26 = tex2D( _CloudsAlpha, ( float4( i.texcoord_0, 0.0 , 0.0 ) + ( appendResult41 * _Time.x ) ).xy );
			float temp_output_81_0 = ( 1.0 - ( _cloudscolor.a * tex2DNode26.a ) );
			float3 lerpResult195 = lerp( float3(0,0,1) , UnpackScaleNormal( tex2D( _Normalmap, uv_Normalmap ) ,_Normalintensity ) , temp_output_81_0);
			o.Normal = lerpResult195;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float dotResult94 = dot( ase_worldlightDir , ase_worldNormal );
			float2 temp_cast_2 = (dotResult94).xx;
			float2 uv_Albedo = i.uv_texcoord;
			float4 lerpResult66 = lerp( ( tex2D( _Albedo, uv_Albedo ) * _Albedocolor ) , _cloudscolor , ( _cloudscolor.a * tex2DNode26.a ));
			float3 desaturateVar142 = lerp( lerpResult66.rgb,dot(lerpResult66.rgb,float3(0.299,0.587,0.114)).xxx,_Desaturation);
			o.Albedo = ( tex2D( _LookupSunset, temp_cast_2 ) * float4( desaturateVar142 , 0.0 ) ).rgb;
			float dotResult247 = dot( ase_worldNormal , ase_worldlightDir );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNDotV9 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode9 = ( 0.0 + _Subatmosphereglobalintensity * pow( 1.0 - fresnelNDotV9, _Subatmospherepower ) );
			float smoothstepResult192 = smoothstep( 0.0 , 10.0 , fresnelNode9);
			float4 clampResult74 = clamp( ( ( dotResult247 * smoothstepResult192 ) * _Subatmospherecolor ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult72 = clamp( -( dotResult247 - 0.65 ) , 0.0 , 1.0 );
			float smoothstepResult281 = smoothstep( 0.3 , 0.7 , clampResult72);
			float smoothstepResult308 = smoothstep( 0.2 , 1.0 , ( 1.0 - tex2DNode26.a ));
			float2 uv_watermask = i.uv_texcoord;
			float4 tex2DNode82 = tex2D( _watermask, uv_watermask );
			float4 temp_output_311_0 = ( ( clampResult74 + ( ( ( 1.0 - _AmbiantLightcontrol ) * ( ( ( smoothstepResult281 * ( tex2D( _Emissivecities, i.texcoord_1 ) * _Citiescolor ) ) * smoothstepResult308 ) * ( 1.0 - tex2DNode82.a ) ) ) * 2.0 ) ) + tex2DNode82 );
			float4 lerpResult272 = lerp( temp_output_311_0 , ( float4( ( desaturateVar142 * ( 1.0 - ( -0.5 + dotResult247 ) ) ) , 0.0 ) + temp_output_311_0 ) , ( 0.5 * _AmbiantLightcontrol ));
			float4 clampResult190 = clamp( lerpResult272 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Emission = clampResult190.rgb;
			o.Smoothness = ( ( temp_output_81_0 * _Gloss ) * tex2DNode82 ).r;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows noambient nolightmap  nodynlightmap nodirlightmap nofog vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				float4 texcoords01 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord.xy = IN.texcoords01.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
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
Version=13501
105;33;2546;1364;5866.401;-379.3748;2.349561;True;True
Node;AmplifyShaderEditor.CommentaryNode;208;-8375.54,-552.9268;Float;False;1711.343;650.7912;Clouds;2;123;122;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;122;-8325.541,-501.5649;Float;False;828;523.9995;Cloud movement;9;32;28;160;161;42;41;33;34;40;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;160;-8259.012,-375.4778;Float;False;Constant;_modifier;modifier;18;0;80;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;28;-8258.543,-450.5649;Float;False;Property;_Cloudspeed;Cloud speed;5;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;280;-6809.369,386.8306;Float;False;565.4004;366.4034;Light mask;3;246;248;247;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;128;-6561.564,1690.601;Float;False;2612.78;842.7703;Cities in the dark;13;68;69;250;245;188;46;47;45;175;176;249;302;308;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;246;-6759.369,436.8305;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;42;-8256.48,-291.5657;Float;False;Constant;_Verticalspeed0;Vertical speed (0);5;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;161;-8077.009,-407.4778;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;248;-6749.97,613.5507;Float;False;1;0;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.CommentaryNode;249;-6488.455,1733.298;Float;False;1000.746;336.4277;Light mask controls;5;72;277;278;279;281;;1,1,1,1;0;0
Node;AmplifyShaderEditor.DotProductOpNode;247;-6478.969,500.2338;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.TimeNode;33;-8011.34,-175.9656;Float;False;0;5;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;279;-6437.201,1974.405;Float;False;Constant;_Float0;Float 0;18;0;0.65;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.DynamicAppendNode;41;-7939.539,-323.5648;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-7955.539,-451.5649;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-7774.739,-211.5656;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;176;-6489.581,2136.666;Float;False;Property;_Citiesoffset;Cities offset;10;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;278;-6272.619,1817.822;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-7636.339,-293.1657;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT4;0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.CommentaryNode;123;-7436.456,-502.9268;Float;False;689.5588;543.2294;Clouds color ;4;26;67;178;179;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;175;-6312.165,2093.73;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.NegateNode;277;-6083.915,1816.704;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;72;-5915.466,1815.191;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;45;-6072.594,2078.907;Float;True;Property;_Emissivecities;Emissive (cities);9;1;[NoScaleOffset];None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;26;-7362.819,-200.6095;Float;True;Property;_CloudsAlpha;Clouds (Alpha);4;1;[NoScaleOffset];None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;124;-5783.904,971.3612;Float;False;1831.8;637.6958;Sub atmosphere;10;12;11;9;19;74;13;15;22;299;300;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;47;-6005.939,2288.857;Float;False;Property;_Citiescolor;Cities color;8;0;1,0.815662,0.559,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;12;-5728.783,1423.379;Float;False;Property;_Subatmospherepower;Sub atmosphere power;16;0;5;0.1;5;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;266;-5260.866,262.5192;Float;False;1223.251;619.2262;Ambiant light control;6;170;171;162;284;285;199;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SmoothstepOpNode;281;-5724.323,1825.225;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.3;False;2;FLOAT;0.7;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-5735.54,2151.058;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.OneMinusNode;304;-5454.632,2325.449;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;188;-5425.837,2032.028;Float;False;286.3003;210.7;Cities only in the dark;1;48;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-5759.276,1335.105;Float;False;Property;_Subatmosphereglobalintensity;Sub atmosphere global intensity;15;0;1;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;126;-6565.623,2575.513;Float;False;1042.692;478.8921;Specular map / gloss;4;80;78;82;83;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;19;-5607.463,1190.73;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;299;-5153.922,1148.796;Float;False;430.7651;365.2241;Fresnel softening;3;194;193;192;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ColorNode;170;-5219.777,681.4563;Float;False;Property;_AmbiantLightcontrol;Ambiant Light control;17;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.FresnelNode;9;-5370.382,1211.055;Float;False;World;4;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;5.0;False;1;FLOAT
Node;AmplifyShaderEditor.SmoothstepOpNode;308;-5244.46,2324.096;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.2;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;193;-5103.922,1322.68;Float;False;Constant;_Float2;Float 2;18;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-5318.636,2091.129;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;82;-6515.623,2844.464;Float;True;Property;_watermask;water mask;11;1;[NoScaleOffset];None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;194;-5103.297,1399.02;Float;False;Constant;_Float3;Float 3;18;0;10;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;302;-5083.641,2039.141;Float;False;219;183;Clouds over cities;1;301;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;301;-5033.641,2091.141;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.OneMinusNode;199;-4942.687,797.1461;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SmoothstepOpNode;192;-4923.157,1198.796;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;-1.17;False;2;FLOAT;2.01;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;245;-4840.007,2058.029;Float;False;182.6621;174.583;No cities on water;1;173;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;119;-6651.16,-943.4936;Float;False;556.2625;499.6566;Albedo 1st pass;3;61;114;113;;1,1,1,1;0;0
Node;AmplifyShaderEditor.OneMinusNode;174;-5426.905,2595.51;Float;False;1;0;FLOAT;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;173;-4820.146,2094.012;Float;False;2;2;0;COLOR;0.0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.ColorNode;15;-4634.642,1378.396;Float;False;Property;_Subatmospherecolor;Sub atmosphere color;14;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-4661.776,1098.267;Float;True;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;250;-4636.874,2037.071;Float;False;344.9958;211.3491;Cities disapears with ambiant light;1;197;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WireNode;294;-4741.372,1639.761;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CommentaryNode;284;-5243.665,299.1527;Float;False;645.1594;352.7144;Ambiant Mask control;3;283;282;270;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;138;-5809.721,-440.0096;Float;False;725.9999;474.9703;Albedo 2nd pass;3;142;144;293;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;61;-6601.16,-893.4933;Float;True;Property;_Albedo;Albedo;1;1;[NoScaleOffset];None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;114;-6550.352,-666.9041;Float;False;Property;_Albedocolor;Albedo color;0;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;67;-7352.132,-413.5577;Float;False;Property;_cloudscolor;clouds color;3;0;1,1,1,0.547;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;69;-4543.488,2293.027;Float;False;Constant;_Citiesintensity;Cities intensity;11;0;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-4354.972,1264.366;Float;True;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;197;-4523.877,2093.371;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-6263.896,-779.2345;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;283;-5192.154,349.1526;Float;False;Constant;_Float1;Float 1;18;0;-0.5;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;178;-6958.685,-292.4877;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;293;-5779.244,-387.169;Float;False;315;303;Albedo + Clouds;1;66;;1,1,1,1;0;0
Node;AmplifyShaderEditor.ClampOpNode;74;-4124.333,1268.327;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR
Node;AmplifyShaderEditor.CommentaryNode;130;-3831.689,1545.235;Float;False;528;316.8262;Emissive 1st pass (Cities + Sub atmosphere );1;76;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-4200.973,2078.309;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;66;-5729.244,-337.169;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;282;-5025.218,353.5332;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;144;-5735.146,-49.39985;Float;False;Property;_Desaturation;Desaturation;2;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;285;-4499.926,313.7243;Float;False;285;303;Ambiant albedo only in darkness;1;269;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;76;-3672.016,1623.421;Float;True;2;2;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CommentaryNode;120;-5766.054,-1055.339;Float;False;893.1963;514.4534;Sunset ramp;4;91;94;93;95;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;162;-4986.211,661.4803;Float;False;Constant;_Ambiantcontrolmax;Ambiant control max;4;0;0.5;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;270;-4773.416,376.2227;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.DesaturateOpNode;142;-5361.891,-266.0792;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.CommentaryNode;129;-6983.429,2579.974;Float;False;352.8645;166.9128;Clouds occlusion (Gloss Normal Cities);1;81;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-4663.864,760.8574;Float;False;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleAddOpNode;311;-3191.157,1563.236;Float;False;2;2;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-6964.228,-87.97632;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;125;-4984.262,2912.576;Float;False;1023.621;720.0508;Normal map;4;195;85;196;90;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;286;-3272.987,1048.393;Float;False;266.5139;192.6634;Emissive 2nd pass + Ambiant;1;273;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;269;-4452.948,377.3245;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.WorldNormalVector;95;-5692.482,-775.7424;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;93;-5716.055,-1005.339;Float;True;1;0;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.WireNode;295;-2883.269,857.6476;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.OneMinusNode;81;-6887.692,2636.923;Float;False;1;0;FLOAT;0,0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;78;-6427.771,2653.909;Float;False;Property;_Gloss;Gloss;12;0;0.81;0.01;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;90;-4960.747,3081.415;Float;False;Property;_Normalintensity;Normal intensity;7;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;273;-3222.986,1098.393;Float;False;2;2;0;FLOAT3;0.0,0,0,0;False;1;COLOR;0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.DotProductOpNode;94;-5418.438,-866.6926;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;287;-2631.722,864.6442;Float;False;388.1348;289.1118;Control between Ambiant and no Ambiant;1;272;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;233;-2169.426,891.3169;Float;False;219;206;Emissive Total;1;190;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;91;-5218.342,-875.7353;Float;True;Property;_LookupSunset;Lookup Sunset;13;1;[NoScaleOffset];Assets/Exo planets/Textures/Lookup Sunset.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;80;-6035.359,2637.453;Float;True;2;2;0;FLOAT;0,0,0,0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;85;-4679.894,3007.598;Float;True;Property;_Normalmap;Normal map;6;1;[NoScaleOffset];None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;272;-2494.077,943.3556;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CommentaryNode;121;-4597.439,-450.2199;Float;False;284.9999;303;Albedo Total;1;99;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector3Node;196;-4593.695,3318.927;Float;True;Constant;_Vector0;Vector 0;18;0;0,0,1;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;190;-2119.426,941.317;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;310;-1828.871,1255.867;Float;False;Property;_Float6;Float 6;19;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;-5650.9,2761.177;Float;False;2;2;0;FLOAT;0,0,0,0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;306;-5440.408,2438.399;Float;False;Constant;_Float4;Float 4;18;0;2;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-4550.149,-381.1784;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.LerpOp;195;-4251.978,3118.373;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;309;-1873.871,1024.867;Float;False;Property;_Float5;Float 5;18;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-1564.801,891.7025;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Custom/Planet lava;False;False;False;False;True;False;True;True;True;True;False;False;False;False;False;False;False;Back;1;0;False;100000;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;0;4;10;25;False;0.5;True;0;DstColor;SrcColor;0;Zero;Zero;Add;Add;0;False;2.5;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;300;-4711.776,1048.267;Float;False;327.7568;301.5256;Fresnel (Sub Atmo) Only in daylight;0;;1,1,1,1;0;0
WireConnection;161;0;28;0
WireConnection;161;1;160;0
WireConnection;247;0;246;0
WireConnection;247;1;248;0
WireConnection;41;0;161;0
WireConnection;41;1;42;0
WireConnection;34;0;41;0
WireConnection;34;1;33;1
WireConnection;278;0;247;0
WireConnection;278;1;279;0
WireConnection;40;0;32;0
WireConnection;40;1;34;0
WireConnection;175;1;176;0
WireConnection;277;0;278;0
WireConnection;72;0;277;0
WireConnection;45;1;175;0
WireConnection;26;1;40;0
WireConnection;281;0;72;0
WireConnection;46;0;45;0
WireConnection;46;1;47;0
WireConnection;304;0;26;4
WireConnection;9;0;19;0
WireConnection;9;2;11;0
WireConnection;9;3;12;0
WireConnection;308;0;304;0
WireConnection;48;0;281;0
WireConnection;48;1;46;0
WireConnection;301;0;48;0
WireConnection;301;1;308;0
WireConnection;199;0;170;0
WireConnection;192;0;9;0
WireConnection;192;1;193;0
WireConnection;192;2;194;0
WireConnection;174;0;82;4
WireConnection;173;0;301;0
WireConnection;173;1;174;0
WireConnection;22;0;247;0
WireConnection;22;1;192;0
WireConnection;294;0;199;0
WireConnection;13;0;22;0
WireConnection;13;1;15;0
WireConnection;197;0;294;0
WireConnection;197;1;173;0
WireConnection;113;0;61;0
WireConnection;113;1;114;0
WireConnection;178;0;67;4
WireConnection;178;1;26;4
WireConnection;74;0;13;0
WireConnection;68;0;197;0
WireConnection;68;1;69;0
WireConnection;66;0;113;0
WireConnection;66;1;67;0
WireConnection;66;2;178;0
WireConnection;282;0;283;0
WireConnection;282;1;247;0
WireConnection;76;0;74;0
WireConnection;76;1;68;0
WireConnection;270;0;282;0
WireConnection;142;0;66;0
WireConnection;142;1;144;0
WireConnection;171;0;162;0
WireConnection;171;1;170;0
WireConnection;311;0;76;0
WireConnection;311;1;82;0
WireConnection;179;0;67;4
WireConnection;179;1;26;4
WireConnection;269;0;142;0
WireConnection;269;1;270;0
WireConnection;295;0;171;0
WireConnection;81;0;179;0
WireConnection;273;0;269;0
WireConnection;273;1;311;0
WireConnection;94;0;93;0
WireConnection;94;1;95;0
WireConnection;91;1;94;0
WireConnection;80;0;81;0
WireConnection;80;1;78;0
WireConnection;85;5;90;0
WireConnection;272;0;311;0
WireConnection;272;1;273;0
WireConnection;272;2;295;0
WireConnection;190;0;272;0
WireConnection;83;0;80;0
WireConnection;83;1;82;0
WireConnection;99;0;91;0
WireConnection;99;1;142;0
WireConnection;195;0;196;0
WireConnection;195;1;85;0
WireConnection;195;2;81;0
WireConnection;0;0;99;0
WireConnection;0;1;195;0
WireConnection;0;2;190;0
WireConnection;0;4;83;0
ASEEND*/
//CHKSM=6FCC87F9B2DC4E516DA432E98FF4C26D9D894F0B