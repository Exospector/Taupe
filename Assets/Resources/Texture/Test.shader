Shader "Custom/Test" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Texture (RGB)", 2D) = "white" {}
		_SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
		_SliceAmount("Slice Amount", Range(0.0, 1.0)) = 0.9
	}
	SubShader {
		
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest Off
		ZWrite On
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SliceGuide;
			float _SliceAmount;
		};

		sampler2D _SliceGuide;
		float _SliceAmount;
		void surf(Input IN, inout SurfaceOutputStandard o) {
			clip(tex2D(_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG

	}
	FallBack "Diffuse"
}
