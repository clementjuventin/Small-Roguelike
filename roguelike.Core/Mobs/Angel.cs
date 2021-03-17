using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.Mobs
{
    public class Angel : MobEntity
    {
        public Angel(Game game, SpriteBatch spriteBatch, Entity target , int level): base(game, spriteBatch, target, 1, followDistance:20f) {
            HealthPoints = 1;
            Damages = 1;
            Vitality = 1;
            Dexterity = 1;
            Armor = 1;
            CriticalChance = 0.02f;
            Speed = 4;

        }
        public Angel(Game game, SpriteBatch spriteBatch) : this(game, spriteBatch, null,1) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("mobSprite/angel"),8,frameSpeed:0.1f) }
            };
        }


        public override void Move()
        {
            if (Target == null) return;

            Vector2 targetPosition = new Vector2(Target.HitBox.Center.X, Target.HitBox.Center.Y);
            Distance = targetPosition - new Vector2(HitBox.Center.X, HitBox.Center.Y);
            Rotation = (float)Math.Atan2(Distance.Y, Distance.X);

            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            float currentDistance = Vector2.Distance(targetPosition, Position);

            if (currentDistance <= 50)
            {
                float t = MathHelper.Min((float)Math.Abs(currentDistance), Speed);

                Follow(t);
            }
            else
            {
                float t = MathHelper.Min((float)Math.Abs(currentDistance), Speed);

                Follow(-t);
            }

            //else velocity = Vector2.Zero;

        }

        public override void Follow(float t)
        {
            velocity = -Direction * t;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsOnAction == false)
            {
                IsOnAttack = false;
            }
        }

        protected override void SetAnimations()
        {
            base.SetAnimations();
            EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
        }
    }
}
