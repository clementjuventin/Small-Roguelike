using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.CameraPackage;
using roguelike.Core.EntityPackage;
using roguelike.Core.MapPackage;
using roguelike.Core.Mobs;
using roguelike.Core.WeaponPackage;
using System.Collections.Generic;
using System.Linq;

namespace roguelike.Core
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int ScreenHeight;
        public static int ScreenWidth;

        private PlayerEntity Player { get; set; }

        private List<Entity> Mobs { get; set; } = new List<Entity>();

        private Camera camera;

        AdventureManager AV { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

            base.Initialize();

            Player = new PlayerEntity(this, _spriteBatch, new FireSword(this, _spriteBatch));
            AV = new AdventureManager(this, _spriteBatch);

            Mobs.Add(new MediumDemon(this, _spriteBatch, Player));
            //Mobs.Add(new MediumSkeleton(this, _spriteBatch, Player));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            camera = new Camera();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (Entity entity in Mobs)
            {
                entity.Update(gameTime);
            }
            Player.Update(gameTime);

            List<Entity> allEntities = new List<Entity>(Mobs);

            if(Player.IsHitting())
            {
                foreach (Entity entity in allEntities)
                {
                    entity.CollisionHandler(Player.WeaponHitBox,Player.GetDamages());
                }
            }
            allEntities.Add(Player);
            /*
            allEntities.Add(Player);

            foreach (Entity entity in allEntities)
            {
                foreach (Entity other in allEntities)
                {
                    entity.CollisionHandler(other.HitBox);
                }
                entity.UpdatePosition();
            }
            */
            foreach (Entity entity in allEntities)
            {
                int offsetX = ((AV.CurrentRoom.GetMap().Width -3)* AV.CurrentRoom.GetTileWidth() ) / 2 - entity.EntitySprite.SpriteWidth;
                int offsetY = ((AV.CurrentRoom.GetMap().Height -3)* AV.CurrentRoom.GetTileHeight() ) / 2 - entity.EntitySprite.SpriteHeight;
                if (entity.Position.X > offsetX)
                    entity.Position = new Vector2(offsetX, entity.Position.Y);
                else if (entity.Position.X < -offsetX)
                    entity.Position= new Vector2(-offsetX, entity.Position.Y);

                if (entity.Position.Y > offsetY)
                    entity.Position = new Vector2(entity.Position.X, offsetY);
                else if (entity.Position.Y < -offsetY)
                    entity.Position = new Vector2(entity.Position.X, -offsetY);
            }


            camera.Follow(Player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            AV.CurrentRoom.Draw(gameTime);

            foreach (Entity entity in Mobs)
            {
                entity.Draw(gameTime);
            }
            Player.Draw(gameTime);

            //model.Draw(_spriteBatch, GraphicsDevice);
            _spriteBatch.End();
        }
    }
}
