Shader "Custom/Toon"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampTex("Ramp",2D) = " white"{}
		_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
		_Alpha("Alpha",Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf ToonRamp alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		sampler2D _RampTex;
		sampler2D _BumpMap;
		half _BumpScale;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
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

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)* _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;

			fixed4 n = tex2D(_BumpMap, IN.uv_MainTex);

			o.Normal = UnpackScaleNormal(n, _BumpScale);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
