
    //<summary>
    // Basic shader.
	// Only draws model with a texture for testing.
    //</summary>

float4x4 xWorldViewProjectionMatrix;	

texture xColorMap;
sampler ColorMapSampler = sampler_state
{
   Texture = <xColorMap>;
   MinFilter = Linear;
   MagFilter = Linear;
   MipFilter = Linear;   
};

struct ShaderOutput
{
    float4 Position : POSITION;
    float2 Texture : TEXCOORD0;
};

ShaderOutput VertexShaderFunction(ShaderOutput aInput)
{
	ShaderOutput Output = (ShaderOutput)0; 

	Output.Position = mul(aInput.Position, xWorldViewProjectionMatrix);
	Output.Texture = aInput.Texture;

	return Output;
}

float4 PixelShaderFunction(ShaderOutput aInput) : COLOR
{
	ShaderOutput Output = (ShaderOutput)0; 
	Output.Position = mul(aInput.Position, xWorldViewProjectionMatrix);

	float4 Color = tex2D(ColorMapSampler, aInput.Texture); 
	
	return Color;
}

technique Standard
{
    pass PassOne
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader  = compile ps_3_0 PixelShaderFunction();
    }
}

technique Debug
{
    pass PassOne
    {
      
    }
}