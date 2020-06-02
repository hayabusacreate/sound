Shader "Custom/crystal" 
{
		Properties{
			_MainTex("Texture", 2D) = "white"{}
			_EmissionMap("Emission Map", 2D) = "black" {}
			[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
			_Color("Color", Color) = (1,1,1,1)
			_Alpha("Alpha",Range(0.5,2)) = 1.5
			_Olpha("Olpha",Range(0.5,2)) = 1.5
			_Glossiness("Smoothness", Range(0,1)) = 0.5
			_Metallic("Metallic", Range(0,1)) = 0.0
		}
			SubShader{
				Tags { "RenderType" = "Opaque" }
				LOD 200

				CGPROGRAM
				#pragma surface surf Standard fullforwardshadows alpha:fade
				#pragma target 3.0

				uniform sampler2D _EmissionMap;
			float4 _EmissionColor;
			fixed _Alpha;
			fixed _Olpha;
			half _Glossiness;
			half _Metallic;

				struct Input {
					float2 uv_MainTex;
					float3 worldNormal;
					float3 viewDir;
				};

				sampler2D _MainTex;
				fixed4 _Color;

				void surf(Input IN, inout SurfaceOutputStandard o) {
					o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _Color;
					float alpha = _Olpha - (abs(dot(IN.viewDir, IN.worldNormal)));
					o.Alpha = alpha * _Alpha;
					o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
					o.Metallic = _Metallic;
					o.Smoothness = _Glossiness;
				}
				ENDCG
			}
				FallBack "Diffuse"
	}