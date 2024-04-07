// Shader "Custom/SpriteOutline"
// {
//     Properties
//     {
//         _MainTex("Texture", 2D) = "white" {}
//         _OutlineColor("Outline Color", Color) = (1, 1, 0, 1)
//         _OutlineWidth("Outline Width", Range(0, 10)) = 0.02
//     }

//     SubShader
//     {
//         Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

//         Blend SrcAlpha OneMinusSrcAlpha
//         Cull Off
//         ZWrite Off

//         Pass
//         {
//             CGPROGRAM
//             #pragma vertex vert
//             #pragma fragment frag
//             #include "UnityCG.cginc"

//             struct appdata
//             {
//                 float4 vertex : POSITION;
//                 float2 uv : TEXCOORD0;
//             };

//             struct v2f
//             {
//                 float2 uv : TEXCOORD0;
//                 float4 vertex : SV_POSITION;
//             };

//             sampler2D _MainTex;
//             float4 _MainTex_ST;
//             float4 _OutlineColor;
//             float _OutlineWidth;

//             v2f vert(appdata v)
//             {
//                 v2f o;
//                 o.vertex = UnityObjectToClipPos(v.vertex);
//                 o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//                 return o;
//             }

//             fixed4 frag(v2f i) : SV_Target
//             {
//                 fixed4 col = tex2D(_MainTex, i.uv);
//                 float2 pixelSize = _OutlineWidth / _ScreenParams.xy;

//                 // Sample neighboring pixels (including diagonals)
//                 fixed4 up = tex2D(_MainTex, i.uv + float2(0, pixelSize.y));
//                 fixed4 down = tex2D(_MainTex, i.uv - float2(0, pixelSize.y));
//                 fixed4 left = tex2D(_MainTex, i.uv - float2(pixelSize.x, 0));
//                 fixed4 right = tex2D(_MainTex, i.uv + float2(pixelSize.x, 0));
//                 fixed4 upLeft = tex2D(_MainTex, i.uv - float2(pixelSize.x, -pixelSize.y));
//                 fixed4 upRight = tex2D(_MainTex, i.uv + float2(pixelSize.x, pixelSize.y));
//                 fixed4 downLeft = tex2D(_MainTex, i.uv - float2(pixelSize.x, pixelSize.y));
//                 fixed4 downRight = tex2D(_MainTex, i.uv + float2(pixelSize.x, -pixelSize.y));

//                 // Check if the current pixel is transparent and any neighboring pixel is not transparent
//                 float outline = col.a * (1 - up.a * down.a * left.a * right.a * upLeft.a * upRight.a * downLeft.a * downRight.a);

//                 // Mix the sprite color with the outline color based on the outline value
//                 col.rgb = lerp(col.rgb, _OutlineColor.rgb, outline);

//                 return col;
//             }
//             ENDCG
//         }
//     }
// }

Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1, 1, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 10)) = 0.02
    }

    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineWidth;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float2 texelSize = _OutlineWidth / _ScreenParams.xy;
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;
                float2 pixelSize = float2(texelSize.x, texelSize.y * aspectRatio);

                // Sample neighboring pixels (including diagonals)
                fixed4 up = tex2D(_MainTex, i.uv + float2(0, pixelSize.y));
                fixed4 down = tex2D(_MainTex, i.uv - float2(0, pixelSize.y));
                fixed4 left = tex2D(_MainTex, i.uv - float2(pixelSize.x, 0));
                fixed4 right = tex2D(_MainTex, i.uv + float2(pixelSize.x, 0));
                fixed4 upLeft = tex2D(_MainTex, i.uv - float2(pixelSize.x, -pixelSize.y));
                fixed4 upRight = tex2D(_MainTex, i.uv + float2(pixelSize.x, pixelSize.y));
                fixed4 downLeft = tex2D(_MainTex, i.uv - float2(pixelSize.x, pixelSize.y));
                fixed4 downRight = tex2D(_MainTex, i.uv + float2(pixelSize.x, -pixelSize.y));

                // Check if the current pixel is transparent and any neighboring pixel is not transparent
                float outline = col.a * (1 - up.a * down.a * left.a * right.a * upLeft.a * upRight.a * downLeft.a * downRight.a);

                // Mix the sprite color with the outline color based on the outline value
                col.rgb = lerp(col.rgb, _OutlineColor.rgb, outline);

                return col;
            }
            ENDCG
        }
    }
}