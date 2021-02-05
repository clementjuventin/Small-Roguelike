using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    class EntitySprite : DrawableGameComponent
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;

        public int SpriteWidth { get; set; }
        public int SpriteHeight { get; set; }

        public SpriteBatch SpriteBatch { get; set; }

        public Boolean IsOnAction { get; set; }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                    _animationManager.Position = _position;
            }
        }

        public EntitySprite(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            LoadContent();

            SpriteWidth = _animations.Values.First().FrameWidth;
            SpriteHeight = _animations.Values.First().FrameHeight;
            _animationManager = new AnimationManager(_animations.First().Value);
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (_animationManager != null)
                _animationManager.Draw(SpriteBatch);
            else if (_texture != null)
                SpriteBatch.Draw(_texture, Position, Color.White);
            else throw new Exception("No texture and no animationmanager");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            try
            {
                _animationManager.Update(gameTime);
            }
            catch (InvalidOperationException)
            {
                IsOnAction = false;
            }
            //SetAnimations();
        }
    }
}
