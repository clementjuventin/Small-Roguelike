using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core
{
    class Player : DrawableGameComponent
    {
        public Texture2D Texture { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Player(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            LoadContent();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            Texture = Game.Content.Load<Texture2D>("playerSprite/Idle");
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
        }
    }
}
