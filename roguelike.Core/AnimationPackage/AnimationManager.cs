using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.AnimationPackage
{
    public class AnimationManager
    {
        private Animation _animation;
        private float Scale { get; set; }
        private float _timer;

        public Vector2 Position { get; set; }
        public AnimationManager(Animation animation) : this(animation, 1f) { }
        public AnimationManager(Animation animation, float scale)
        {
            _animation = animation;
            Scale = scale;
        }
        public void Draw(SpriteBatch spriteBatch, SpriteEffects spriteEffects)
        {
            spriteBatch.Draw(
                _animation.Texture,
                Position,
                new Rectangle(
                    _animation.CurrentFrame * _animation.FrameWidth,
                    0,
                    _animation.FrameWidth,
                    _animation.FrameHeight),
                Color.White,
                0f,
                Vector2.Zero,
                Scale,
                spriteEffects,
                0f
                );
        }
        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;
            _animation = animation;
            _animation.CurrentFrame = 0;

            _timer = 0f;
        }
        public void Stop()
        {
            _timer = 0f;
            _animation.CurrentFrame = 0;
        }
        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;
                if (_animation.CurrentFrame >= _animation.FrameCount)
                    if (_animation.IsLopping)
                        _animation.CurrentFrame = 0;
                    else throw new InvalidOperationException("The animation doesn't loop");

            }
        }
    }
}
