using cse3902.Interfaces;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.SpriteFactory;
using cse3902.Sprites.EnemySprites;
using Microsoft.Xna.Framework;
using System;
using cse3902.Constants;
using cse3902.Sounds;
using cse3902.ParticleSystem;

namespace cse3902.Entities.Enemies
{
    public class BoggusBoss : IEntity
    {
        private BoggusBossSprite boggusBossSprite;
        private BoggusBossStateMachine boggusBossStateMachine;
        private readonly Game1 game;

        private Vector2 direction;
        private (Vector2 previous, Vector2 current) center;
        private int travelDistance;
        private Vector2 shoveDirection;
        private int shoveDistance;
        private Boolean pauseAnim;

        private ICollidable collidable;
        private int health;
        private float remainingDamageDelay;

        public BoggusBoss(Game1 game, Vector2 start)
        {
            this.game = game;
            center.current = start;
            center.previous = center.current;

            boggusBossSprite = (BoggusBossSprite)EnemySpriteFactory.Instance.CreateBoggusBossSprite(game.SpriteBatch, center.current);
            boggusBossStateMachine = new BoggusBossStateMachine(boggusBossSprite, game.SpriteBatch, this.center.current, game.Player);
            direction = new Vector2(1, 0);
            travelDistance = (int)(MovementConstants.BoggusMaxTravel);
            shoveDistance = MovementConstants.BoggusShoveDistance;
            shoveDirection = new Vector2(1, 0);
            pauseAnim = false;
            remainingDamageDelay = DamageConstants.DamageDisableDelay;

            this.collidable = new EnemyCollidable(this, this.Damage);
            health = SettingsValues.Instance.GetValue(SettingsValues.Variable.BoggusHealth);
        }

        public ref Rectangle Bounds
        {
            get => ref boggusBossSprite.Box;
        }

        public void Attack()
        {
            this.boggusBossStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = -this.direction;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health > 0)
            {
                SoundFactory.PlaySound(SoundFactory.Instance.bossHurt, MovementConstants.BoggusDelay);
            }
            this.boggusBossSprite.Damaged = true;
            this.collidable.DamageDisabled = true;
        }

        public void Die()
        {
            SoundFactory.PlaySound(SoundFactory.Instance.bossDefeat, MovementConstants.BoggusDelay);
            this.boggusBossStateMachine.Die();
            ItemSpriteFactory.Instance.SpawnRandomItem(game.SpriteBatch, center.current, IEntity.EnemyType.A);
            if (ParticleEngine.Instance.UseParticleEffects) ParticleEngine.Instance.CreateEnemyDeathEffect(center.current);
        }

        public void BeShoved()
        {
            this.shoveDistance = MovementConstants.BoggusShoveDistance;
            this.pauseAnim = true;
        }

        public void StopShove()
        {
            this.shoveDistance = 0;
        }

        private void UpdateDamage(GameTime gameTime)
        {
            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (collidable.DamageDisabled)
            {
                remainingDamageDelay -= timer;
                if (remainingDamageDelay < 0)
                {
                    remainingDamageDelay = DamageConstants.DamageDisableDelay;
                    collidable.DamageDisabled = false;
                    boggusBossSprite.Damaged = false;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamage(gameTime);
            if (this.shoveDistance > 0) ShoveMovement();
            else RegularMovement(gameTime);
            this.collidable.ResetCollisions();

            boggusBossStateMachine.Update(gameTime, this.Center, this.pauseAnim);
        }

        private void ShoveMovement()
        {
            this.Center += shoveDirection;
            shoveDistance--;
        }

        private void RegularMovement(GameTime gameTime)
        {
            pauseAnim = false;

            this.Center += direction * MovementConstants.BoggusSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (travelDistance <= 0)
            {
                direction.X *= -1;
                travelDistance = MovementConstants.BoggusMaxTravel;
            }
            else
            {
                travelDistance--;
            }

        }

        public void Draw()
        {
            boggusBossStateMachine.Draw();
        }

        public IEntity Duplicate()
        {
            return new BoggusBoss(game, center.current);
        }

        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.A;
        }

        public Vector2 Center
        {
            get => this.center.current;
            set
            {
                this.PreviousCenter = this.center.current;
                this.center.current = value;
                boggusBossSprite.Center = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => this.center.previous;
            set
            {
                this.center.previous = value;
            }
        }

        public int Damage
        {
            get => SettingsValues.Instance.GetValue(SettingsValues.Variable.BoggusDamage);
        }

        public int Health
        {
            get => this.health;
            set
            {
                this.health = value;
            }
        }

        public Vector2 Direction
        {
            get => this.direction;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public (bool, float) Stunned
        {
            get => (false, 0);
            set { }
        }
    }
}
