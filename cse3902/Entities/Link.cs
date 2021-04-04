﻿using System;
using cse3902.Collision;
using cse3902.Collision.Collidables;
using cse3902.Constants;
using cse3902.Entities.DamageMasks;
using cse3902.HUD;
using cse3902.Interfaces;
using cse3902.Sounds;
using cse3902.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Entities
{
    public class Link : IPlayer
    {
        private LinkSprite linkSprite;
        private LinkStateMachine linkStateMachine;
        private LinkInventory linkInventory;
        private Game1 game;

        private ICollidable collidable;

        private float remainingDamageDelay;

        public Link(Game1 game)
        {
            this.game = game;
            // TODO Add this into sprite factory
            Texture2D linkTexture = game.Content.Load<Texture2D>("Link");
            Texture2D linkDamageSequenceTexture = game.Content.Load<Texture2D>("LinkDamageSequence");
            Texture2D linkDeathTexture = game.Content.Load<Texture2D>("LinkDeath");
            DamageMaskHandler linkDamageMaskHandler = new DamageMaskHandler(linkTexture, linkDamageSequenceTexture, 1, 4, 0);
            SingleMaskHandler linkDeathMaskHandler = new SingleMaskHandler(linkTexture, linkDeathTexture);

            Vector2 centerPosition = new Vector2(50, 200);
            linkSprite = new LinkSprite(game.SpriteBatch, linkTexture, 6, 4, linkDamageMaskHandler, linkDeathMaskHandler, centerPosition);
            linkStateMachine = new LinkStateMachine(game, linkSprite, centerPosition);
            linkInventory = linkStateMachine.Inventory;

            //Link's body does no damage itself
            this.collidable = new PlayerCollidable(this, 0, game);
            remainingDamageDelay = DamageConstants.DamageDisableDelay;
        }

        public ref Rectangle Bounds
        {
            get => ref linkSprite.Box;
        }

        public void Attack()
        {
            linkStateMachine.Attack();
        }

        public void ChangeDirection(Vector2 direction)
        {
            linkStateMachine.ChangeDirection(direction);
        }

        public void Die()
        {
	        SoundFactory.PlaySound(SoundFactory.Instance.linkDie);
            GameStateManager.Instance.LinkDies(128);
            linkStateMachine.Die();
        }

        public void TakeDamage(int damage)
        {
            collidable.DamageDisabled = true;
            linkStateMachine.TakeDamage(damage);
        }

        public void BeGrabbed(IEntity enemy, float speed)
        {
            this.linkStateMachine.BeGrabbed(enemy, speed);
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
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateDamage(gameTime);
            linkStateMachine.Update(gameTime);
            collidable.ResetCollisions();
        }

        public void Draw()
        {
            linkSprite.Draw();
        }

        //  DO NOT USE - only here to prevent compile error
        public IEntity Duplicate()
        {
            return new Link(game);
        }

        //  DO NOT USE - only here to prevent compile error
        public IEntity.EnemyType Type
        {
            get => IEntity.EnemyType.X;
        }

        public Vector2 Direction
        {
            get => linkStateMachine.Direction;
        }

        public void UseItem()
        {
            linkStateMachine.UseItem();
        }

        public void AddItem(IItem item)
        {
            linkInventory.AddItemToInventory(item);
        }

        public void ChangeItem(int itemNum)
        {
            linkInventory.ChangeItem((InventoryManager.ItemType) itemNum);
        }

        public void ChangeWeapon(int index)
        {
            linkInventory.ChangeWeapon(index); ;
        }

        public void BeShoved()
        {
            linkStateMachine.BeShoved();
        }

        public void StopShove()
        {
        }

        public float Speed
        {
            get => linkStateMachine.Speed;
            set => linkStateMachine.Speed = value;
        }

        public int TotalHealthCount
        {
            get => linkStateMachine.TotalHealth;
        }

        public int Health
        {
            get => linkStateMachine.Health;
            set => linkStateMachine.Health = value;
        }

        public ICollidable Collidable
        {
            get => this.collidable;
        }

        public void Reset()
        {
            linkSprite.DamageMaskHandler.Reset();
            linkStateMachine.Health = linkStateMachine.TotalHealth;
        }

        public Vector2 Center
        {
            get => this.linkStateMachine.CenterPosition;
            set
            {
                this.PreviousCenter = this.linkStateMachine.CenterPosition;
                this.linkStateMachine.CenterPosition = value;
            }
        }

        public Vector2 PreviousCenter
        {
            get => linkSprite.PreviousCenter;
            set => this.linkSprite.PreviousCenter = value;
        }

      

    }
}