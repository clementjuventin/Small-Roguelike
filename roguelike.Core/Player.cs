using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using roguelike.Core.AnimationPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core
{
    class Player : DrawableGameComponent
    {
        protected AnimationManager _animationManager;

        protected Dictionary<string, Animation> _animations;

        protected Vector2 _position;

        protected Texture2D _texture;
        public SpriteBatch SpriteBatch { get; set; }

        public float Speed = 5f;

        public Vector2 Velocity;

        public Boolean IsHitting { get; set; }

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

        public Player(Game game, SpriteBatch spriteBatch) : base(game)
        {
            SpriteBatch = spriteBatch;
            LoadContent();

            _animationManager = new AnimationManager(_animations.First().Value);
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _animations = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("playerSprite/Idle"),11) },
                {"RunRight", new Animation(Game.Content.Load<Texture2D>("playerSprite/RunRight"),8) },
                {"RunLeft", new Animation(Game.Content.Load<Texture2D>("playerSprite/RunLeft"),8) },
                {"Attack1", new Animation(Game.Content.Load<Texture2D>("playerSprite/Attack1"),7, false) }
            };
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (_texture != null)
                SpriteBatch.Draw(_texture, Position, Color.White);
            else if (_animationManager != null)
                _animationManager.Draw(SpriteBatch);
            else throw new Exception("No texture and no animationmanager");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Move();

            try
            {
                _animationManager.Update(gameTime);
            }
            catch (InvalidOperationException)
            {
                IsHitting = false;
            }
            SetAnimations();

            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        public void Move()
        {
            if (IsHitting) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Velocity.Y += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Velocity.Y += Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Velocity.X += -Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Velocity.X += Speed;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                IsHitting = true;
            }
        }

        protected virtual void SetAnimations()
        {
            if (IsHitting)
            {
                _animationManager.Play(_animations["Attack1"]);
                return;
            }
            if (Velocity.X > 0)
                _animationManager.Play(_animations["RunRight"]);
            else if (Velocity.X < 0)
                _animationManager.Play(_animations["RunLeft"]);
            else
            {
                if (Velocity.Y != 0)
                    _animationManager.Play(_animations["RunRight"]);
                else _animationManager.Play(_animations["Idle"]);
            }
        }
    }
}
