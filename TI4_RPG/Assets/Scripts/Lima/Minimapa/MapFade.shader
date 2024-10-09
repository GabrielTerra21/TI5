Shader "Unlit/MapFade"
{
    Properties
    {
        [PerRendererData]_MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", color) = (1, 1, 1, 1)
        _Range ("Range", Range(0, 0.5)) = 0.4
        
        // Coisa de Manipulação de Imagem
        _StencilComp ("Stencil Comparison", float) = 8
        _Stencil ("Stencil ID", float ) = 0
        _StencilOp ("Stencil Operation", float) = 0
        _StencilWriteMask ("Stencil Write Mask", float) = 255
        _StencilReadMask ("Stencil Read Mask", float ) = 255
        
        _ColorMask ("Color Mask", float) = 15
        
        // Tenho quase Certeza que é irrelevante, mas n testei tirar ainda
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", float) = 0
    }
    SubShader
    {
        Tags {
            // Determina que vai ser desenhado na parte correta do buffer
            "Queue" = "Transparent"
            // Importante por motivos
            "IgnoreProjector" = "True"
            // Autoexplicativo 
            "RenderType" = "Transparent"
            // Não existe outro tipo de Preview possivel pra esse tipo de UI
            "PreviewType" = "Plane"
            // Mexe com mapeamento de Sprite pra uv
            "CanUseSpriteAtlas" = "True"
        }
        
        // Eu não sei exatamente o que que todas as coisas aqui fazem
        // Eu sei que todas tem a ver com manipulação de Imagem na UI
        Stencil{
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
        
        // Configuração em ShaderLab 
        // Precisa de tudo isso pra renderizar corretamente na UI
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        // BlendMode usado, você pode usar outro sem problema, só deixei esse pq é o mais comum e acho que é o que 
        // você ta precisando
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            // Declara o Vertex Shader
            #pragma vertex vert
            // Declara o Fragment Shader
            #pragma fragment frag

            #include "UnityCG.cginc"
            // Inclui funções especificas pra UI
            #include  "UnityUI.cginc"

            // Importante pra UI
            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            //Input
            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // V 2 F ou "Vertex to Fragment", é o struct que determina o que que o vertex shader manda pro
            // fragment shader
            struct v2f
            {
                // Todos Importantes para UI
                // Tem como mudar os nomes da direita, mas os da esquerda são predeterminados por uma lista de
                // Espaços pra transferencia de dados
                // Todos necessarios
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex; // Necessario
            fixed4 _Color;
            fixed4 _TextureSampleAdd; // Necessario
            float4 _ClipRect; // Necessario
            float4 _MainTex_ST; // Necessario
            float _Range;

            // Vertex Shader, um foreach por vertice
            v2f vert (appdata v)
            {
                v2f o; // declaração do output

                // Não sei o que nenhuma dessas duas coisas fazem exatamente
                // mas não recomendo tirar
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                
                o.worldPosition = v.vertex; // recebe a informação do input
                o.vertex = UnityObjectToClipPos(o.worldPosition); // converte a coordenada recebida do input pra clipspace, bota dentro do frustrum
                
                o.uv = TRANSFORM_TEX(v.uv, _MainTex); // Faz mapeamento da uv

                o.color = v.color * _Color; // pega cor do input
                return o; // retorna o input para o Fragment Shader
            }

            // Função que eu fiz pro efeito especifico que eu queria, pode desconsiderar
            float CalculateDistance(in v2f i)
            {
                float2 center = (0.5, 0.5);
                float distance = length(i.uv - center) / 0.5;
                return distance;
            }

            // Fragment Shader
            fixed4 frag (v2f i) : SV_Target
            {
                // Cor da textura recebida, a parte do _TextureSampleAdd é incomun
                half4 color = (tex2D(_MainTex, i.uv) + _TextureSampleAdd) * i.color;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping( i.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                // Minha logica pra distancia, pode desconsiderar
                float scale = CalculateDistance(i);
                scale *= scale;
                scale = 1 - step(scale, _Range);

                
                return color * (1- scale); // multiplica a cor pelo valor do meu calculo, pra reduzir o alpha de acordo
            }
            ENDCG
        }
    }
}
