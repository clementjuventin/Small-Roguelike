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
        public float WeaponAngle { get; set; } = 0;
        private Boolean _isHitting;
        public Boolean IsHitting() { return _isHitting; }
        public void SetIsHitting(Boolean isHitting) 
        {
            if (isHitting)
            {
                WeaponAngle = 0.3f * (IsOnRight? -1:1);
                Weapon.Sprite.AnimationManager.Angle = 0.3f * (IsOnRight ? -1 : 1);
            }
            else
            {
                WeaponAngle = 0;
                Weapon.Sprite.AnimationManager.Angle = 0;
            }
            _isHitting = isHitting;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Weapon.IsOnRight = !IsOnRight;

            float xOffset;
            float yOffset = Position.Y - EntitySprite.SpriteHeight / 6;
            float len = EntitySprite.SpriteWidth * 3;

            float relativeAngle = 0;

            if (!IsOnRight)
            {
                xOffset = Position.X + EntitySprite.SpriteWidth * 2;
                if (IsHitting())
                {
                    Weapon.Sprite.AnimationManager.Angle -= 0.1f * 2;
                    WeaponAngle -= 0.1f;
                }
            }
            else
            {
                xOffset = Position.X - EntitySprite.SpriteWidth * 2;
                relativeAngle = (float) Math.PI;
                if (IsHitting())
                {
                    Weapon.Sprite.AnimationManager.Angle += 0.1f * 2;
                    WeaponAngle += 0.1f;
                }
            }
            if((WeaponAngle <(float) - Math.PI/5 && !IsOnRight) || (WeaponAngle > (float) Math.PI / 5 && IsOnRight))
            {
                SetIsHitting(false);
            }
            Weapon.Sprite.AnimationManager.Position = new Vector2(xOffset - (float)Math.Cos(WeaponAngle + relativeAngle) * len, yOffset - (float)Math.Sin(WeaponAngle + relativeAngle) * len);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Weapon.Draw(gameTime);
        }
        public override void Move()
        {
            base.Move();
            if (IsHitting()) return;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) 
            {
                SetIsHitting(true);
            }
                
        }
    }
}
