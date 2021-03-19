using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.ObjectPackage
{
    class Chest : DrawableGameComponent
    {
        public SpriteBatch SpriteBatch { get; set; }
        public Chest(Game game, SpriteBatch spriteBatch) : base(game)
        {
           
        }
    }
}
