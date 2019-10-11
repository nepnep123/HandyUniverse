// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Atmosphere ASE"
{
	Properties
	{
		_Atmospheresize("Atmosphere size", Range( 0 , 10)) = 0
		_Atmospherecolor("Atmosphere color", Color) = (1,1,1,0)
		_Atmospherecircle("Atmosphere circle", Range( 0 , 1)) = 0
		_AtmosphereSharpness("Atmosphere Sharpness", Range( 0 , 1)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Overlay"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Front
		ZWrite On
		Blend One One
		BlendOp Add
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Lambert keepalpha noshadow noambient nofog vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float3 viewDir;
			INTERNAL_DATA
		};

		uniform float4 _Atmospherecolor;
		uniform float _AtmosphereSharpness;
		uniform float _Atmospherecircle;
		uniform float _Atmospheresize;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertexNormal = v.normal.xyz;
			v.vertex.xyz += ( ase_vertexNormal * _Atmospheresize );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			float dotResult16 = dot( ase_worldlightDir , i.worldNormal );
			float dotResult26 = dot( i.worldNormal , i.viewDir );
			float temp_output_32_0 = pow( (0.0 + (-dotResult26 - 0.0) * (( 15.0 * _AtmosphereSharpness ) - 0.0) / (1.0 - 0.0)) , ( 15.0 * _AtmosphereSharpness ) );
			float lerpResult41 = lerp( ( dotResult16 * temp_output_32_0 ) , temp_output_32_0 , _Atmospherecircle);
			float clampResult43 = clamp( lerpResult41 , 0.0 , 1.0 );
			o.Emission = ( _Atmospherecolor * clampResult43 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13501
2567;29;1666;968;1125.773;408.7021;1.330569;True;True
Node;AmplifyShaderEditor.CommentaryNode;46;-967.4506,101.3996;Float;False;1475.601;411.5002;Hand made fresnel ;11;25;24;29;28;26;27;32;33;30;51;52;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;24;-917.4506,151.3996;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;25;-891.1507,289.0996;Float;False;World;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;53;-553.6279,601.1993;Float;False;Property;_AtmosphereSharpness;Atmosphere Sharpness;4;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;26;-669.5507,153.0999;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;30;-575.6974,404.5526;Float;False;Constant;_Atmosphereintensity;Atmosphere intensity;3;0;15;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;29;-456.3501,316.8998;Float;False;Constant;_Float2;Float 2;1;0;1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;28;-459.3501,242.8998;Float;False;Constant;_Float1;Float 1;1;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.NegateNode;51;-442.4108,140.8691;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;33;18.95053,251.2991;Float;False;Constant;_Atmospherepower;Atmosphere power;4;0;15;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-296.8285,445.5231;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;47;-49.14934,-401.8528;Float;False;541.457;360.2413;light mask on direction;3;16;40;14;;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldNormalVector;14;18.46293,-222.7618;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;40;0.8506591,-351.8528;Float;False;1;0;FLOAT;0.0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;224.7545,361.6973;Float;False;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.TFHCRemap;27;-244.6504,175.3999;Float;True;5;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;3;FLOAT;0.0;False;4;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;48;622.9507,-180.2;Float;False;562.6014;474.4;Control over either fresnel or directional light;3;42;41;37;;1,1,1,1;0;0
Node;AmplifyShaderEditor.PowerNode;32;328.1508,184.2999;Float;False;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;16;257.3077,-294.6115;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;42;672.9507,179.1999;Float;False;Property;_Atmospherecircle;Atmosphere circle;3;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;699.3507,-130.2;Float;True;2;2;0;FLOAT;0.0;False;1;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;49;1253.751,-203.5001;Float;False;450.9996;382.0001;Color control;3;43;34;35;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;45;1028.8,393.4002;Float;False;627.8995;345.7003;Vertex offset;3;9;8;7;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;41;1001.552,5.299968;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;35;1303.751,-153.5;Float;False;Property;_Atmospherecolor;Atmosphere color;2;0;1,1,1,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.NormalVertexDataNode;8;1156.401,443.4003;Float;False;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;1078.8,602.9006;Float;False;Property;_Atmospheresize;Atmosphere size;1;0;0;0;10;0;1;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;43;1319.151,22.49998;Float;False;3;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;1421.7,486.1007;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.0,0,0;False;1;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;1535.75,-17.80019;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2059,87.49983;Float;False;True;2;Float;ASEMaterialInspector;0;0;Lambert;Custom/Atmosphere ASE;False;False;False;False;True;False;False;False;False;True;False;False;False;False;False;False;False;Front;1;0;False;100000;100000;Custom;0;True;False;0;True;Overlay;Overlay;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;100;255;255;2;1;1;1;0;0;0;0;False;0;4;10;25;False;0.5;False;4;One;One;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;0;0;False;14;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;26;0;24;0
WireConnection;26;1;25;0
WireConnection;51;0;26;0
WireConnection;54;0;30;0
WireConnection;54;1;53;0
WireConnection;52;0;33;0
WireConnection;52;1;53;0
WireConnection;27;0;51;0
WireConnection;27;1;28;0
WireConnection;27;2;29;0
WireConnection;27;3;28;0
WireConnection;27;4;54;0
WireConnection;32;0;27;0
WireConnection;32;1;52;0
WireConnection;16;0;40;0
WireConnection;16;1;14;0
WireConnection;37;0;16;0
WireConnection;37;1;32;0
WireConnection;41;0;37;0
WireConnection;41;1;32;0
WireConnection;41;2;42;0
WireConnection;43;0;41;0
WireConnection;9;0;8;0
WireConnection;9;1;7;0
WireConnection;34;0;35;0
WireConnection;34;1;43;0
WireConnection;0;2;34;0
WireConnection;0;11;9;0
ASEEND*/
//CHKSM=5914FDFC03498428713D1F9A3A051F4FB5D42E39