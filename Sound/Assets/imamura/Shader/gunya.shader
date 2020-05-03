Shader "Custom/gunya" {
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
			   }
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200


			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert
			#pragma target 3.0

			sampler2D _MainTex;
			sampler2D _SubTex;
			sampler2D _MaskTex;

			struct Input {
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				float amp = 0.002*sin(_Time * 200  + v.vertex.z * 200);
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
				//v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
			}

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed2 uv = IN.uv_MainTex;
				uv.x -= 4* _Time;
				uv.y -= 0.0* _Time;
				fixed2 uv2 = IN.uv_MainTex;
				uv2.x -= 0.0* _Time;
				uv2.y -= 0.5* _Time;
				fixed4 c1 = tex2D(_MainTex, uv);
				fixed4 c2 = tex2D(_SubTex,  uv);
				fixed4 p = tex2D(_MaskTex, uv2);
				o.Albedo = lerp(c1, c2, p);
			}
			ENDCG
			
	}
		FallBack "Diffuse"
}