using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.WeaponPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    class WeaponHolder : Entity
    {
        public Weapon Weapon { get; set; }
        public Boolean IsOnAttack { get; set; }
        public WeaponHolder(Game game, SpriteBatch spriteBatch, Weapon weapon ,float scale = 1, float speed = 5) : base(game, spriteBatch, scale, speed)
        {
            Weapon = weapon;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Weapon.Sprite.AnimationManager.Position = new Vector2(Position.X + Weapon.Sprite.SpriteWidth/2*(IsOnRight?1:-1), Position.Y - EntitySprite.SpriteHeight / 4f);
            Weapon.IsOnRight = !IsOnRight;

            Weapon.Sprite.AnimationManager.Angle += 0.2f;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Weapon.Draw(gameTime);
        }
        public override void Move()
        {
            if (IsOnAction) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { }
                //velocity.Y += -Speed;
        }
    }
}
