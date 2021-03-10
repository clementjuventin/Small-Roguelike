using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.CameraPackage;
using roguelike.Core.EntityPackage;
using roguelike.Core.MapPackage;
using roguelike.Core.Mobs;
using roguelike.Core.WeaponPackage;
using System;
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

        private Camera camera;

        AdventureManager AV { get; set; }

        public DrawMode DrawMode { get; set; }

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
            AV = new AdventureManager(this, _spriteBatch, Player);

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

            AV.GetCurrentRoom().Update(gameTime);

            Player.Update(gameTime);

            camera.Follow(Player);

            SetDrawMode();

            base.Update(gameTime);
        }

        private void SetDrawMode()
        {
            DrawMode = DrawMode.Game;
            if (Keyboard.GetState().IsKeyDown(Keys.M))
                DrawMode = DrawMode.Map;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            base.Draw(gameTime);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

            switch (DrawMode)
            {
                case DrawMode.Game:
                    AV.GetCurrentRoom().Draw(gameTime);
                    Player.Draw(gameTime);
                    break;
                case DrawMode.Map:
                    AV.CurrentMap.Draw(gameTime);
                    break;
                default:
                    break;
            }
            _spriteBatch.End();
        }
    }

    public enum DrawMode
    {
        Game,
        Map
    }
}
