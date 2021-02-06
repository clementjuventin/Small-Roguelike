using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    public class EntitySprite
    {
        public AnimationManager AnimationManager { get; set; } 
        public Dictionary<string, Animation> Animation { get; set; }
        public Texture2D Texture { get; set; }
        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }

        public void SetSpriteSize()
        {
            if(Animation != null && Animation.First().Value != null)
            {
                SpriteHeight = Animation.First().Value.FrameHeight;
                SpriteWidth = Animation.First().Value.FrameWidth;
            }
        }
    }
}
