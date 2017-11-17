Shader "Custom/NewSurfaceShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 100

		Pass{
		Lighting Off

		SetTexture[_MainTex]{
		// Sets our color as the 'constant' variable
		constantColor[_Color]

		// Multiplies color (in constant) with texture
		combine constant * texture
			}
		}
	}
	FallBack "Diffuse"
}
