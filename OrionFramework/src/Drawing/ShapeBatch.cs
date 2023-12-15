using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OrionFramework.BaseEngine;
using OrionFramework.CameraView;

namespace OrionFramework.Drawing;

internal class ShapeBatch : IDisposable
{
    // -- Properties / Fields

    private int _maxVertices => 2048;
    private int _maxIndeces => _maxVertices * 3;

    private GraphicsDevice _device;
    private BasicEffect _effect;
    private readonly VertexPositionColor[] _vertices;
    private readonly int[] _indeces;

    private int _indexCount;
    private int _vertexCount;
    private int _shapeCount;
    private bool _isStarted;
    private bool _isDisposed;

    // -- Initialization

    public ShapeBatch(GraphicsDevice device)
    {
        _device = device;
        _effect = new BasicEffect(device);
        _vertices = new VertexPositionColor[_maxVertices];
        _indeces = new int[_maxIndeces];
    }

    // -- Public Interface

    public void Begin(Matrix? transformMatrix = null, BlendState? blend = null)
    {
        if (_isStarted)
            throw new Exception("Shape batching was already started.");

        _device.BlendState = blend ?? BlendState.AlphaBlend;
        _effect.Projection = Matrix.CreateOrthographicOffCenter(0f, _device.Viewport.Width, _device.Viewport.Height, 0f, 0f, 1f);
        _effect.World = Matrix.CreateTranslation(0.5f, 0.5f, 0f);
        _effect.View = transformMatrix ?? Matrix.Identity;
        _effect.VertexColorEnabled = true;
        _effect.FogEnabled = false;
        _effect.TextureEnabled = false;
        _isStarted = true;
    }

    public void End()
    {
        Flush();
        _isStarted = false;
    }

    // -- Internal Processing

    private void Flush()
    {
        EnsureStarted();
        _shapeCount = _indexCount / 3;

        if (_shapeCount == 0)
            return;

        _effect.CurrentTechnique.Passes[0].Apply();
        _device.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _vertices, 0, _vertexCount,
            _indeces, 0, _shapeCount);

        _indexCount = 0;
        _vertexCount = 0;
        _shapeCount = 0;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing || _isDisposed)
            return;

        if (_effect is not null)
            _effect.Dispose();

        _isDisposed = true;
    }

    private void EnsureStarted()
    {
        if (!_isStarted)
            throw new Exception("The Shape batcher was never started.");
    }

    private void EnsureSpace(int vertexCount, int indexCount)
    {
        if (vertexCount >= 1024)
            throw new Exception("Too many vertices to fit in the batch at once");
        if (indexCount >= 3064)
            throw new Exception("Too many indeces to fit in the batch at once");

        bool maxVerticesReached = _vertexCount + vertexCount >= _maxVertices;
        bool maxIndecesReached = _indexCount + vertexCount >= _maxIndeces;

        if (maxVerticesReached || maxIndecesReached)
            Flush();
    }

    // -- Batch Allocation

    private void AllocateVertices(Vector3[] positions, Color color)
    {
        for (int x = 0; x < positions.Length; x++)
            _vertices[_vertexCount++] = new VertexPositionColor(positions[x], color);
    }

    private void AllocateIndeces(int faces)
    {
        int last_index = 1;
        for (int i = 0; i < faces; i++)
        {
            _indeces[_indexCount++] = 0 + _vertexCount;
            _indeces[_indexCount++] = last_index + _vertexCount;
            _indeces[_indexCount++] = last_index + 1 + _vertexCount;
            last_index++;
        }
    }

    // -- Primitive Methods

    /// <summary>Draw a primitive filled rectangle.</summary>
    public void DrawFillRect(float x, float y, float w, float h, Color color, float rotation = 0f, bool outline = false, Color? outlineColor = null)
    {
        EnsureStarted();
        EnsureSpace(vertexCount: 4, indexCount: 6);

        float left = x - 0.5f;
        float top = y - 0.5f;
        float right = x + w - 0.5f;
        float bottom = y + h - 0.5f;

        var vertexPositions = new Vector3[4];
        vertexPositions[0] = new Vector3(left, top, 0f);
        vertexPositions[1] = new Vector3(right, top, 0f);
        vertexPositions[2] = new Vector3(right, bottom, 0f);
        vertexPositions[3] = new Vector3(left, bottom, 0f);

        AllocateIndeces(faces: 2);
        AllocateVertices(vertexPositions, color);

        if (outline)
        {
            DrawRect(x, y, w, h, outlineColor ?? Color.Black, 1, rotation);
        }
    }

    /// <summary>Draw a primitive line segment with an optional thickness.</summary>
    public void DrawLine(int x1, int y1, int x2, int y2, Color color, float thickness = 1)
    {
        EnsureStarted();
        EnsureSpace(vertexCount: 4, indexCount: 6);
        thickness = Math.Clamp(thickness, 1, 30);

        var pointA = new Vector3(x1, y1, 0f);
        var pointB = new Vector3(x2, y2, 0f);
        Vector3 direction = Vector3.Normalize(pointB - pointA);

        Vector3 edgeRight = direction * (thickness * 0.5f);
        Vector3 edgeLeft = -direction * (thickness * 0.5f);
        var edgeUp = new Vector3(edgeRight.Y, -edgeRight.X, 0f);
        var edgeDown = new Vector3(edgeLeft.Y, -edgeLeft.X, 0f);

        var vertexPositions = new Vector3[4];
        vertexPositions[0] = pointA + edgeUp + edgeLeft;
        vertexPositions[1] = pointB + edgeUp + edgeRight;
        vertexPositions[2] = pointB + edgeDown + edgeRight;
        vertexPositions[3] = pointA + edgeDown + edgeLeft;

        AllocateIndeces(faces: 2);
        AllocateVertices(vertexPositions, color);
    }


    /// <summary>Draw a primitive filled circle using 80 vertices.</summary>
    public void DrawFillCircle(int x, int y, float radius, Color color)
    {
        EnsureStarted();
        EnsureSpace(vertexCount: 80, indexCount: 234); // indeces = vertices * 3 - 6

        float cos = MathF.Cos(MathHelper.TwoPi / 80);
        float sin = MathF.Sin(MathHelper.TwoPi / 80);
        float currentX = MathF.Cos(0f) * radius;
        float currentY = MathF.Sin(0f) * radius;

        Vector3[] positions = new Vector3[80];
        for (int z = 0; z < positions.Length; z++)
        {
            float xx = cos * currentX - sin * currentY;
            float yy = sin * currentX + cos * currentY;

            positions[z] = new Vector3(xx + x, yy + y, 0f);
            currentX = xx;
            currentY = yy;
        }

        AllocateIndeces(faces: 78); // vertices - 2
        AllocateVertices(positions, color);
    }

    /// <summary>Draw a primitive circle using 80 vertices. Giving it a rotation(radians) will draw a line from the center.</summary>
    public void DrawCircle(int x, int y, float radius, Color color, int thickness = 1, float rotation = 0f)
    {
        EnsureStarted();

        float cos = MathF.Cos(MathHelper.TwoPi / 80);
        float sin = MathF.Sin(MathHelper.TwoPi / 80);
        float currentX = MathF.Cos(rotation) * radius;
        float currentY = MathF.Sin(rotation) * radius;

        Vector3[] positions = new Vector3[80];
        for (int z = 0; z < positions.Length; z++)
        {
            float xx = cos * currentX - sin * currentY;
            float yy = sin * currentX + cos * currentY;

            positions[z] = new Vector3(xx + x, yy + y, 0f);
            currentX = xx;
            currentY = yy;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 pointOne = positions[i];
            Vector3 pointTwo = i == positions.Length - 1 ? positions[0] : positions[i + 1];
            DrawLine((int)pointOne.X, (int)pointOne.Y, (int)pointTwo.X, (int)pointTwo.Y, color, thickness);
        }

        if (rotation != 0f)
            DrawLine(x, y, (int)positions[^1].X, (int)positions[^1].Y, color, thickness);
    }

    public void DrawPolygon(int x, int y, int sides, float radius, Color color, int thickness = 1, float rotation = 0f)
    {
        EnsureStarted();

        float cos = MathF.Cos(MathHelper.TwoPi / sides);
        float sin = MathF.Sin(MathHelper.TwoPi / sides);
        float currentX = MathF.Cos(rotation) * radius;
        float currentY = MathF.Sin(rotation) * radius;

        Vector3[] positions = new Vector3[sides];
        for (int z = 0; z < positions.Length; z++)
        {
            float xx = cos * currentX - sin * currentY;
            float yy = sin * currentX + cos * currentY;

            positions[z] = new Vector3(xx + x, yy + y, 0f);
            currentX = xx;
            currentY = yy;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 pointOne = positions[i];
            Vector3 pointTwo = i == positions.Length - 1 ? positions[0] : positions[i + 1];
            DrawLine((int)pointOne.X, (int)pointOne.Y, (int)pointTwo.X, (int)pointTwo.Y, color, thickness);
        }

        if (rotation != 0f)
            DrawLine(x, y, (int)positions[^1].X, (int)positions[^1].Y, color, thickness);
    }

    public void DrawFillPolygon(int x, int y, int sides, float radius, Color color, float rotation = 0f)
    {
        EnsureStarted();
        EnsureSpace(vertexCount: sides, indexCount: sides * 3 - 6);

        float cos = MathF.Cos(MathHelper.TwoPi / sides);
        float sin = MathF.Sin(MathHelper.TwoPi / sides);
        float currentX = MathF.Cos(rotation) * radius;
        float currentY = MathF.Sin(rotation) * radius;

        Vector3[] positions = new Vector3[sides];
        for (int z = 0; z < positions.Length; z++)
        {
            float xx = cos * currentX - sin * currentY;
            float yy = sin * currentX + cos * currentY;

            positions[z] = new Vector3(xx + x, yy + y, 0f);
            currentX = xx;
            currentY = yy;
        }

        AllocateIndeces(faces: sides - 2);
        AllocateVertices(positions, color);
    }

    /// <summary>Draw a primitive rectangle with an optional thickness.</summary>
    public void DrawRect(float x, float y, float w, float h, Color color, int thickness = 1, float rotation = 0f)
    {
        EnsureStarted();
        int left = (int)x;
        int top = (int)y;
        int right = (int)(x + w - 1);
        int bottom = (int)(y + h - 1);


        DrawLine(left, top, right, top, color, thickness);
        DrawLine(right, top, right, bottom, color, thickness);
        DrawLine(right, bottom, left, bottom, color, thickness);
        DrawLine(left, bottom, left, top, color, thickness);
    }
}