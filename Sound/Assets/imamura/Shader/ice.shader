Shader "Custom/ice" {
	Properties{
	_MainTex("Texture", 2D) = "white"{}
	_Color("Color", Color) = (1,1,1,1)
		_DisolveTex("DisolveTex (RGB)", 2D) = "white" {}
		_Threshold("Threshold", Range(0,1)) = 1.0
			_BumpMap("Normal Map"  , 2D) = "bump" {}
		_BumpScale("Normal Scale", Range(0, 1)) = 1.0
			_EmissionMap("Emission Map", 2D) = "black" {}
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
	}
		SubShader{
			Tags { "Queue" = "Transparent" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard alpha:fade
			#pragma target 3.0

			uniform sampler2D _EmissionMap;
		float4 _EmissionColor;
		sampler2D _DisolveTex;
		sampler2D _BumpMap;
		half _BumpScale;

			struct Input {
				float2 uv_MainTex;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			half _Threshold;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 m = tex2D(_DisolveTex, IN.uv_MainTex);
				half g = m.r * 0.4 + m.g * 0.7 + m.b * 0.2;
				if (g < _Threshold) {
					discard;
				}
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color;
				o.Albedo = c.rgb;
				o.Alpha = (c.r*0.3 + c.g*0.6 + c.b*0.1 < 0.2) ? 1 : 0.5;
				o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
				fixed4 n = tex2D(_BumpMap, IN.uv_MainTex);

				o.Normal = UnpackScaleNormal(n, _BumpScale);
			}
			ENDCG
	}
		FallBack "Diffuse"
}