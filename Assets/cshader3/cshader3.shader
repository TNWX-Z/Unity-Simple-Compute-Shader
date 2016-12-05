Shader "compute/cshader3"
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

				StructuredBuffer<float3> posbuffer;

				struct vertIN{
					uint vID : SV_VertexID;
					uint ins : SV_InstanceID;
				};

				struct vertOUT{
					float4 pos : SV_POSITION;
					//float3 nDir : NORMAL;
					float co : TEXCOORD0;
				};

				struct fragIN{
					bool face : SV_IsFrontFace;
				};

				vertOUT vert(vertIN i){
					vertOUT o = (vertOUT)1;
						float4 position = float4(buffer[i.vID],1);

							   position.xyz += posbuffer[i.ins]*50;
						o.pos = mul(UNITY_MATRIX_VP,position);
						o.co = float(i.ins);
					return o;
				}

				fixed4 frag(vertOUT vout,fragIN fin):SV_Target{
					fixed4 c = fixed4(1,1,0,0)*vout.co/4;

					return c;
				}
			ENDCG
		}
	}
}
