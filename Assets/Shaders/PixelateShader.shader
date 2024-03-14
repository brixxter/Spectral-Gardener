Shader "Hidden/PixelateShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
		_PixelCountU ("Pixel Count U", float) = 16
		_PixelCountV ("Pixel Count V", float) = 9
        _ColorDepth ("Color Depth", float) = 16
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Color;
			float _PixelCountU;
			float _PixelCountV;
            float _ColorDepth;

            fixed4 frag (v2f i) : SV_Target
            {
                float pixelWidth = 1.0f / _PixelCountU;
				float pixelHeight = 1.0f / _PixelCountV;
				
				float2 uv = float2((int)(i.uv.x / pixelWidth) * pixelWidth, (int)(i.uv.y / pixelHeight) * pixelHeight);			
				float4 col = tex2D(_MainTex, uv);
                float4 clampedCol = trunc(col * _ColorDepth) / _ColorDepth;
			
			    return clampedCol;
            }
            ENDCG
        }
    }
}
