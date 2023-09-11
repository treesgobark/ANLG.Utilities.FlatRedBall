#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0

//==============================================================================
// External Parameters
//==============================================================================
float Time;
float NormalizedTime;
float2 UVPerPixel;
float2 Resolution;

texture CurrentTexture;
sampler pointTextureSampler = sampler_state
{
    Texture = <CurrentTexture>;
    MipFilter = Point;
    MinFilter = Point;
    MagFilter = Point;
};

//==============================================================================
// Shader Stage Parameters
//==============================================================================
struct AssemblerToVertex
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float4 TexCoord : TEXCOORD0;
    float4 Normal : NORMAL0;
};

struct VertexToPixel
{
    float4 Position : SV_Position0;
    float4 Color : COLOR0;
    float4 TexCoord : TEXCOORD0;
    float4 ScreenPosition : TEXCOORD1;
    float4 WorldPosition : TEXCOORD2;
};

//==============================================================================
// Vertex Shaders
//==============================================================================
VertexToPixel VsMain(const in AssemblerToVertex input)
{
    VertexToPixel output;
    
    output.Position = input.Position;
    output.ScreenPosition = input.Position;
    output.WorldPosition = input.Normal;
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;
    
    return output;
}

//==============================================================================
// Pixel Shaders
//==============================================================================
float4 PsMain(VertexToPixel input) : SV_TARGET
{
    float4 textureSample = tex2D(pointTextureSampler,  input.TexCoord.xy);
    
    return TexWeight * textureSample
        + PixelPosWeight * float4(input.Position.xy, 0, 0)
        + ScreenPosWeight * input.ScreenPosition
        + WorldPosWeight * input.WorldPosition
        + ColorWeight * input.Color
        + UvWeight * input.TexCoord;
}

//==============================================================================
// Techniques
//==============================================================================
technique Tech0
{
    pass Pass0
    {
        VertexShader = compile VS_SHADERMODEL VsMain();
        PixelShader = compile PS_SHADERMODEL PsMain();
    }
}
