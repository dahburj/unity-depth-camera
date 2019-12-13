#include "Common.cginc"

sampler2D_float _CameraDepthTexture;
half4 _Color;
int _R_State;
float _R_Min;
float _R_Max;
int _G_State;
float _G_Min;
float _G_Max;
int _B_State;
float _B_Min;
float _B_Max;

half4 DepthToRGBFragment(CommonVaryings input) : SV_Target
{
    float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, input.uv1));
    half4 src = tex2D(_MainTex, input.uv0);

    if (_R_State < 2)
    {
        float d = _R_State == 1 ? 1 - depth : depth;
        if (d >= _R_Min && d < _R_Max)
        {
            _Color.r = 1 - (d - _R_Min) * (1 / (_R_Max - _R_Min)); 
            _Color.a = 1;
        }
    }
    else if (_R_State == 2)
    {
        _Color.r = src.r;
    }
    
    if (_G_State < 2)
    {
        float d = _G_State == 1 ? 1 - depth : depth;
        if (d >= _G_Min && d < _G_Max)
        {
            _Color.g = 1 - (d - _G_Min) * (1 / (_G_Max - _G_Min)); 
            _Color.a = 1;
        }
    }
    else if (_G_State == 2)
    {
        _Color.g = src.g;
    }

    if (_B_State < 2)
    {
        float d = _B_State == 1 ? 1 - depth : depth;
        if (d >= _B_Min && d < _B_Max)
        {
            _Color.b = 1 - (d - _B_Min) * (1 / (_B_Max - _B_Min)); 
            _Color.a = 1;
        }
    }
    else if (_B_State == 2)
    {
        _Color.b = src.b;
    }

    return _Color;
}
