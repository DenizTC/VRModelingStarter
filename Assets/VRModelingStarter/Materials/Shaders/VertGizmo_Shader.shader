Shader "Custom/VertGizmo_Shader" {
	Properties {
		[MaterialToggle] _IsPinchReady("Is pinch ready", Float) = 0
		[MaterialToggle] _IsPinched("Is pinching", Float) = 0
		_Color ("Color", Color) = (1,0,0,1)
		_ColorPinchReady ("Pinch ready color", Color) = (1,1,0,1)
		_ColorPinching ("Pinching color", Color) = (0,1,0,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags
		{ 
			"Queue" = "Transparent" 
			"RenderType" = "Transparent" 
		}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _ColorPinchReady;
		fixed4 _ColorPinching;
		uniform float _IsPinchReady;
		uniform float _IsPinched;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed3 fc = lerp(_Color.rgb, _ColorPinchReady.rgb, _IsPinchReady);
			fc = lerp(fc, _ColorPinching.rgb, _IsPinched);

			o.Albedo = fc;
			o.Alpha = 0.25;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			
		}
		ENDCG
	}
	FallBack "Diffuse"
}
