using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace roguelike.Core.MapPackage
{
    public class Room : DrawableGameComponent
    {
        private TmxMap map;
        public TmxMap GetMap() { return map; }

        private Texture2D tileset;

        private int tileWidth;
        private int tileHeight;

        public int GetTileWidth() { return tileWidth; }
        public int GetTileHeight() { return tileHeight; }
        private int tilesetTilesWide;
        private int tilesetTilesHigh;

        Rectangle bounds;
        public SpriteBatch SpriteBatch { get; set; }
        public RoomType RoomType { get; set; }
        public Vector2 Position { get; set; }
        public List<Room> Neighbour { get; set; }
        public Room(Game game, SpriteBatch spriteBatch, RoomType type, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            bounds = new Rectangle(0, 0, 0, 0);

            RoomType = type;
            Position = position;
            Neighbour = new List<Room>();

            LoadContent();
        }
        public void AddNeighbour(Room neighbour)
        {
            if (Neighbour.Contains(neighbour)) return;
            Neighbour.Add(neighbour);
        }
        public void BuildRoom()
        {

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
        }
    }
}
