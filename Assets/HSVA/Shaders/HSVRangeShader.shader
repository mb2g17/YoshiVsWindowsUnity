// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
MIT License

Copyright 2015, Gregg Tavares.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

    * Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above
copyright notice, this list of conditions and the following disclaimer
in the documentation and/or other materials provided with the
distribution.
    * Neither the name of Gregg Tavares. nor the names of its
contributors may be used to endorse or promote products derived from
this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
Shader "Custom/HSVRangeShader"
{
    Properties
    {
       _MainTex ("Sprite Texture", 2D) = "white" {}
       _Color ("Alpha Color Key", Color) = (0,0,0,1)

       _HSVRangeMin ("HSV Affect Range Min Body", Range(0, 1)) = 0
       _HSVRangeMax ("HSV Affect Range Max Body", Range(0, 1)) = 1
       _HSVAAdjust ("HSVA Adjust Body", Vector) = (0, 0, 0, 0)

	   _HSVRangeMinA("HSV Affect Range Min Accessories", Range(0, 1)) = 0
	   _HSVRangeMaxA("HSV Affect Range Max Accessories", Range(0, 1)) = 1
	   _HSVAAdjustA("HSVA Adjust Accessories", Vector) = (0, 0, 0, 0)

       _StencilComp ("Stencil Comparison", Float) = 8
       _Stencil ("Stencil ID", Float) = 0
       _StencilOp ("Stencil Operation", Float) = 0
       _StencilWriteMask ("Stencil Write Mask", Float) = 255
       _StencilReadMask ("Stencil Read Mask", Float) = 255
       _ColorMask ("Color Mask", Float) = 15
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        ColorMask [_ColorMask]

        Pass
        {
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile DUMMY PIXELSNAP_ON

			#include "UnityCG.cginc"

            sampler2D _MainTex;
			float4  _Color;

            float _HSVRangeMin;
            float _HSVRangeMax;
            float4 _HSVAAdjust;

			float _HSVRangeMinA;
			float _HSVRangeMaxA;
			float4 _HSVAAdjustA;

			float _Flip;

            struct Vertex
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };

            struct Fragment
            {
                float4 vertex : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};


			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

            /* -- HSV vert --
			Fragment vert(Vertex v)
            {
                Fragment o;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.uv_MainTex;

                return o;
            }*/

			// -- Unity vert --
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

				return OUT;
			}

            float3 rgb2hsv(float3 c) {
              float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
              float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
              float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

              float d = q.x - min(q.w, q.y);
              float e = 1.0e-10;
              return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }

            float3 hsv2rgb(float3 c) {
              c = float3(c.x, clamp(c.yz, 0.0, 1.0));
              float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
              float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
              return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

			fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 color = tex2D(_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D(_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 unity_frag(v2f IN) : COLOR
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
				c.rgb *= c.a;
				return c;
			}

			float4 hsv_frag(v2f IN) : COLOR
			{
				float4 color = tex2D(_MainTex, IN.texcoord);
				float3 hsv = rgb2hsv(color.rgb);

				float affectMult = step(_HSVRangeMin, hsv.r) * step(hsv.r, _HSVRangeMax);

				// If we're on the body
				if (affectMult != 0)
				{
					float3 rgb = hsv2rgb(hsv + _HSVAAdjust.xyz * affectMult);
					return float4(rgb, color.a + _HSVAAdjust.a);
				}
				else
				{
					affectMult = step(_HSVRangeMinA, hsv.r) * step(hsv.r, _HSVRangeMaxA);
					affectMult += 1;
					float3 rgb = hsv2rgb(hsv + _HSVAAdjustA.xyz * affectMult);
					return float4(rgb, color.a + _HSVAAdjustA.a);
				}
			}

			float4 frag(v2f IN) : SV_Target
			{
				// Uses the min of the two alphas: the HSVA alpha and the unity tint alpha
				float4 rv = hsv_frag(IN);
				fixed4 c = unity_frag(IN);
				return float4(
					rv.r,
					rv.g,
					rv.b,
					min(c.a, rv.a)
				);
			}

            ENDCG
        }
    }
}
