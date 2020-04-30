Shader "Custom/invisible" {
	SubShader{
		Tags { "Queue" = "Geometry+2" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Standard alpha:fade 
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = fixed4(0.6f, 0.6f, 0.6f, 1);
			o.Alpha = 0.3;
		}
		ENDCG
	}
		FallBack "Diffuse"
}