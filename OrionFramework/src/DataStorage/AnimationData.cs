using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using OrionFramework.Animation;
using OrionFramework.AssetManagement;

namespace OrionFramework.DataStorage;

public struct AnimationData : IDataModel
{
    public Texture2D Texture;
    public FrameData[] Frames;
    public Dictionary<int, string>? KeyFrames;

    [JsonConstructor]
    public AnimationData(string textureName, int[] frames, int ox, int oy, Dictionary<int, string>? keyFrames)
    {
        Texture = AssetManager.LoadAsset<Texture2D>(textureName);
        KeyFrames = keyFrames;
        Frames = new FrameData[frames.Length];

        int width = Texture.Width / frames.Length;

        for (var i = 0; i < frames.Length; i++)
        {
            Frames[i].X = i * width;
            Frames[i].Y = 0;
            Frames[i].W = width;
            Frames[i].H = Texture.Height;
            Frames[i].Ox += ox;
            Frames[i].Oy += oy;
            Frames[i].Duration = frames[i];
        }
    }
}