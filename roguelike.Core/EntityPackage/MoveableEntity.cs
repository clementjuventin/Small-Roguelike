using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace roguelike.Core.EntityPackage
{
    class MoveableEntity : DrawableGameComponent, IStatistics
    {
        protected Vector2 velocity;
        public float Speed { get; set; }
        public Boolean IsOnRight { get; set; }
        public Boolean IsOnAction { get; set; }

        public int HealthPoints { get; set; }
        public int Damages { get; set; }
        public int Vitality { get; set; }
        public int Dexterity { get; set; }
        public int Level { get; set; }
        public int Armor { get; set; }
        public float CriticalChange { get; set; }
        protected Vector2 Velocity { get => velocity; set => velocity = value; }

        public MoveableEntity(Game game) : base(game)
        {
        }
        public MoveableEntity(Game game, float speed) : base(game)
        {
            Speed = speed;
        }

        public virtual void Move() { }
    }
}
