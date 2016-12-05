Shader "Unlit/shader2"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass{
			CGPROGRAM
				#pragma target 5.0
				#pragma vertex vert 
				#pragma fragment frag 
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				//StructuredBuffer<float4> imgbuffer;

				struct vertIN{
					uint vid : SV_VertexID;
					uint ins : SV_InstanceID;
					float4 vertex : POSITION;
					float2 coord : TEXCOORD0;
				};

				struct vertOUT{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				vertOUT vert(vertIN i){
					vertOUT o;
						o.pos = mul(UNITY_MATRIX_MVP,i.vertex);
						o.uv = i.coord;
					return o;
				}

				fixed4 frag(vertOUT ou):COLOR{
					fixed4 c = tex2D(_MainTex,ou.uv);
					return c;
				}	
			ENDCG
		}
	}
}
