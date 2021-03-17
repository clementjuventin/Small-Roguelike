using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.MapPackage
{
    public class Door : DrawableGameComponent
    {
        public Texture2D Texture { get; set; }
        public Rectangle HitBox { get; set; }
        public Rectangle TexturePlace { get; set; }
        public Direction Dir { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public Door(Game game, SpriteBatch sp, Direction dir, int tileHeight, int tileWidth):base(game)
        {
            Dir = dir;
            SpriteBatch = sp;
            LoadContent();
            switch (Dir)
            {
                case Direction.Top:
                    HitBox = new Rectangle(-16, tileHeight * 8 - 16, 32, 16);
                    TexturePlace = new Rectangle(-16, tileHeight * 8 - 16, 32, 16);
                    break;
                case Direction.Bot:
                    HitBox = new Rectangle(-16, -tileHeight * 8, 32, 16);
                    TexturePlace = new Rectangle(-16, -tileHeight * 10, 32, 32);
                    break;
                case Direction.Left:
                    HitBox = new Rectangle(tileWidth * 8 - 16, -16, 16, 32);
                    TexturePlace = new Rectangle(tileWidth * 8 - 16, -16, 16, 32);
                    break;
                case Direction.Right:
                    HitBox = new Rectangle(-tileWidth * 8, -16, 16, 32);
                    TexturePlace = new Rectangle(-tileWidth * 8, -16, 16, 32);
                    break;
                default:
                    break;
            }
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            switch (Dir)
            {
                case Direction.Bot:
                    Texture = Game.Content.Load<Texture2D>("doors/doorTopClose");
                    break;
                case Direction.Top:
                    Texture = Game.Content.Load<Texture2D>("doors/doorBot");
                    break;
                case Direction.Left:
                    Texture = Game.Content.Load<Texture2D>("doors/doorRight");
                    break;
                case Direction.Right:
                    Texture = Game.Content.Load<Texture2D>("doors/doorLeft");
                    break;
                default:
                    break;
            }
        }
        public void Open()
        {
            if(Dir == Direction.Bot) Texture = Game.Content.Load<Texture2D>("doors/doorTopOpen");
        }
        public override void Draw(GameTime gameTime)
        {
                SpriteBatch.Draw(Texture, TexturePlace, Color.White);
        }
    }
}
