Shader "Custom/sample2"
{
	Properties{
		_MainTex("Texture", 2D) = "white"{}
		_EmissionMap("Emission Map", 2D) = "black" {}
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows alpha:fade
			#pragma target 3.0

			uniform sampler2D _EmissionMap;
		float4 _EmissionColor;

			struct Input {
				float2 uv_MainTex;
				float3 worldNormal;
				float3 viewDir;
			};

			sampler2D _MainTex;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
				float alpha = 1.2 - (abs(dot(IN.viewDir, IN.worldNormal)));
				o.Alpha = alpha * 1.5f;
				o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
			}
			ENDCG
	}
		FallBack "Diffuse"
}