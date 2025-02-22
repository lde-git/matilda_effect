Shader "Custom/BlurredLineRenderer" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        
        Pass {
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _BlurSize;
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Define a small offset based on blur size and screen-space factors
                float2 offset = _BlurSize * 0.001; 
                fixed4 col = fixed4(0, 0, 0, 0);
                
                // Sample in a cross pattern
                col += tex2D(_MainTex, i.uv + float2(-offset.x, 0));
                col += tex2D(_MainTex, i.uv + float2(offset.x, 0));
                col += tex2D(_MainTex, i.uv + float2(0, -offset.y));
                col += tex2D(_MainTex, i.uv + float2(0, offset.y));
                col *= 0.25;
                
                return col * _Color;
            }
            ENDCG
        }
    }
}
