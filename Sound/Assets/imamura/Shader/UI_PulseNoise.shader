Shader "UI/PulseNoise"
{
	
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_SubTex("SubTexture", 2D) = "white" {}	
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
			_Ratio("ratio", Range(0, 1)) = 0
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				Name "Default"
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
				#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 uv : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 uv  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				sampler2D _SubTex;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				float4 _MainTex_ST;
				float _Ratio;

				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
					OUT.worldPosition = v.vertex;
					OUT.vertex = UnityObjectToClipPos(v.vertex);

					OUT.uv = TRANSFORM_TEX(v.uv, _MainTex);
					//OUT.u = TRANSFORM_TEX(v.texcoord, _MainTex);
					return OUT;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 uv = i.uv;
					/*float x = 2 * uv.x;
					float y = 2 * uv.y;
					uv.y += _Amount * sin(10 * x)*(-(x - 1)*(x - 1) + 1);
					float amp = 0.05*sin(_Time * _UTime + uv.x * _XA);
					uv.xy = float2(uv.x, uv.y + amp);
					half4 color = (tex2D(_MainTex, uv)) * i.color;*/
					//color.xyz = half3(IN.texcoord.x,IN.texcoord.y,0.);
					float t1 = _Ratio + sin(_Time.w + i.uv.x * 3) / 20;
					float t2 = _Ratio + sin(_Time.w + i.uv.x * 3) / 20;//_Ratio + 0.05 + cos(_Time.z + i.uv.x) / 40;

					fixed4 gaugeColor = lerp(tex2D(_SubTex, i.uv), tex2D(_MainTex, i.uv), saturate(i.uv.y < t2));
					half4 color = lerp(gaugeColor, tex2D(_MainTex, i.uv), saturate(i.uv.y < t1));
					//color.xyz = lerp(gaugeColor, tex2D(_MainTex, i.uv), saturate(i.uv.y < t1));

					#ifdef UNITY_UI_CLIP_RECT
					color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
					#endif

					#ifdef UNITY_UI_ALPHACLIP
					clip(color.a - 0.001);
					#endif
					
					return color;
				}
			ENDCG
			}
		}
}