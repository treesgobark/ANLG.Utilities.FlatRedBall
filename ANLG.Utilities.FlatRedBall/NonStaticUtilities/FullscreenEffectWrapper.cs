using FlatRedBall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ANLG.Utilities.FlatRedBall.NonStaticUtilities;

public class FullscreenEffectWrapper
{
    protected VertexPositionColorNormalTexture[] Vertices;
    protected VertexBuffer VertexBuffer;
    
    public float Period { get; set; } = 1;

    public FullscreenEffectWrapper()
    {
        Vertices = new VertexPositionColorNormalTexture[]
        {
            new(new Vector3(1f, 1f, 0f), Color.Blue, new Vector3(), new Vector2(1f, 0f)), // Top-Right
            new(new Vector3(1f, -1f, 0f), Color.Green, new Vector3(), new Vector2(1f, 1f)), // Bottom-Right
            new(new Vector3(-1f, 1f, 0f), Color.Red, new Vector3(), new Vector2(0f, 0f)), // Top-Left
            new(new Vector3(-1f, -1f, 0f), Color.Yellow, new Vector3(), new Vector2(0f, 1f)), // Bottom-Left
        };
        VertexBuffer = new VertexBuffer(FlatRedBallServices.GraphicsDevice, typeof(VertexPositionColorNormalTexture), Vertices.Length, BufferUsage.WriteOnly);
    }

    public virtual void Draw(Camera camera, Effect effect, Texture2D texture, float zDepth = 0, bool isVisible = true)
    {
        if (!isVisible) { return; }
        
        SetParameters(effect, camera, texture);
        SetVertexData(camera, Vertices, zDepth);
        DrawVertices(VertexBuffer, Vertices, effect);
    }

    public virtual void SetParameters(Effect effect, Camera camera, Texture2D texture)
    {
        effect.Parameters["CurrentTexture"]?.SetValue(texture);
        effect.Parameters["Time"]?.SetValue((float)TimeManager.CurrentScreenTime);
        effect.Parameters["NormalizedTime"]?.SetValue((float)TimeManager.CurrentScreenTime % Period);
        effect.Parameters["UVPerPixel"]?.SetValue(new Vector2(1f / camera.OrthogonalWidth, 1f / camera.OrthogonalHeight));
        effect.Parameters["Resolution"]?.SetValue(new Vector2(camera.OrthogonalWidth, camera.OrthogonalHeight));
    }

    public virtual void SetVertexData(Camera camera, VertexPositionColorNormalTexture[] vertices, float zDepth)
    {
        float internalAspectRatio = camera.OrthogonalWidth / camera.OrthogonalHeight;
        float externalAspectRatio = (float)FlatRedBallServices.GraphicsOptions.ResolutionWidth / FlatRedBallServices.GraphicsOptions.ResolutionHeight;

        float gameScreenWidth = 1;
        float gameScreenHeight = 1;

        if (internalAspectRatio < externalAspectRatio)
        {
            gameScreenWidth = internalAspectRatio / externalAspectRatio;
        }

        if (externalAspectRatio < internalAspectRatio)
        {
            gameScreenHeight = externalAspectRatio / internalAspectRatio;
        }

        vertices[0].Position = new Vector3(gameScreenWidth, gameScreenHeight, 0f); // Top-Right
        vertices[1].Position = new Vector3(gameScreenWidth, -gameScreenHeight, 0f); // Bottom-Right
        vertices[2].Position = new Vector3(-gameScreenWidth, gameScreenHeight, 0f); // Top-Left
        vertices[3].Position = new Vector3(-gameScreenWidth, -gameScreenHeight, 0f); // Bottom-Left

        vertices[0].Normal = new Vector3(camera.AbsoluteRightXEdge, camera.AbsoluteTopYEdge, zDepth); // Top-Right
        vertices[1].Normal = new Vector3(camera.AbsoluteRightXEdge, camera.AbsoluteBottomYEdge, zDepth); // Bottom-Right
        vertices[2].Normal = new Vector3(camera.AbsoluteLeftXEdge, camera.AbsoluteTopYEdge, zDepth); // Top-Left
        vertices[3].Normal = new Vector3(camera.AbsoluteLeftXEdge, camera.AbsoluteBottomYEdge, zDepth); // Bottom-Left
    }

    public virtual void DrawVertices(VertexBuffer vertexBuffer, VertexPositionColorNormalTexture[] vertices, Effect effect)
    {
        vertexBuffer.SetData(vertices);
        effect.CurrentTechnique.Passes[0].Apply();
        FlatRedBallServices.GraphicsDevice.SetVertexBuffer(vertexBuffer);
        FlatRedBallServices.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, vertices.Length - 2);
    }
}
