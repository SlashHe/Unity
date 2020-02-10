Shader "MyShader/SkillRange"
{
	Properties
	{
		_MainColor("MainColor",COLOR) = (1,1,1,1)
		_ColorPower("ColorPower",Range(0,2))=1
		_IntersecRange("IntersecRange",Range(0,1))=0.4
		_MainTex("MainTex",2D) = "white" {}
		_NoiseTex("NoiseTex",2D) = "white" {}
		_speed("speed",Range(0,1))=1

	}
	SubShader
	{
		Tags { 
		"RenderType"="Transparent"
		"Queue" = "Transparent"
		"IgnoreProjector" = "true"
		
		}
		

		Pass
		{
			ZWrite Off
			Cull Off
			Blend One One

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct v2f
			{
				
				float4 pos : SV_POSITION;
				float4 screenPos : TEXCOORD0;
				float4 uv : TEXCOORD1;
			};

			fixed4 _MainColor;
			float _ColorPower;
			sampler2D _CameraDepthTexture;
			float _IntersecRange;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _speed;
			
			v2f vert (appdata_base v)
			{
				v2f o;
				o.uv.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.uv.zw = TRANSFORM_TEX(v.texcoord,_NoiseTex);
				o.pos = UnityObjectToClipPos(v.vertex);
				//得到屏幕空间坐标
				o.screenPos = ComputeScreenPos(o.pos);
				//将Z代入函数，得到view空间深度值
				COMPUTE_EYEDEPTH(o.screenPos.z);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				
				i.uv.w = i.uv.w+_Time.y*_speed;
				fixed mainTexCol = tex2D(_MainTex,i.uv.xy).r;
				fixed noiseTexCol = tex2D(_NoiseTex,i.uv.zw).r;
				fixed3 texCol = _MainColor.rgb*mainTexCol*noiseTexCol;
				float screenZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
				screenZ = LinearEyeDepth(screenZ);
				//相交得范围在0-_IntersecRange
				float dis =saturate( _IntersecRange-abs(i.screenPos.z - screenZ));
				//fixed3 col = _MainColor.rgb*dis*_ColorPower+texCol;
				fixed3 col = lerp(texCol,_MainColor.rgb*dis*_ColorPower,dis);
				return fixed4(col,1);
			}
			ENDCG
		}
	}
}
