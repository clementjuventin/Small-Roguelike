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

            Weapon.Sprite.AnimationManager.Position = new Vector2(Position.X + (IsOnRight?1:-1)*EntitySprite.SpriteWidth, Position.Y - EntitySprite.SpriteHeight/6);
            Weapon.IsOnRight = !IsOnRight;

            if (!IsOnRight)
            {
                Weapon.Sprite.AnimationManager.RotationOrigin = new Vector2(-EntitySprite.SpriteWidth*2, EntitySprite.SpriteWidth);
                Weapon.Sprite.AnimationManager.Angle -= 0.05f;
            }
            else
            {
                Weapon.Sprite.AnimationManager.RotationOrigin = new Vector2(0f, EntitySprite.SpriteWidth);
                Weapon.Sprite.AnimationManager.Angle += 0.05f;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Weapon.Draw(gameTime);
        }
        public override void Move()
        {
            base.Move();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) { }
                
        }
    }
}
