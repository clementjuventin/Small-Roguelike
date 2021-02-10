using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    public class Entity : MoveableEntity
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (EntitySprite.AnimationManager != null)
                    EntitySprite.AnimationManager.Position = _position;
            }
        }
        public SpriteBatch SpriteBatch { get; set; }
        public EntitySprite EntitySprite { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, EntitySprite.SpriteWidth, EntitySprite.SpriteHeight);
            }
        }

        public void CollisionHandler(Rectangle other)
        {
            if (other == HitBox) return;
            if (HitBox.Intersects(other))
            {
                base.AddForce(0.9f, Vector2.Negate(Velocity));
            }
        }

        public Entity(Game game, SpriteBatch spriteBatch, float scale=1f, float speed=5f) : base(game, speed)
        {
            EntitySprite = new EntitySprite();
            SpriteBatch = spriteBatch;
            LoadContent();

            EntitySprite.AnimationManager = new AnimationManager(EntitySprite.Animation.First().Value, scale);
            EntitySprite.SetSpriteSize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (EntitySprite.AnimationManager != null)
                if (IsOnRight) EntitySprite.AnimationManager.Draw(SpriteBatch, SpriteEffects.None);
                else EntitySprite.AnimationManager.Draw(SpriteBatch, SpriteEffects.FlipHorizontally);
            else if (EntitySprite.Texture != null)
                SpriteBatch.Draw(EntitySprite.Texture, Position, Color.White);
            else throw new Exception("No texture and no animationmanager");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Velocity = Vector2.Zero;
            Move();

            try { EntitySprite.AnimationManager.Update(gameTime); }
            catch (InvalidOperationException) { IsOnAction = false; }

            SetAnimations();
        }

        public void UpdatePosition()
        {
            base.ApplyForce();
            Position += Velocity;
        }

        protected virtual void SetAnimations()
        {
            if (Velocity.X > 0)
            {
                IsOnRight = true;
            }
            if (Velocity.X < 0)
            {
                IsOnRight = false;
            }
        }
    }
}
