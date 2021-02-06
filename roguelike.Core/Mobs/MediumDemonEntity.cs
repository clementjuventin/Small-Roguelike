using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.Mobs
{
    public class MediumDemonEntity : Entity
    {
        public MediumDemonEntity(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch, 3f) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("mobSprite/demon1"),8) }
            };
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsOnAction == false)
            {
                IsOnAttack = false;
            }
        }

        protected override void SetAnimations()
        {
            EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
        }

        public override void Move()
        {
            
        }
    }
}
