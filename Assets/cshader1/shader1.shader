Shader "Unlit/shader"
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

				StructuredBuffer<float3> buffer;

				struct vertIN{
					uint vID : SV_VertexID;
					//uint pID : SV_PrimitiveID;
					uint insID : SV_InstanceID;
				};
				struct vertOUT{
					float4 pos : SV_POSITION;
					//float2 uv : TEXCOORD0;
					//float3 nDir : TEXCOORD1;
				};

				struct fragIN{
					bool vface : SV_IsFrontFace;
				};

				vertOUT vert(vertIN i){
					vertOUT o;
						float4 position = float4(buffer[i.vID],1);
						o.pos = mul(UNITY_MATRIX_VP,position);
					return o;
				}

				fixed4 frag(vertOUT vin,fragIN fin):COLOR{
					fixed4 c;// = tex2D(_MainTex,vin.uv);
					if(fin.vface){
						c = fixed4(0,1,1,1);
					}

					return c;
				}
			ENDCG
		}
	}
}
