Shader "MyShader/Water"
{
	Properties
	{
		_MainColor("MainColor",COLOR) = (1,1,1,1)
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseMap("NoiseMap",2D) = "white" {}
		_Cubemap("Cubemap",Cube) = "_Skybox" {}
		_WaveXSpeed ("Wave Horizontal Speed", Range(-0.1, 0.1)) = 0.01
		_WaveYSpeed ("Wave Vertical Speed", Range(-0.1, 0.1)) = 0.01

	}
	SubShader
	{
		Tags {
		"RenderType"="Opaque"
		"Queue" = "Transparent"	
		}
		

		Pass
		{
			Tags {
			"LightMode" = "ForwardBase"
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma mulit_compile_fwdbase

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			fixed4 _MainColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _MainTex_TexelSize;
			sampler2D _NoiseMap;
			float4 _NoiseMap_ST;
			samplerCUBE _Cubemap;
			sampler2D _CameraDepthTexture;
			//变化矩阵
			float4x4 _FrustumCornersRay;
			fixed _WaveXSpeed;
			fixed _WaveYSpeed;



			struct v2f
			{
				float4 pos:SV_POSITION;
				float4 uv : TEXCOORD0;
				float4 TtoW0 : TEXCOORD1;
				float4 TtoW1 : TEXCOORD2;
				float4 TtoW2 : TEXCOORD3;
				float4 screenPos : TEXCOORD4;
				float4 interpolatedRay : TEXCOORD5;
				float2 uv_depth : TEXCOORD6;
			};

			
			
			v2f vert (appdata_tan v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv.xy = v.texcoord;
				o.uv.zw = TRANSFORM_TEX(v.texcoord, _NoiseMap);
				o.uv_depth = v.texcoord;

				//平台差异化处理
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
					o.uv_depth.y = 1 - o.uv_depth.y;
				#endif

				//确定射线方向
				int index = 0;
				if (v.texcoord.x < 0.5 && v.texcoord.y < 0.5) {
					index = 0;
				} else if (v.texcoord.x > 0.5 && v.texcoord.y < 0.5) {
					index = 1;
				} else if (v.texcoord.x > 0.5 && v.texcoord.y > 0.5) {
					index = 2;
				} else {
					index = 3;
				}
				#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						index = 3 - index;
				#endif
				
				o.interpolatedRay = _FrustumCornersRay[index];

				
				//得到切线空间到世界空间得矩阵
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal); 
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
				fixed3 worldBinormal = cross(worldNormal, worldTangent) * v.tangent.w; 
				o.TtoW0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);  
				o.TtoW1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);  
				o.TtoW2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);  
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
				float3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				float3 worldLight = normalize(UnityWorldSpaceLightDir(worldPos.xyz).xyz);
				//水波朝两个方向移动，一个主方向，一个次方向
				float2 speed = _Time.y * float2(_WaveXSpeed, _WaveYSpeed);
				fixed3 bump1 = UnpackNormal(tex2D(_NoiseMap, i.uv.zw + speed)).rgb;
				fixed3 bump2 = UnpackNormal(tex2D(_NoiseMap, i.uv.zw - speed)).rgb;
				fixed3 bump = normalize(bump1*0.7 + bump2*0.3);
				//计算法线值
				fixed3 bumpNormal = normalize(half3(dot(i.TtoW0.xyz, bump), dot(i.TtoW1.xyz, bump), dot(i.TtoW2.xyz, bump)));
				//得到屏幕图像得深度值，再还原世界空间的坐标
				float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv_depth));
				float linearEyeDepth = LinearEyeDepth(depth);
				float3 worldPos2 = _WorldSpaceCameraPos.xyz + i.interpolatedRay * linearEyeDepth;
				//对MainTex根据view方向确定反射强度
				float offset =saturate(dot(bumpNormal,viewDir));
				//对MainTex颜色采样
				fixed4 rfrCol = tex2D(_MainTex, i.uv.xy+offset);
				float dis =1;
				if(worldPos.y- worldPos2.y<0){
					float dis = 0;
				}
				fixed3 difuse = _LightColor0.rgb*_MainColor.rgb*saturate(dot(bumpNormal,worldLight))+
				rfrCol*dis;
				//反射
				fixed3 reflDir = reflect(-viewDir, bumpNormal);
				fixed3 reflCol = texCUBE(_Cubemap, reflDir).rgb * _MainColor.rgb ;

				fixed3 finalColor = reflCol +difuse;

				return fixed4(finalColor, 1);



			}
			ENDCG
		}
	}
}
