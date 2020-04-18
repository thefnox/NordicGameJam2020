Shader "Custom/EggYolkShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
		Pass
		{
			Stencil {
				Ref 2
				Comp notEqual
				Pass replace
			}

			CGPROGRAM

			#pragma vertex vert alpha
			#pragma fragment frag alpha

			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex  : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			v2f vert(appdata_t v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				v.texcoord.x = 1 - v.texcoord.x;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				nointerpolation fixed4 col = tex2D(_MainTex, i.texcoord) * _Color; // multiply by _Color
				return col;
			}

			ENDCG
		}
    }
}
