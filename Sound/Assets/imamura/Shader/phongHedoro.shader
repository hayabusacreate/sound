Shader "Custom/phongHedoro"
{
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
		_SubTex("Sub Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
	}
		SubShader
	{

		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf SimplePhong

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
			sampler2D _SubTex;
			sampler2D _MaskTex;

		struct Input
		{
			float2 uv_MainTex;
		};


		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed2 uv = IN.uv_MainTex;
			uv.x -= 0.0* _Time;
			uv.y -= 0.4* _Time;
			fixed2 uv2 = IN.uv_MainTex;
			uv2.x -= 0.0* _Time;
			uv2.y -= 0.5* _Time;
			fixed4 c1 = tex2D(_MainTex, uv);
			fixed4 c2 = tex2D(_SubTex, uv);
			fixed4 p = tex2D(_MaskTex, uv2);
			o.Albedo = lerp(c1, c2, p);
			/*o.Albedo = tex2D(_MainTex, IN.uv_MainTex);*/
		}

		half4 LightingSimplePhong(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
		{
			half NdotL = max(0, dot(s.Normal, lightDir));
			float3 R = normalize(-lightDir + 2.0 * s.Normal * NdotL);
			float3 spec = pow(max(0, dot(R, viewDir)), 10.0);

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * NdotL + spec + fixed4(0.1f, 0.1f, 0.1f, 1);
			c.a = s.Alpha;
			return c;
		}

		ENDCG
	}
		FallBack "Diffuse"
}

