using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.MapPackage
{
    public class AdventureManager
    {
        public Map CurrentMap { get; set; }
        public Room CurrentRoom { get; set; }

        public AdventureManager(Game game, SpriteBatch spriteBatch, PlayerEntity player)
        {
            Room.Player = player;
            CurrentMap = new Map(game, spriteBatch, this);
            CurrentRoom = CurrentMap.ModelBuilder.Entry;
        }
    }
}
