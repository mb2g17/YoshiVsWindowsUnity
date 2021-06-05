Shader "Custom/RGB Cube"
{
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert // vert function is the vertex shader 
			#pragma fragment frag // frag function is the fragment shader

			// for multiple vertex output parameters an output structure 
			// is defined:
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 col : TEXCOORD0;
			};

			vertexOutput vert(float4 vertexPos : POSITION) // vertex shader 
			{
				vertexOutput output; // we don't need to type 'struct' here
				output.pos = UnityObjectToClipPos(vertexPos);
				output.col = vertexPos + float4(0.5, 0.5, 0.5, 0.0);

				output.col = float4(output.col[0], output.col[1], output.col[2], 0.0);
				//output.col = float4(1.0, 1.0, output.col[2], 0.0);

				// Gets height
				float height = output.col[1];

				if (height >= 0 && height < 0.1)
					output.col = float4(1, 0, 0, 0);
				else if (height >= 0.1 && height < 0.2)
					output.col = float4(1, 0, 1, 0);
				else if (height >= 0.2 && height < 0.3)
					output.col = float4(0.5, 0, 1, 0);
				else if (height >= 0.3 && height < 0.4)
					output.col = float4(0.2, 0.3, 1, 0);
				else if (height >= 0.4 && height < 0.5)
					output.col = float4(0, 1, 1, 0);
				else if (height >= 0.5 && height < 0.6)
					output.col = float4(0, 1, 0, 0);
				else if (height >= 0.6 && height < 0.7)
					output.col = float4(0, 1, 1, 0);
				else if (height >= 0.7 && height < 0.8)
					output.col = float4(0.2, 0.3, 1, 0);
				else if (height >= 0.8 && height < 0.9)
					output.col = float4(0.5, 0, 0.1, 0);
				else if (height >= 0.9 && height < 1)
					output.col = float4(1, 0, 1, 0);
				else
					output.col = float4(1, 1, 1, 0);

				/*
				float t = _Time[1];
				t = sin(t * 2);
				t += 1;
				t /= 2.0f;

				output.col = float4(t, output.col[1], 1 - t, 0.0);
				*/

				//output.col = float4(output.col[0], 1.0, 1.0, 0.0);

				// Here the vertex shader writes output data
				// to the output structure. We add 0.5 to the 
				// x, y, and z coordinates, because the 
				// coordinates of the cube are between -0.5 and
				// 0.5 but we need them between 0.0 and 1.0. 
				return output;
			}

			float4 frag(vertexOutput input) : COLOR // fragment shader
			{
				return input.col;
				// Here the fragment shader returns the "col" input 
				// parameter with semantic TEXCOORD0 as nameless
				// output parameter with semantic COLOR.
			}

			ENDCG
		}
	}
}