using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace roguelike.Core.MapPackage
{
    class Map
    {
        public ModelBuilder ModelBuilder { get; set; }
        public Map(Game game, SpriteBatch spriteBatch)
        {
            ModelBuilder = new ModelBuilder(game, spriteBatch);
        }
    }
}
