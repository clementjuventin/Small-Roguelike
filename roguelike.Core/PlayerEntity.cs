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
        public PlayerEntity(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch, speed:3f) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("playerSprite/knightPlayer"),8) }
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
            base.SetAnimations();
            EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
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
