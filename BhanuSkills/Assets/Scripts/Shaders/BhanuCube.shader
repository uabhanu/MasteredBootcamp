Shader "BhanuCube"
{
    Properties
    {
        _MainTexture("Texture" , 2D) = "white" {}
        _Colour("Colour" , Color) = (0 , 1 , 1 , 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 position : SV_POSITION;
                    float2 uv : TEXTCOORD0;
                };

                fixed4 _Colour;
                sampler2D _MainTexture;

                v2f vert (appdata IN)
                {
                    v2f OUT;
                    OUT.position = UnityObjectToClipPos(IN.vertex);
                    OUT.uv = IN.uv;
                    return OUT;
                }

                fixed4 frag (v2f IN) : SV_Target
                {
                    fixed4 col = tex2D(_MainTexture , IN.uv);
                    return col * _Colour;
                }
            
            ENDCG
        }
    }
}
