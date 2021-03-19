using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.EntityPackage;
using roguelike.Core.Mobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public Boolean RoomDone { get; set; }
        public SpriteBatch SpriteBatch { get; set; }
        public RoomType RoomType { get; set; }
        public Vector2 Position { get; set; }
        public Dictionary<Room, Door> DoorRoom { get; set; }
        public AdventureManager AV { get; set; }

        public Texture2D SimpleDoor { get; set; }
        public Texture2D TopDoorClose { get; set; }
        public Texture2D TopDoorOpen { get; set; }

        public Texture2D TextureMap { get; set; }
        public Room(Game game, SpriteBatch spriteBatch, RoomType type, Vector2 position, AdventureManager av) : base(game)
        {
            AV = av;
            SpriteBatch = spriteBatch;

            RoomType = type;
            Position = position;

            Mobs = new List<MobEntity>();
            DoorRoom = new Dictionary<Room, Door>();
            RoomDone = false;

            LoadContent();
            BuildRoom();

            TextureMap = new Texture2D(GraphicsDevice, 16, 16);
            UnsetCurrentRoom();
        }
        public void SetCurrentRoom()
        {
            Color[] data = new Color[16 * 16];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Gray;
            }
            TextureMap.SetData(data);
        }
        public void UnsetCurrentRoom()
        {
            Color[] data = new Color[16 * 16];
            for (int i = 0; i < data.Length; ++i)
            {
                switch (RoomType)
                {
                    case RoomType.Entry:
                        data[i] = Color.Blue;
                        break;
                    case RoomType.Outry:
                        data[i] = Color.Red;
                        break;
                    default:
                        data[i] = Color.White;
                        break;
                }
            }
            TextureMap.SetData(data);
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
        public void AddNeighbour(Room neighbour, Direction dir)
        {
            if (DoorRoom.Count >= 4) return;
            foreach (Room room in DoorRoom.Keys)
            {
                if (Vector2.Equals(room.Position, neighbour.Position)) return;
            }
            DoorRoom.Add(neighbour, new Door(Game, SpriteBatch, dir, tileHeight, tileWidth));
        }
        private void BuildRoom()
        {
            switch (RoomType)
            {
                case RoomType.Entry:
                    break;
                case RoomType.Outry:
                    break;
                case RoomType.Casual:
                    //Permet de récupérer les classes filles de MobEntity
                    IEnumerable<Type> ChildClasses = Assembly.GetAssembly(typeof(MobEntity)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(MobEntity)));
                    object[] paramArray = new object[] { Game, SpriteBatch, Player };
                    Random r = new Random();

                    int mobCount = r.Next(2, 5);
                    for (int i = 0; i < mobCount; i++)
                    {
                        Mobs.Add((MobEntity)Activator.CreateInstance(ChildClasses.ElementAt(r.Next(0, ChildClasses.Count())), args: paramArray));
                    }
                    foreach (Entity ett in Mobs)
                    {
                        ett.Position = new Vector2(r.Next(-tileWidth * 10 * 16, tileWidth * 10 * 16), r.Next(-tileWidth * 10 * 16, tileWidth * 10 * 16));
                    }
                    break;
                default:
                    break;
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(Mobs.Count == 0 && !RoomDone)
            {
                RoomDone = true;
                foreach(Door d in DoorRoom.Values)
                {
                    d.Open();
                }
            }

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

            foreach (KeyValuePair<Room, Door> kv in DoorRoom)
            {
                if (Player.CollideDoor(kv.Value.HitBox) && RoomDone)
                {
                    Player.Position = Vector2.Zero;
                    AV.SetCurrentRoom(kv.Key);
                }

            }
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

            foreach (KeyValuePair<Room, Door> kv in DoorRoom)
            {
                kv.Value.Draw(gameTime);
            }
        }
    }
}
