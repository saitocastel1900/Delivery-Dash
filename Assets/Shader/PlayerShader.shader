Shader "Unlit/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor("Main Color", Color) = (1,1,1,1)
        _RampTex ("Ramp Texture", 2D) = "white" {}
        _OutlineColor("Color", Color) = (1, 1, 1, 1)
        _OutlineWidth("Width", float) = 1
        _RimColor("RimColor", Color) = (1,1,1,1)
        _RimMultiplier ("Rim Multiplier", Float) = 1.5
        _SpecInt("SpecInt",Range(0,20)) = 0.5
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "RenderPipeline"="UniversalPipeline"
        }
        LOD 100

        Pass
        {
            Tags
            {
                "RenderType"="Opaque"
                "Queue"="Geometry"
                "RenderPipeline"="UniversalPipeline"
            }
            
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            half _OutlineWidth;
            half4 _OutlineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth / 100);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            Tags
            {
                "LightMode"="UniversalForward"
                "RenderPipeline"="UniversalPipeline"
            }
            LOD 100
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            
            half4 _MainColor;
            sampler2D _MainTex;
            sampler2D _RampTex;
            fixed4 _RimColor;
            float _RimMultiplier;
             float _lightInt;
            float _SpecInt;

            struct appdata
            {
                float3 normal : NORMAL;
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldNormal : NORMAL;
                float4 vertex : SV_POSITION;
                float3 ambient : COLOR0;
                float3 viewDir : TEXCOORD1;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex);
                 o.ambient = ShadeSH9(half4(o.worldNormal, 1));
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 light = normalize(_WorldSpaceLightPos0.xyz);
                float ndotl = dot(i.worldNormal, light);
                
                float rim = 1 - abs(dot(i.viewDir, i.worldNormal));
                
                float3 normal = normalize(i.worldNormal);
                float3 a = normal * max(0,dot(light, normal));
                float3 R = normalize(-light+2*a);
                float4 spec = pow(max(0, dot(R, i.viewDir)), _SpecInt);
                
                fixed3 ramp = tex2D(_MainTex,i.uv).rgb * tex2D(_RampTex, float2(ndotl, 0)).rgb * (_LightColor0 + float4(i.ambient, 0) + spec+(_RimColor * pow(rim, _RimMultiplier))).rgb;
                
                return fixed4(ramp,1);
            }
            ENDCG
        }
    }
}
