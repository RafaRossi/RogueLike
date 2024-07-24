// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: upgraded instancing buffer 'GPUInstanceOpaque' to new syntax.

// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Opaque Billboard Shadows"
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
			"Queue" = "Geometry" 
			"IgnoreProjector" = "True" 
			"RenderType" = "TransparentCutout" 
			"DisableBatching" = "True" 
			"PreviewType" = "Plane" 
			"CanUseSpriteAtlas" = "True"
			"SortingLayer" = "Resources_Sprites" 
		}
		LOD 200
		Cull Off
		Lighting Off
		//ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		/*Pass {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }

            ZWrite On ZTest LEqual

            CGPROGRAM
            #pragma target 3.0

            // -------------------------------------


            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing
            // Uncomment the following line to enable dithering LOD crossfade. Note: there are more in the file to uncomment for other passes.
            //#pragma multi_compile _ LOD_FADE_CROSSFADE

            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster

            #include "UnityStandardShadow.cginc"

            ENDCG
        }*/

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
				fixed4 baseColor = SampleSpriteTexture(input.tex) * input.color;
				baseColor.a = input.color.a * baseColor.a;
				return baseColor;
			}

			ENDCG
		}
	}
}
