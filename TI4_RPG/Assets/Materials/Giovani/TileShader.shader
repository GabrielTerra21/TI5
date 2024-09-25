Shader "Custom/TileShader"
{
    Properties
    {
        _Color1 ("Color One", Color) = (1,1,1,1)
        _Color2 ("Color Two", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _A ("A", Range(-3, 3)) = 1.0
        _B ("B", Range(-3, 3)) = 1.0
    }
    SubShader
    {
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input{float2 uv_MainTex;};
        fixed4 _Color1, _Color2;
        half _A, _B;

        void surf (Input IN, inout SurfaceOutput o)
        {
            half z = IN.uv_MainTex.x - IN.uv_MainTex.y;
            half a = saturate(round(-2 * z + _A));
            half b = saturate(round(2 * z + _B));
            o.Albedo = a * _Color1 + b * _Color2; 
        }
        ENDCG
    }
}
