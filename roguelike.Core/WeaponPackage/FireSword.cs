using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.WeaponPackage
{
    class FireSword : Weapon
    {
        public FireSword(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch)
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Sprite.Animation = new Dictionary<string, Animation>()
            {
                { "Right", new Animation(Game.Content.Load<Texture2D>("swords/sword1Right"),1) },
                { "Left", new Animation(Game.Content.Load<Texture2D>("swords/sword1Left"),1) }
            };
        }
    }
}
