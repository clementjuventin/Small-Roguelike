using Microsoft.Xna.Framework;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.CameraPackage
{
    class Camera
    {
        private const float LERP_AMOUNT = 0.06f;
        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }

        public void Follow(Entity entity)
        {
            Position = Vector2.Lerp(Position, entity.Position, LERP_AMOUNT);

            Transform = Matrix.CreateTranslation(-(entity.EntitySprite.SpriteWidth / 2 + Position.X), -(entity.EntitySprite.SpriteHeight / 2 + Position.Y), 0);
            Transform *= Matrix.CreateTranslation(Game1.ScreenWidth/2, Game1.ScreenHeight/2, 0);
        }
    }
}
