Shader "Custom/rock"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 0.0
        _RampTex("Ramp",2D) = " white"{}
		_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
		_Color("Color", Color) = (1,1,1,1)
		_EmissionMap("Emission Map", 2D) = "black" {}
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf ToonRamp

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
	sampler2D _DisolveTex;
		sampler2D _RampTex;

		sampler2D _BumpMap;
		half _BumpScale;

		uniform sampler2D _EmissionMap;
		float4 _EmissionColor;

        struct Input
        {
            float2 uv_MainTex;
        };

		fixed4 _Color;
		half _Threshold;

		fixed4 LightingToonRamp(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			half d = dot(s.Normal, lightDir)*0.5 + 0.5;
			fixed3 ramp = tex2D(_RampTex, fixed2(d, 0.5)).rgb;
			fixed4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp;
			c.a = 0;
			return c;
		}

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed4 m = tex2D(_DisolveTex, IN.uv_MainTex);
			half g = m.r * 0.2 + m.g * 0.7 + m.b * 0.1;
			if (g < _Threshold) {
				discard;
			}
			o.Albedo = tex2D(_MainTex,IN.uv_MainTex)* _Color;

			fixed4 n = tex2D(_BumpMap, IN.uv_MainTex);

			o.Normal = UnpackScaleNormal(n, _BumpScale);
			o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
