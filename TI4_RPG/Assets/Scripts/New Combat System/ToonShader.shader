Shader "LimaShaders/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        
        _ShadowTone ("Shadow Color", color) = (0.4, 0.4, 0.4, 1)
        
        _HighlightCol ("Highlight Color", color) = (0.9, 0.9, 0.9, 1)
        _HighlightSize ("Highlight Size", Float) = 32
        
        _RimColor ("Rim Light Color", color) = (1, 1, 1, 1)
        _RimAmount ("Rim Color Amount" , Range(0,1)) = 0.716
        _RimThreshold (" Rim Threshold" , Range(0,1)) = 0.1
    }
    SubShader
    {

        Pass
        {
            Tags
            {
                "RenderType" = "Opaque"
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
            }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            //#include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                //SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float4 _ShadowTone;
            float4 _HighlightCol;
            float _HighlightSize;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                //TRANSFER_SHADOW(0)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                float3 viewDir = normalize(i.viewDir);
                
                float lit = dot(normal, _WorldSpaceLightPos0);
                
                //float shadow = SHADOW_ATTENUATION(i);
                
                float intensity = smoothstep(0, 0.01, lit);
                
                float light = intensity * _LightColor0;
                
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float reflection = dot(normal, halfVector);
                
                float highlightIntensity = pow(reflection * intensity, _HighlightSize * _HighlightSize);
                float smoothedHighlight = smoothstep(0.005, 0.01, highlightIntensity);
                float4 highlight = smoothedHighlight * _HighlightCol;
                
                float4 rimDot = 1 - dot(viewDir, normal);
                
                float rimIntensity = rimDot * pow(lit, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;
                
                //float intensity = lit > 0 ? 1 : 0;
                fixed4 col = tex2D(_MainTex, i.uv);
                
                return col * (light + _ShadowTone + highlight + rim);
            }
            ENDHLSL
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
    Fallback "Diffuse" 
}
