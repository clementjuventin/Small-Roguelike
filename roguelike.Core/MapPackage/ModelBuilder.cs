using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core.MapPackage
{
    public class ModelBuilder
    {
        //Rayon du modèle (la taille correspondra à 2 * ray + 1 unitées)
        public int Ray { get; set; }
        public int Size { get; set; }
        public Room[,] Rooms { get; set; }
        public Random Randomizer { get; set; }

        private int _maxOutry = 1;
        private int _outryCount = 0;

        public SpriteBatch SpriteBatch { get; set; }
        public Game Game { get; set; }

        public Room Entry { get; set; }

        public AdventureManager AV { get; set; }

        public ModelBuilder(Game game, SpriteBatch spriteBatch, AdventureManager av, int ray = 20, Double propagationCoeff = 0.9999f, Double outryCoeff = 0.1f)
        {
            AV = av;
            Ray = ray;
            Size = 2 * ray + 1;
            Rooms = new Room[Size, Size];
            Randomizer = new Random();

            Game = game;
            SpriteBatch = spriteBatch;

            Entry = new Room(Game, SpriteBatch,RoomType.Entry, Vector2.Zero, AV);
            Rooms[Ray, Ray] = Entry;
            List<Vector2> neighbourPosition = new List<Vector2>()
            {
                new Vector2(0,1),
                new Vector2(1,0),
                new Vector2(0,-1),
                new Vector2(-1,0)
            };

            foreach (Vector2 neighbour in neighbourPosition)
            {
                Build(Rooms[Ray, Ray], neighbour, propagationCoeff, outryCoeff);
            }
        }
        private void Build(Room entry, Vector2 position, double propagationCoeff, double outryCoeff)
        {
            List<Vector2> availablePosition;
            Double outryChance = 1f;
            Room room;

            if (Randomizer.NextDouble() > propagationCoeff)
            {
                if (_outryCount < _maxOutry) { AppendRoom(new Room(Game, SpriteBatch, RoomType.Outry, position, AV)); _outryCount++; }
                else AppendRoom(new Room(Game, SpriteBatch, RoomType.Casual, position, AV));
                return;
            }
            room = AppendRoom(new Room(Game, SpriteBatch, RoomType.Casual, position, AV));

            availablePosition = GetAvailablePosition(room);
            foreach (Vector2 available in availablePosition)
            {
                if (Randomizer.NextDouble() <= outryChance)
                {
                    if (GetRoomFromBasePosition(available) == null) Build(room, GetBaseToCanoniquePosition(available), propagationCoeff * propagationCoeff, outryCoeff);
                    else continue;
                }
                else break;
                outryChance *= outryCoeff;
            }
        }
        private Room AppendRoom(Room room)
        {
            Vector2 roomPosition = GetCanoniqueToBasePosition(room.Position);
            Rooms[(int)roomPosition.X, (int)roomPosition.Y] = room;
            return room;
        }
        private Room GetRoomFromBasePosition(Vector2 position)
        {
            if (position.Y > Size || position.X > Size || position.Y <= 0 || position.X <= 0) return null;
            return Rooms[(int)position.X, (int)position.Y];
        }
        private List<Vector2> GetAvailablePosition(Room currentRoom)
        {
            Vector2 currentBasePosition = GetCanoniqueToBasePosition(currentRoom.Position);
            List<Vector2> availablePosition = new List<Vector2>();
            Room neighbour;
            List<Vector2> testedPosition = new List<Vector2>()
            {
                new Vector2(currentBasePosition.X, currentBasePosition.Y + 1),
                new Vector2(currentBasePosition.X, currentBasePosition.Y - 1),
                new Vector2(currentBasePosition.X + 1, currentBasePosition.Y),
                new Vector2(currentBasePosition.X - 1, currentBasePosition.Y)
            };

            foreach (Vector2 position in testedPosition)
            {
                neighbour = GetRoomFromBasePosition(position);
                if (neighbour != null)
                {
                    neighbour.AddNeighbour(currentRoom, GetDirection(neighbour, currentRoom));
                    currentRoom.AddNeighbour(neighbour, GetDirection(currentRoom, neighbour));
                    continue;
                }
                availablePosition.Add(position);
            }
            return availablePosition.OrderBy(e => Guid.NewGuid()).ToList();
        }
        private Vector2 GetCanoniqueToBasePosition(Vector2 v2)
        {
            return new Vector2(v2.X + Ray, v2.Y + Ray);
        }
        private Vector2 GetBaseToCanoniquePosition(Vector2 v2)
        {
            return new Vector2(v2.X - Ray, v2.Y - Ray);
        }

        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device)//For debug
        {
            Texture2D rect;
            Color[] data;

            foreach (Room room in Rooms)
            {
                if (room != null)
                {
                    rect = new Texture2D(device, 16, 16);
                    data = new Color[16 * 16];
                    switch (room.RoomType)
                    {
                        case RoomType.Entry:
                            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
                            break;
                        case RoomType.Outry:
                            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
                            break;
                        default:
                            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
                            break;
                    }
                    rect.SetData(data);
                    spriteBatch.Draw(rect, new Rectangle((int)room.Position.X*16,(int)room.Position.Y*16, 16,16), Color.White);
                }
            }
        }
        private Direction GetDirection(Room one, Room other)
        {
            Vector2 direction = other.Position - one.Position;
            if (direction.X == 1)
            {
                return Direction.Left;
            }
            else if(direction.X == -1)
            {
                return Direction.Right;
            }
            else if (direction.Y == 1)
            {
                return Direction.Top;
            }
            else if (direction.Y == -1)
            {
                return Direction.Bot;
            }
            throw new Exception("False direction");
        }
    }
}
