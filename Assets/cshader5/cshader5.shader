Shader "Unlit/cshader5"
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

				StructuredBuffer<float3> databuffer;
				StructuredBuffer<float3> posbuffer;

				struct vertIN{
					uint vID : SV_VertexID;
					uint ins : SV_InstanceID;
				};

				struct vertOUT{
					float4 pos : SV_POSITION;
				};

				vertOUT vert(vertIN i){
					vertOUT o = (vertOUT)0;
						float4 vertex = float4(databuffer[i.vID],1);
							   vertex.xyz += posbuffer[i.ins]*10;
						o.pos = mul(UNITY_MATRIX_VP,vertex);
					return o;
				}

				fixed4 frag(vertOUT ou):COLOR{

					return 1;
				}

			ENDCG
		}
	}
}
