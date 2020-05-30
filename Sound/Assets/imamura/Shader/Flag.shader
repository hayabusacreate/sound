Shader "Custom/Flag"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
				_EmissionMap("Emission Map", 2D) = "black" {}
		[HDR] _EmissionColor("Emission Color", Color) = (0,0,0)
	}
		SubShader
		{
			Tags { "Queue" = "Geometry+2" "RenderType" = "Transparent" }

			Stencil {
				Ref 1
				Comp Greater
				Pass Replace
			}

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
			#pragma target 3.0

			sampler2D _MainTex;
		uniform sampler2D _EmissionMap;
		float4 _EmissionColor;

			struct Input
			{
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				float amp = 0.5*sin(_Time * 25 + v.vertex.z * 150);
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
			}

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;

			void surf(Input IN, inout SurfaceOutputStandard o)
			{
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
				o.Albedo = c.rgb;
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				o.Alpha = c.a;
				o.Emission = tex2D(_EmissionMap, IN.uv_MainTex) * _EmissionColor;
			}
			ENDCG
		}
			FallBack "Standard"
}
