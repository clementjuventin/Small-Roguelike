using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using roguelike.Core.AnimationPackage;
using roguelike.Core.EntityPackage;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.Mobs
{
    public class Odds : MobEntity
    {
        public Odds(Game game, SpriteBatch spriteBatch, Entity target ): base(game, spriteBatch, target, 1, followDistance:20f) {
            HealthPoints = 50;
            Damages = 1;
            Vitality = 1;
            Dexterity = 2;
            Armor = 2;
            Level = 1;
            CriticalChance = 0.02f;

        }
        public Odds(Game game, SpriteBatch spriteBatch) : this(game, spriteBatch, null) { }

        public Boolean IsOnAttack { get; set; } = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            EntitySprite.Animation = new Dictionary<string, Animation>()
            {
                {"Idle", new Animation(Game.Content.Load<Texture2D>("mobSprite/odds"),8,frameSpeed:0.1f) }
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
