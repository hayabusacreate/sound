Shader "UI/UIshader"
{
	Properties{
	_MainTex("Texture", 2D) = "white" {}
	_MainTex2("Texture", 2D) = "white" {}
	}
		SubShader{
		  Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		  Blend SrcAlpha OneMinusSrcAlpha

		  Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
			  float2 uv : TEXCOORD0;
			  float4 vertex : POSITION;
			};
			struct v2f {
			  float2 uv : TEXCOORD0;
			  float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex,_MainTex2;
			float4 _MainTex_ST;

			v2f vert(appdata v) {
			  v2f o;
			  o.vertex = UnityObjectToClipPos(v.vertex); //==mul(UNITY_MATRIX_MVP, v.vertex);
			  o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			  return o;
			}

			fixed4 frag(v2f i) : SV_Target {
			  fixed4 txt = tex2D(_MainTex , i.uv);
			  fixed4 tex = tex2D(_MainTex2, i.uv);
			  return fixed4(tex.r,tex.g,tex.b,txt.a);
			}
			ENDCG
		  }
	}
}