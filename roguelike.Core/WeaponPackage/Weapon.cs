using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core.WeaponPackage
{
    public class Weapon : DrawableGameComponent,IStatisticsWeapon
    {
        public EntitySprite Sprite { get; set; } = new EntitySprite();
        public SpriteBatch SpriteBatch { get; set; }
        public Boolean IsOnRight { get; set; }
        public int Damages { get; set; }
        public Weapon(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;

            LoadContent();

            Sprite.AnimationManager = new AnimationManager(Sprite.Animation.First().Value, 0.75f);
            Sprite.SetSpriteSize();
        }

        public override void Draw(GameTime gameTime)
        {
            SetAnimation();
            base.Draw(gameTime);
            if (Sprite.AnimationManager != null)
                Sprite.AnimationManager.Draw(SpriteBatch, SpriteEffects.None);
            else throw new Exception("No texture and no animationmanager");
        }

        public void SetAnimation()
        {
            if (IsOnRight)
                Sprite.AnimationManager.Play(Sprite.Animation["Right"]);
            else Sprite.AnimationManager.Play(Sprite.Animation["Left"]);
        }
    }
}
