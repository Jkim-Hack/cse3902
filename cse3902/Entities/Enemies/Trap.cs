using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites.EnemySprites;
using cse3902.SpriteFactory;
using cse3902.Collision;
using cse3902.Collision.Collidables;

namespace cse3902.Entities.Enemies
{
    public class Trap: IEntity
    {
        private TrapSprite trapSprite;
        private readonly Game1 game;

        private Vector2 direction;
        private float speed;
        private Vector2 startingPos;
        private Vector2 center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;

        private ICollidable collidable;
        private int health;

        public Trap(Game1 game, Vector2 start)
        {
            this.game = game;
            startingPos = start;
            center = startingPos;

            //gel sprite sheet is 1 row, 2 columns
            trapSprite = (TrapSprite)EnemySpriteFactory.Instance.CreateGelSprite(game.spriteBatch, startingPos);
            direction = new Vector2(-1, 0);
            speed = 50.0f;
            travelDistance = 50;
            shoveDistance = -10;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = 2;
        }

        public int Damage
        {
            get => 3;
        }
    }
}
