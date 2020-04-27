Shader "Custom/SimpleLambert"
{
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
	}
    SubShader
    {
		
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

				   sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };


        void surf (Input IN, inout SurfaceOutput o)
        {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex);
        }

		half4 LightingSimpleLambert(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = max(0, dot(s.Normal, lightDir));
			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb*NdotL + fixed4(0.2f, 0.2f, 0.2f, 1);
			c.a = s.Alpha;
			return c;
		}

        ENDCG
    }
    FallBack "Diffuse"
}
