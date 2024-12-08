Shader "Unlit/ScanlineUnlitShader"
{
    Properties
    {
        _MainColor ("MainColor", Color) = (1,1,1,1)
        _SliceSpace ("SliceSpace", Range(0,30)) = 15
        _FillRatio("FillRatio",Range(0,1)) = 1
        _Step("Step",Range(0,1)) = 1
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode" = "UniversalForward"
                "RenderType"="Transparent"
                "Queue"="Transparent" }
             Blend SrcAlpha OneMinusSrcAlpha
            LOD 100
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _MainColor; 
            fixed _SliceSpace;
            fixed _FillRatio;
            fixed _Step;
            
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

            fixed4 frag (v2f i) : SV_Target
            {
                float interpolation = step(frac((i.uv.y+_Time.x) * _SliceSpace)-_FillRatio,_Step) ;
                clip(interpolation-0.5);
                return fixed4(_MainColor);
            }
            ENDCG
        }
    }
}
