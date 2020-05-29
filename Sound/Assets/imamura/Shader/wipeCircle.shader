Shader"SmyCustom/wipeCircle"{
	Properties{
		_MainTex("None",2D) = "white"{}
		_Radius("Radius",Range(0,2)) = 2
	}
		SubShader{
			Pass {
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert_img
			#pragma fragment frag

			sampler2D _MainTex;
			float _Radius;
	fixed4 frag(v2f_img i) : COLOR
	{
		fixed4 c = tex2D(_MainTex,i.uv);
	i.uv -= fixed2(0.5, 0.5);
	i.uv.x *= 16.0 / 9.0;

	fixed4 color = (distance(i.uv, fixed2(0, 0)) < _Radius) ? c : fixed4(0, 0, 0, 1);

	return color;
	}
			ENDCG
		}
		}
}
