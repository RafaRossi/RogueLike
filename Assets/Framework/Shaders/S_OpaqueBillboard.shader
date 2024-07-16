// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: upgraded instancing buffer 'GPUInstanceOpaque' to new syntax.

// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Opaque Billboard"
{
	Properties
	{
	   _MainTex("Texture Image", 2D) = "white" {}
	   _Color("Tint", Color) = (1, 1, 1, 1)
	   [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	SubShader
	{
		Tags 
		{
			"Queue" = "Transparent" 
			"IgnoreProjector" = "True" 
			"RenderType" = "Transparent" 
			"DisableBatching" = "True" 
			"PreviewType" = "Plane" 
			"CanUseSpriteAtlas" = "True"
			"SortingLayer" = "Resources_Sprites" 
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM

			#pragma vertex vert  
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON

			#include "UnityCG.cginc"

			// User-specified uniforms            
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;

			float _EnableExternalAlpha;
			fixed4 _Color;
			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 tex : TEXCOORD0;
        		float4 color: COLOR;
			};
			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 tex : TEXCOORD0;

				float4 color: COLOR;
			};

			v2f vert(appdata input)
			{
				v2f output;

				float4 scale = float4(
                length(unity_ObjectToWorld._m00_m10_m20),
                length(unity_ObjectToWorld._m01_m11_m21),
                length(unity_ObjectToWorld._m02_m12_m22),
                1.0
                );
				
				output.pos = mul(UNITY_MATRIX_P, float4(UnityObjectToViewPos(float4(0.0, 0.0, 0.0, 1.0)), 1.0) + float4(input.vertex.xy, 0.0, 0.0) * scale);
				
				output.tex = input.tex;
				output.color = input.color * _Color;

				
				#ifdef PIXELSNAP_ON
				output.pos = UnityPixelSnap (output.pos);
				#endif

				return output;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif

				return color;
			}

			float4 frag(v2f input) : SV_Target
			{
				return SampleSpriteTexture(input.tex) * input.color;
			}

			ENDCG
		}
	}
}
