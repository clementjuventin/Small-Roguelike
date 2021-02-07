using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    public class MobEntity : Entity
    {
        public Entity Target { get; set; }
        public Vector2 Distance { get; set; }
        public float Rotation { get; set; }
        public Vector2 Direction {get;set;}

        public MobEntity(Game game, SpriteBatch spriteBatch, float scale = 1, float speed = 5) : this(game, spriteBatch, null, scale, speed) { }
        public MobEntity(Game game, SpriteBatch spriteBatch, Entity target, float scale = 1, float speed = 2) : base(game, spriteBatch, scale, speed)
        {
            Target = target;
        }

        public override void Move()
        {
            if (Target == null) return;
            Vector2 targetPosition = new Vector2(Target.Position.X + Target.EntitySprite.SpriteWidth / 2, Target.Position.Y + Target.EntitySprite.SpriteHeight / 2);
            Distance = targetPosition - new Vector2(Position.X + EntitySprite.SpriteWidth / 2, Position.Y + EntitySprite.SpriteHeight / 2);
            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            float currentDistance = Vector2.Distance(Target.Position, Position);

            var t = MathHelper.Min((float)Math.Abs(currentDistance), Speed);

            velocity = Direction * t;

            Follow();
        }
        public void Follow() { }
    }
}
