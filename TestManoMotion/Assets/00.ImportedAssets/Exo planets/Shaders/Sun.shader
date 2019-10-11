// Upgrade NOTE: replaced 'UNITY_INSTANCE_ID' with 'UNITY_VERTEX_INPUT_INSTANCE_ID'

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Sun"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Sunlabedo2("Sun labedo2", 2D) = "white" {}
		_Sun_Albedo("Sun_Albedo", 2D) = "white" {}
		_flowmap("flowmap", 2D) = "white" {}
		_Flowpower("Flow power", Range( 0 , 1)) = 0.5
		_Flowspeed("Flow speed", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
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
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 texcoord_0;
			float2 uv_texcoord;
		};

		uniform sampler2D _Sun_Albedo;
		uniform float _Flowpower;
		uniform sampler2D _flowmap;
		uniform float4 _flowmap_ST;
		uniform float _Flowspeed;
		uniform sampler2D _Sunlabedo2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float3 worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelFinalVal3 = (0.0 + 0.8*pow( 1.0 - dot( worldNormal, worldViewDir ) , 0.94));
			float2 temp_output_47_0 = ( i.texcoord_0 * 1.0 );
			float2 uv_flowmap = i.uv_texcoord * _flowmap_ST.xy + _flowmap_ST.zw;
			float2 componentMask15 = tex2D( _flowmap,uv_flowmap).xy;
			float2 temp_cast_1 = 0.0;
			float2 temp_cast_2 = 1.0;
			float2 temp_cast_3 = -0.5;
			float2 temp_cast_4 = 0.5;
			float2 temp_output_25_0 = ( ( _Flowpower * -1.0 ) * (temp_cast_3 + (componentMask15 - temp_cast_1) * (temp_cast_4 - temp_cast_3) / (temp_cast_2 - temp_cast_1)) );
			float temp_output_11_0 = ( _Flowspeed * _Time.y );
			float temp_output_30_0 = frac( temp_output_11_0 );
			float2 temp_cast_5 = 0.0;
			float2 temp_cast_6 = 1.0;
			float2 temp_cast_7 = -0.5;
			float2 temp_cast_8 = 0.5;
			float3 desaturateVar49 = lerp( ( ( fresnelFinalVal3 * float4(1,1,1,0) ) + lerp( tex2D( _Sun_Albedo,( temp_output_47_0 + ( temp_output_25_0 * temp_output_30_0 ) )) , tex2D( _Sunlabedo2,( temp_output_47_0 + ( temp_output_25_0 * frac( ( temp_output_11_0 + 0.5 ) ) ) )) , abs( ( ( temp_output_30_0 - 0.5 ) / 0.5 ) ) ) ).xyz,dot(( ( fresnelFinalVal3 * float4(1,1,1,0) ) + lerp( tex2D( _Sun_Albedo,( temp_output_47_0 + ( temp_output_25_0 * temp_output_30_0 ) )) , tex2D( _Sunlabedo2,( temp_output_47_0 + ( temp_output_25_0 * frac( ( temp_output_11_0 + 0.5 ) ) ) )) , abs( ( ( temp_output_30_0 - 0.5 ) / 0.5 ) ) ) ).xyz,float3(0.299,0.587,0.114)),0.3);
			o.Emission = desaturateVar49;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha vertex:vertexDataFunc 

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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
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
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.texcoords01 = float4( v.texcoord.xy, v.texcoord1.xy );
				o.worldPos = worldPos;
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
				surfIN.uv_texcoord = IN.texcoords01.xy;
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
Version=6001
2567;29;1666;974;300.1923;355.3988;1;True;True
Node;AmplifyShaderEditor.TimeNode;7;-2034.801,397.3;Float;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;26;-2071.299,296.6003;Float;False;Property;_Flowspeed;Flow speed;5;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;14;-2489.201,-365.2001;Float;True;Property;_flowmap;flowmap;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;24;-1997.701,-493.1996;Float;False;Constant;_Float7;Float 7;2;0;-1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;19;-2060.101,-83.59981;Float;False;Constant;_Float3;Float 3;2;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;31;-1790.297,617.1005;Float;False;Constant;_Float0;Float 0;4;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;20;-2066.501,-3.599826;Float;False;Constant;_Float4;Float 4;2;0;-0.5;0;0;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;15;-2168.899,-365.2;Float;True;True;True;False;False;0;FLOAT4;0,0,0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1784.8,356.3;Float;False;0;FLOAT;0.0;False;1;FLOAT;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;18;-2060.1,-177.9998;Float;False;Constant;_Float2;Float 2;2;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;23;-2119.501,-611.4998;Float;False;Property;_Flowpower;Flow power;4;0;0.5;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;21;-2062.499,82.40018;Float;False;Constant;_Float5;Float 5;2;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;17;-1872.601,-339.3999;Float;True;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,0;False;3;FLOAT2;0,0;False;4;FLOAT2;1,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-1564.197,508.8006;Float;True;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1818.502,-566.7994;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;48;-1243.993,-420.3989;Float;False;Constant;_Float6;Float 6;6;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.FractNode;33;-1277.096,327.3008;Float;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;40;-1420.495,734.5005;Float;False;Constant;_Float1;Float 1;6;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1263.499,-566.6998;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1534.401,-407.7996;Float;False;0;FLOAT;0,0;False;1;FLOAT2;0.0;False;FLOAT2
Node;AmplifyShaderEditor.FractNode;30;-1540.299,326.6006;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1062.298,-67.19958;Float;False;0;FLOAT2;0.0;False;1;FLOAT;0.0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-989.1934,-511.3989;Float;False;0;FLOAT2;0.0;False;1;FLOAT;0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1022.596,131.5007;Float;False;0;FLOAT2;0.0;False;1;FLOAT;0.0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;39;-1212.495,592.1007;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-764.4982,-112.5996;Float;True;0;FLOAT2;0.0;False;1;FLOAT2;0.0,0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleDivideOpNode;41;-1009.895,593.8005;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-767.0964,103.6006;Float;True;0;FLOAT2;0.0;False;1;FLOAT2;0,0;False;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;2;-527.0001,-112;Float;True;Property;_Sun_Albedo;Sun_Albedo;2;0;Assets/Exo planets/Textures/Sun/Sun_Albedo.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.AbsOpNode;42;-815.4952,594.2999;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;37;-526.2957,81.40097;Float;True;Property;_Sunlabedo2;Sun labedo2;0;0;Assets/Exo planets/Textures/Sun/Sun_Albedo.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;38;-118.2953,139.0008;Float;True;0;FLOAT4;0.0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.ColorNode;6;-69.20034,-111.7;Float;False;Constant;_Color0;Color 0;1;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.FresnelNode;3;-77.20033,-256.7001;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;2;FLOAT;0.8;False;3;FLOAT;0.94;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;192.8,-224.7001;Float;False;0;FLOAT;0.0,0,0,0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.WireNode;46;357.8063,111.0013;Float;False;0;FLOAT4;0.0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleAddOpNode;4;424.2,-25.1001;Float;False;0;COLOR;0.0,0,0,0;False;1;FLOAT4;0.0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;50;446.8077,76.6012;Float;False;Constant;_Float8;Float 8;6;0;0.3;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;36;1767.305,1054;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Assets/Exo planets/Textures/Sun/Sun_Albedo.tga;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.DesaturateOpNode;49;574.8077,-29.3988;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT;0.0;False;FLOAT3
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;809.6,-62.20004;Float;False;True;2;Float;ASEMaterialInspector;Standard;Custom/Sun;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;15;0;14;0
WireConnection;11;0;26;0
WireConnection;11;1;7;2
WireConnection;17;0;15;0
WireConnection;17;1;18;0
WireConnection;17;2;19;0
WireConnection;17;3;20;0
WireConnection;17;4;21;0
WireConnection;32;0;11;0
WireConnection;32;1;31;0
WireConnection;22;0;23;0
WireConnection;22;1;24;0
WireConnection;33;0;32;0
WireConnection;25;0;22;0
WireConnection;25;1;17;0
WireConnection;30;0;11;0
WireConnection;27;0;25;0
WireConnection;27;1;30;0
WireConnection;47;0;29;0
WireConnection;47;1;48;0
WireConnection;34;0;25;0
WireConnection;34;1;33;0
WireConnection;39;0;30;0
WireConnection;39;1;40;0
WireConnection;28;0;47;0
WireConnection;28;1;27;0
WireConnection;41;0;39;0
WireConnection;41;1;40;0
WireConnection;35;0;47;0
WireConnection;35;1;34;0
WireConnection;2;1;28;0
WireConnection;42;0;41;0
WireConnection;37;1;35;0
WireConnection;38;0;2;0
WireConnection;38;1;37;0
WireConnection;38;2;42;0
WireConnection;5;0;3;0
WireConnection;5;1;6;0
WireConnection;46;0;38;0
WireConnection;4;0;5;0
WireConnection;4;1;46;0
WireConnection;49;0;4;0
WireConnection;49;1;50;0
WireConnection;0;2;49;0
ASEEND*/
//CHKSM=00CB6C6FA68A4783E0F730DC176ECFC11346325A