Shader "Custom/blend" {
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
			   }
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200


			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _SubTex;
			sampler2D _MaskTex;

			struct Input {
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed2 uv = IN.uv_MainTex;
				uv.x -= 0.0* _Time;
				uv.y -= 0.4* _Time;
				fixed4 c1 = tex2D(_MainTex, uv);
				fixed4 c2 = tex2D(_SubTex,  uv);
				fixed4 p = tex2D(_MaskTex, IN.uv_MainTex);
				o.Albedo = lerp(c1, c2, p);
			}
			ENDCG
			
	}
		FallBack "Diffuse"
}