// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Red" {
	Properties {
		Speed("Flashing speed", Float) = 2
	}

	SubShader {

		Pass {

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			float Speed;

			struct vertInput {
				float4 pos : POSITION;
			};

			struct vertOutput {
				float4 pos : SV_POSITION;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.pos);
				return o;
			}

			half4 frag(vertOutput output) : COLOR{
				float t = _Time[1];
				t = sin(t * Speed);
				t += 1;
				t /= 2.0f;
				return half4(t, 0.0, 0.0, 1.0);
			}
			ENDCG
		}
	}
}