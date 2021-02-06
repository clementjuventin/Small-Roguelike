using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core
{
    class PlayerEntity : Entity
    {
        public PlayerEntity(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch, 5f) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("playerSprite/Idle"),11) },
                {"RunRight", new Animation(Game.Content.Load<Texture2D>("playerSprite/RunRight"),8) },
                {"Attack1", new Animation(Game.Content.Load<Texture2D>("playerSprite/Attack1"),7, false) }
            };
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
            if (IsOnAttack)
            {
                EntitySprite.AnimationManager.Play(EntitySprite.Animation["Attack1"]);
                return;
            }
            if (Velocity.X > 0)
            {
                IsOnRight = true;
                EntitySprite.AnimationManager.Play(EntitySprite.Animation["RunRight"]);
            }
            else if (Velocity.X < 0)
            {
                IsOnRight = false;
                EntitySprite.AnimationManager.Play(EntitySprite.Animation["RunRight"]);
            }
            else
            {
                if (Velocity.Y == 0)
                    EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
                else
                    EntitySprite.AnimationManager.Play(EntitySprite.Animation["RunRight"]);
            }
        }

        public override void Move()
        {
            if (IsOnAction) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                velocity.Y += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                velocity.Y += Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X += Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                IsOnAttack = true;
                IsOnAction = true;
            }
        }
    }
}
