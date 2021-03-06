using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    public class MobEntity : Entity,IStatistics
    {
        public Entity Target { get; set; }
        public Vector2 Distance { get; set; }
        public float Rotation { get; set; }
        public Vector2 Direction {get;set;}
        public float FollowDistance { get; set; }



        public MobEntity(Game game, SpriteBatch spriteBatch, float scale = 1, float speed = 2) : this(game, spriteBatch, null, scale, speed) { }
        public MobEntity(Game game, SpriteBatch spriteBatch, Entity target, float scale = 1, float speed = 2, float followDistance = 20f) : base(game, spriteBatch, scale, speed)
        {
            FollowDistance = followDistance;
            Target = target;
        }
        public override void Move()
        {
            if (Target == null) return;

            Vector2 targetPosition = new Vector2(Target.HitBox.Center.X, Target.HitBox.Center.Y);
            Distance = targetPosition - new Vector2(HitBox.Center.X, HitBox.Center.Y);
            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            float currentDistance = Vector2.Distance(targetPosition, Position);

            if (FollowDistance <= currentDistance)
            {
                float t = MathHelper.Min((float)Math.Abs(currentDistance), Speed);

                Follow(t);
            }
            else velocity = Vector2.Zero;
        }
        public virtual void Follow(float t)
        {
            velocity = Direction * t;
        }
    }
}
