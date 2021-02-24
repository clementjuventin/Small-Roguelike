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
        public Rectangle WeaponHitBox { get; set; }
        public Boolean IsHitting() { return _isHitting; }
        public void SetIsHitting(Boolean isHitting) 
        {
            if (isHitting)
            {
                WeaponHitBox = new Rectangle((int) Position.X + EntitySprite.SpriteWidth/2 * (IsOnRight?1:-3), (int) Position.Y - EntitySprite.SpriteHeight, EntitySprite.SpriteWidth, EntitySprite.SpriteHeight * 2);
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
        
            
            if (IsHitting())
            {

            }
        
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Weapon.Draw(gameTime);

            Texture2D rect = new Texture2D(GraphicsDevice, 80, 30);

            Color[] data = new Color[80 * 30];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);


            SpriteBatch.Draw(rect, WeaponHitBox, Color.White);
            
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
