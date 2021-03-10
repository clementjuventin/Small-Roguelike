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
        public List<MobEntity> Mobs { get; set; }

        private Texture2D tileset;

        private int tileWidth;
        private int tileHeight;

        private int tilesetTilesWide;
        private int tilesetTilesHigh;

        Rectangle bounds;
        public SpriteBatch SpriteBatch { get; set; }
        public RoomType RoomType { get; set; }
        public Vector2 Position { get; set; }
        public List<Room> Neighbour { get; set; }
        public Rectangle[] Doors { get; set; } = new Rectangle[4];
        public Room(Game game, SpriteBatch spriteBatch, RoomType type, Vector2 position) : base(game)
        {
            SpriteBatch = spriteBatch;
            bounds = new Rectangle(0, 0, 0, 0);

            RoomType = type;
            Position = position;
            Neighbour = new List<Room>();
            Mobs = new List<MobEntity>();

            LoadContent();
            BuildRoom();
        }
        public void AddNeighbour(Room neighbour)
        {
            if (Neighbour.Contains(neighbour)) return;
            Neighbour.Add(neighbour);
        }
        private void BuildRoom()
        {
            Mobs.Add(new MediumDemon(Game, SpriteBatch, Player));
            Mobs.Add(new MediumSkeleton(Game, SpriteBatch, Player));

            Random r = new Random();
            foreach(Entity ett in Mobs)
            {
                ett.Position = new Vector2(r.Next(-tileWidth*10*16, tileWidth*10*16), r.Next(-tileWidth * 10 * 16, tileWidth * 10 * 16));
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (MobEntity entity in Mobs)
            {
                foreach (MobEntity entity2 in Mobs)
                {
                    entity2.CollisionHandler(entity.HitBox);
                }
            }

            foreach (MobEntity entity in new List<MobEntity>(Mobs))
            {
                entity.Update(gameTime);
                if (Player.IsHitting())
                {
                    entity.HitHandler(Player.WeaponHitBox, Player.GetDamages());
                }

                if (entity.HealthPoints<=0)
                {
                    Mobs.Remove(entity);
                }

                if (entity.IsHitting())
                {
                    Player.HitHandler(entity.AttaqueHitBox, entity.Damages);
                }
                         
                if (Player.HealthPoints <= 0)
                {
                    Mobs.Remove(entity);
                }


            }


            List<Entity> allEntities = new List<Entity>(Mobs);

           
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
