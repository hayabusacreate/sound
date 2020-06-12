Shader"SmyCustom/wipeTex"{
	Properties{
		_MainTex("None",2D) = "white"{}
	_MainTex2("MT",2D) = "white"{}
		_Radius("Radius",Range(-1,30)) = 0
			_Color("Color",Color) = (1,1,1,1)
	}
		SubShader{
			Pass {
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert_img
			#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _MainTex2;
			float _Radius;
			fixed4 _Color;
	fixed4 frag(v2f_img i) : COLOR
	{
		fixed4 c = tex2D(_MainTex,i.uv);
	i.uv -= fixed2(0.2, 0);
	i.uv.x *= 16.0 / 9.0;

	fixed4 color = (tex2D(_MainTex2, fixed2((i.uv.x*_Radius) + (i.uv.x-_Radius / 2), (i.uv.y*_Radius) + (i.uv.y - _Radius / 2)))*_Color>fixed4(0.2, 0.2, 0.2, 1)) ? c : fixed4(0, 0, 0, 1);

	return color;
	}
			ENDCG
		}
		}
}
