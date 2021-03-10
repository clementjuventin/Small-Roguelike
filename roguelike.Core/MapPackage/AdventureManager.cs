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
        private Room currentRoom; 
        public Room GetCurrentRoom() { return currentRoom; }
        public void SetCurrentRoom(Room room)
        {
            if(currentRoom!= null) currentRoom.UnsetCurrentRoom();
            currentRoom = room;
            currentRoom.SetCurrentRoom();
        }

        public AdventureManager(Game game, SpriteBatch spriteBatch, PlayerEntity player)
        {
            Room.Player = player;
            CurrentMap = new Map(game, spriteBatch, this);
            SetCurrentRoom(CurrentMap.ModelBuilder.Entry);
        }
    }
}
