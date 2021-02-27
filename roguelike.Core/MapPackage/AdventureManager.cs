using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.MapPackage
{
    class AdventureManager
    {
        public Map CurrentMap { get; set; }
        public Room CurrentRoom { get; set; }

        public AdventureManager(Game game, SpriteBatch spriteBatch)
        {
            CurrentMap = new Map(game, spriteBatch);
            CurrentRoom = CurrentMap.ModelBuilder.Entry;
        }
    }
}
