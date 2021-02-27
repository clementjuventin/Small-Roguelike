using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using roguelike.Core.WeaponPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core
{
    public class PlayerEntity : WeaponHolder
    {
        public PlayerEntity(Game game, SpriteBatch spriteBatch, Weapon weapon) : base(game, spriteBatch, weapon, speed:3f) 
        {
            HealthPoints = 100;
            Damages = 3;
            Vitality = 1;
            Dexterity = 5;
            Armor = 5;
            Level = 1;
            CriticalChance = 0.1f;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("playerSprite/knightPlayer"),8) }
            };
        }

        protected override void SetAnimations()
        {
            base.SetAnimations();
            EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
        }

        public override void Move()
        {
            base.Move();
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                velocity.Y += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                velocity.Y += Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X += Speed;
        }
    }
}
