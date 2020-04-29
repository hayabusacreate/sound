Shader "Custom/hedoro" {
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 0.0


		_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
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
			sampler2D _DisolveTex;

			sampler2D _BumpMap;
			half _BumpScale;

			struct Input {
				float2 uv_MainTex;
			};

			half _Threshold;

			UNITY_INSTANCING_BUFFER_START(Props)
				UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {

				fixed4 m = tex2D(_DisolveTex, IN.uv_MainTex);
				half g = m.r * 0.2 + m.g * 0.7 + m.b * 0.1;
				if (g < _Threshold) {
					discard;
				}
				fixed2 uv = IN.uv_MainTex;
				uv.x -= 0.0* _Time;
				uv.y -= 0.4* _Time;
				fixed2 uv2 = IN.uv_MainTex;
				uv2.x -= 0.0* _Time;
				uv2.y -= 0.5* _Time;
				fixed4 c1 = tex2D(_MainTex, uv);
				fixed4 c2 = tex2D(_SubTex,  uv);
				fixed4 p = tex2D(_MaskTex, uv2);
				o.Albedo = lerp(c1, c2, p);

				fixed4 n = tex2D(_BumpMap, uv2);

				o.Normal = UnpackScaleNormal(n, _BumpScale);
			}
			ENDCG
			
	}
		FallBack "Diffuse"
}