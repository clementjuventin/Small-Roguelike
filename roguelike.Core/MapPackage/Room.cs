using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.MapPackage
{
    public class Room
    {
        public const int ROOM_SIZE = 37;
        public RoomType RoomType { get; set; }
        public Vector2 Position { get; set; }
        public List<Room> Neighbour { get; set; }
        public Texture2D[,] TileSet { get; set; }
        public Room(RoomType type, Vector2 position)
        {
            RoomType = type;
            Position = position;
            Neighbour = new List<Room>();
            TileSet = new Texture2D[ROOM_SIZE, ROOM_SIZE];
        }
        public void AddNeighbour(Room neighbour)
        {
            if (Neighbour.Contains(neighbour)) return;
            Neighbour.Add(neighbour);
        }
        public void BuildRoom()
        {

        }
    }
}
