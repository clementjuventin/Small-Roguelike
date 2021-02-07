using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.AnimationPackage
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public float FrameSpeed { get; set; }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool IsLopping { get; set; }
        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount, float frameSpeed = 0.05f)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLopping = true;
            FrameSpeed = frameSpeed;
        }
        public Animation(Texture2D texture, int frameCount, Boolean isLopping)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLopping = isLopping;
            FrameSpeed = 0.05f;
        }
    }
}
