Shader"SmyCustom/wipeLine"{
	Properties{
		_MainTex("None",2D) = "white"{}
		_Length("Length",Range(0,0.5)) = 0.5
	}
		SubShader{
			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex:POSITION;
					float2 uv:TEXCOORD0;
				};

				struct v2f {
					float4 vertex:SV_POSITION;
					float2 uv:TEXCOORD0;
				};

				sampler2D _MainTex;
				float _Length;

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) :COLOR{
					//ここでカメラに写っている色情報を取得する
					fixed4 c = tex2D(_MainTex,i.uv);
				//uv座標のｙから0.5を引いた値の絶対値（ここでは画面の真ん中を(0.5,0.5)としている）
				fixed l = abs(i.uv.y - 0.5);
				//lの値が＿Length以下ならそのまま描画する。違うのなら黒く描画
				fixed4 color = (l < _Length) ? c : fixed4(0, 0, 0, 1);

				return color;
			}
			ENDCG
		}
		}
}
