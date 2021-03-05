using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace roguelike.Core.MapPackage
{
    public class Map : DrawableGameComponent
    {
        public ModelBuilder ModelBuilder { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public Map(Game game, SpriteBatch spriteBatch, AdventureManager av) : base(game)
        {
            ModelBuilder = new ModelBuilder(game, spriteBatch, av);
            SpriteBatch = spriteBatch;
        }

        public override void Draw(GameTime gameTime)
        {
            ModelBuilder.Draw(SpriteBatch, GraphicsDevice);
        }
    }
}
