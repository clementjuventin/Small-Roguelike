using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.Mobs
{
    class MediumSkeleton : MobEntity
    {
        public MediumSkeleton(Game game, SpriteBatch spriteBatch, Entity target) : base(game, spriteBatch, target, 1.5f, followDistance:50) { }
        public MediumSkeleton(Game game, SpriteBatch spriteBatch) : this(game, spriteBatch, null) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("mobSprite/skeleton1"),8,frameSpeed:0.1f) }
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
            base.SetAnimations();
            EntitySprite.AnimationManager.Play(EntitySprite.Animation["Idle"]);
        }
    }
}
