using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.EntityPackage;
using roguelike.Core.Mobs;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace roguelike.Core.MapPackage
{
    public class Room : DrawableGameComponent
    {
        private TmxMap map;

        public static PlayerEntity Player { get; set; }
        public List<Entity> Mobs { get; set; }

        private Texture2D tileset;

        private int tileWidth;
        private int tileHeight;

        private int tilesetTilesWide;
        private int tilesetTilesHigh;

        public Boolean RoomDone { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public RoomType RoomType { get; set; }
        public Vector2 Position { get; set; }
        public Dictionary<Room, Rectangle> DoorRoom { get; set; }
        public Room(Game game, SpriteBatch spriteBatch, RoomType type, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;

            RoomType = type;
            Position = position;
            DoorRoom = new Dictionary<Room, Rectangle>();

            Mobs = new List<Entity>();
            RoomDone = false;

            LoadContent();
            BuildRoom();
        }
        public void AddNeighbour(Room neighbour, Direction dir)
        {
            if (DoorRoom.ContainsKey(neighbour)) return;
            switch (dir)
            {
                case Direction.Top:
                    DoorRoom.Add(neighbour, new Rectangle(tilesetTilesHigh, tileHeight, 32, 16));
                    break;
                case Direction.Bot:
                    DoorRoom.Add(neighbour, new Rectangle(tilesetTilesHigh, tileHeight, 32, 16));
                    break;
                case Direction.Left:
                    DoorRoom.Add(neighbour, new Rectangle(tilesetTilesHigh, tileHeight, 32, 16));
                    break;
                case Direction.Right:
                    DoorRoom.Add(neighbour, new Rectangle(tilesetTilesHigh, tileHeight, 32, 16));
                    break;
                default:
                    break;
            }
        }
        private void BuildRoom()
        {
            Mobs.Add(new MediumDemon(Game, SpriteBatch, Player));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Entity entity in Mobs)
            {
                entity.Update(gameTime);
            }
            List<Entity> allEntities = new List<Entity>(Mobs);

            if (Player.IsHitting())
            {
                foreach (Entity entity in allEntities)
                {
                    entity.CollisionHandler(Player.WeaponHitBox, Player.GetDamages());
                }
            }
            allEntities.Add(Player);

            foreach (Entity entity in allEntities)
            {
                int offsetX = ((map.Width - 3) *tileWidth) / 2 - entity.EntitySprite.SpriteWidth;
                int offsetY = ((map.Height - 3) * tileHeight) / 2 - entity.EntitySprite.SpriteHeight;
                if (entity.Position.X > offsetX)
                    entity.Position = new Vector2(offsetX, entity.Position.Y);
                else if (entity.Position.X < -offsetX)
                    entity.Position = new Vector2(-offsetX, entity.Position.Y);

                if (entity.Position.Y > offsetY)
                    entity.Position = new Vector2(entity.Position.X, offsetY);
                else if (entity.Position.Y < -offsetY)
                    entity.Position = new Vector2(entity.Position.X, -offsetY);
            }
            /*
            for (int i = 0; i < Doors.Length; i++)
            {
                if (RoomDone && Player.CollideDoor(Doors[i]))
                {
                    switch (i)
                    {
                        /*
                        case 0;
                            break;
                        default:
                            break;
                       
                    }
                }

            */
            
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            map = new TmxMap("Content/room.tmx");
            tileset = Game.Content.Load<Texture2D>(map.Tilesets[0].Name.ToString());

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;
        }
        public override void Draw(GameTime gameTime)
        {
            int centerX = map.Width * tileWidth / 2;
            int centerY = map.Height * tileHeight / 2;
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    SpriteBatch.Draw(tileset,new Rectangle((int)x-centerX, (int)y-centerY, tileWidth, tileHeight), tilesetRec, Color.White);
                }
            }
            foreach (Entity entity in Mobs)
            {
                entity.Draw(gameTime);
            }
        }
    }
}
