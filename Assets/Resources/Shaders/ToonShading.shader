﻿Shader "Toon/Lighted" 
{
	Properties
	{
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf ToonRamp

		sampler2D _Ramp;

		#pragma lighting ToonRamp exclude_path:prepass
		inline half4 LightingToonRamp (SurfaceOutput s, half3 lightDir, half atten)
		{
			#ifndef USING_DIRECTIONAL_LIGHT
				lightDir = normalize (lightDir);
			#endif

			int intAtten = atten * 10.0f;

			float attenToMultiply = intAtten * 0.1f;

			half d = (dot (s.Normal, lightDir)*0.5 + 0.5) * attenToMultiply;
			half3 ramp = tex2D (_Ramp, float2(d,d)).rgb;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * ramp * (attenToMultiply * 2);
			c.a = 0;
			return c;
		}


		sampler2D _MainTex;
		float4 _Color;

		struct Input
		{
			float2 uv_MainTex : TEXCOORD0;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}