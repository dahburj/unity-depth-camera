Shader "Custom/DepthCam"
{
    Properties
    {
        _MainTex("", 2D) = ""{}
        _Color ("Background", COLOR) = (0,0,0,1)
        _R_State("R_State", Int) = 0
        _R_Min("R_Min", Float) = 0
        _R_Max("R_Max", Float) = 1
        _G_State("G_State", Int) = 0
        _G_Min("G_Min", Float) = 0
        _G_Max("G_Max", Float) = 1
        _B_State("B_State", Int) = 0
        _B_Min("B_Min", Float) = 0
        _B_Max("B_Max", Float) = 1
    }
    Subshader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #include "DepthToRGB.cginc"
            #pragma multi_compile _ UNITY_COLORSPACE_GAMMA
            #pragma vertex CommonVertex
            #pragma fragment DepthToRGBFragment
            #pragma target 3.0
            ENDCG
        }
    }
}
