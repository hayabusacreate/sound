Shader "Custom/magma"
{
	Properties
	{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 0.0
		_RampTex("Ramp",2D) = " white"{}
		_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
		_Alpha("Alpha",Range(0,1)) = 1.0
		_EmissionMap("Emission Map", 2D) = "black" {}               
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)    
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf ToonRamp alpha:fade

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;

		sampler2D _SubTex;
		sampler2D _MaskTex;
		sampler2D _DisolveTex;
			sampler2D _RampTex;

			sampler2D _BumpMap;
			half _BumpScale;

			uniform sampler2D _EmissionMap;
			float4 _EmissionColor;

			struct Input
			{
				float2 uv_MainTex;
			};

			half _Threshold;
			float _Alpha;

			fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				half d = dot(s.Normal, lightDir)*0.5 + 0.5;
				fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
				fixed4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * ramp;
				c.a = _Alpha;
				return c;
			}

			void surf(Input IN, inout SurfaceOutput o)
			{
				fixed4 m = tex2D(_DisolveTex, IN.uv_MainTex);
				half g = m.r * 0.2 + m.g * 0.7 + m.b * 0.1;
				if (g < _Threshold) {
					discard;
				}
				fixed2 uv = IN.uv_MainTex;
				uv.x -= 0.4* _Time;
				uv.y -= 0.0* _Time;
				fixed2 uv2 = IN.uv_MainTex;
				uv2.x -= 0.0* _Time;
				uv2.y -= 0.0* _Time;
				fixed4 c1 = tex2D(_MainTex, uv2);
				fixed4 c2 = tex2D(_SubTex, uv2);
				fixed4 p = tex2D(_MaskTex, uv);
				o.Albedo = lerp(c1, c2, p);
				o.Alpha = c1.a;
				fixed4 n = tex2D(_BumpMap, uv2);

				o.Normal = UnpackScaleNormal(n, _BumpScale);
				o.Emission = tex2D(_EmissionMap, uv) * _EmissionColor;
			}
			ENDCG
		}
			FallBack "Diffuse"
}

