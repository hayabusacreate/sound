Shader "Custom/sample2"
{
	Properties{
		_MainTex("Texture", 2D) = "white"{}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows alpha:fade
			#pragma target 3.0

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
			}
			ENDCG
	}
		FallBack "Diffuse"
}