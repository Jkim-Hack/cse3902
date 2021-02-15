using System;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using cse3902.Sprites;
using System.Collections.Generic;

namespace cse3902.Entities {

    public class LinkStateMachine : IEntityStateMachine {

        private enum LinkWeapon { WoodenSword, WhiteSword, MagicalSword, MagicalRod };
        private LinkWeapon currentWeapon;
        private enum LinkDirection { Up, Down, Left, Right };
        private enum LinkMode { Still, Moving, Attack, Item };
        private LinkDirection direction;
        private LinkMode mode;
        private int health;
        // TODO private LinkSword weapon;

        private LinkSprite linkSprite;
        private float speed;
        private Vector2 centerPosition;
        private Vector2 vDirection;
        private float remainingDelay;
        private const float delay = 0.2f;
        private int animationStage;
        // TODO replace with Iprojectile or IItem once merged
        private List<Object> items;

        public LinkStateMachine(LinkSprite linkSprite, Vector2 centerPosition) {

            currentWeapon = LinkWeapon.WoodenSword;
            mode = LinkMode.Still;
            direction = LinkDirection.Right;
            vDirection = new Vector2(0, 0);
            speed = 1.0f;
            this.linkSprite = linkSprite;
            health = 10;
            remainingDelay = 0;
            animationStage = -1;

            //TODO weapon = null;
        }

        public void ChangeDirection(Vector2 newDirection)
        {
            /* No need to update sprite if currently attacking */
            if (mode == LinkMode.Attack) return;
            /* No need to update sprite if currently grabbing Item */
            if (mode == LinkMode.Item) return;

            vDirection = newDirection;
            if(newDirection.X == 0 && newDirection.Y == 0)
            {
                linkSprite.SpriteLock = true;
            }
            else
            {
                linkSprite.SpriteLock = false;
            }
            if (newDirection.X < 0)
            {
                this.FaceLeft();
            }
            else if (newDirection.X > 0)
            {
                this.FaceRight();
            }
            else if (newDirection.Y > 0)
            {
                this.FaceUp();
            }
            else if (newDirection.Y < 0)
            {
                this.FaceDown();
            }
        }

        private void FaceUp(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Up && mode == LinkMode.Moving) return;

            direction = LinkDirection.Up;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.UpFacing;
        }

        private void FaceDown(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Down && mode == LinkMode.Moving) return;

            direction = LinkDirection.Down;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.DownFacing;
        }

        private void FaceLeft(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Left && mode == LinkMode.Moving) return;

            direction = LinkDirection.Left;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.LeftFacing;
        }

        private void FaceRight(){

            /* No need to update sprite if current direction is already picked */
            if (direction == LinkDirection.Right && mode == LinkMode.Moving) return;

            direction = LinkDirection.Right;
            linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.RightFacing;
        }
        public void Update(GameTime gameTime)
        {
            if(mode == LinkMode.Attack)
            {
                remainingDelay -= (float) gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingDelay <= 0)
                {
                    if (animationStage <= 3)
                    {
                        //TODO weapon.erase
                        mode = LinkMode.Still;
                        animationStage = -1;
                        switch (direction)
                        {
                            case LinkDirection.Right:
                                FaceRight();
                                break;
                            case LinkDirection.Left:
                                FaceLeft();
                                break;
                            case LinkDirection.Up:
                                FaceUp();
                                break;
                            case LinkDirection.Down:
                                FaceDown();
                                break;
                        }
                    }
                    else
                    {
                        remainingDelay = delay;
                        // TODO weapon.update(animationStage);
                        if (animationStage == 2)
                        {
                            switch (direction)
                            {
                                case LinkDirection.Right:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.RightFacing;
                                    break;
                                case LinkDirection.Left:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.LeftFacing;
                                    break;
                                case LinkDirection.Up:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.UpFacing;
                                    linkSprite.advanceFrame();
                                    break;
                                case LinkDirection.Down:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.DownFacing;
                                    linkSprite.advanceFrame();
                                    break;
                            }
                        }
                        if (animationStage == 3)
                        {
                            linkSprite.advanceFrame();
                        }
                    }
                }
            }
            if(mode == LinkMode.Item)
            {
                remainingDelay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (remainingDelay <= 0)
                {
                    if (animationStage <= 3)
                    {
                        // TODO weapon.erase
                        mode = LinkMode.Still;
                        animationStage = -1;
                        switch (direction)
                        {
                            case LinkDirection.Right:
                                FaceRight();
                                break;
                            case LinkDirection.Left:
                                FaceLeft();
                                break;
                            case LinkDirection.Up:
                                FaceUp();
                                break;
                            case LinkDirection.Down:
                                FaceDown();
                                break;
                        }
                    }
                    else
                    {
                        remainingDelay = delay;
                        //TODO weapon.update(animationStage);
                        if (animationStage == 2)
                        {
                            switch (direction)
                            {
                                case LinkDirection.Right:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.RightFacing;
                                    break;
                                case LinkDirection.Left:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.LeftFacing;
                                    break;
                                case LinkDirection.Up:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.UpFacing;
                                    linkSprite.advanceFrame();
                                    break;
                                case LinkDirection.Down:
                                    linkSprite.StartingFrameIndex = (int)LinkSprite.FrameIndex.DownFacing;
                                    linkSprite.advanceFrame();
                                    break;
                            }
                        }
                        if (animationStage == 3)
                        {
                            linkSprite.advanceFrame();
                        }
                    }
                }
            }
            //TODO: go through items and draw them
            centerPosition += vDirection * speed * (float) gameTime.ElapsedGameTime.TotalSeconds;
            linkSprite.Update(gameTime);

        }

        public void Draw()
        {
            //TODO && weapon != null
            if(mode == LinkMode.Attack)
            {
                //TODO Draw Weapon
            }
            if(items.Count > 0)
            {
                //TODO Draw projectiles
            }
            linkSprite.Draw();
        }

        public void Attack()
        {
            if(mode == LinkMode.Moving || mode == LinkMode.Still)
            remainingDelay = delay;
            animationStage = 0;
            vDirection = new Vector2(0, 0);
            mode = LinkMode.Attack;
            Vector2 delta = vDirection;
            linkSprite.SpriteLock = true;
            switch (direction)
            {
                case LinkDirection.Right:
                    vDirection = new Vector2(0, 1);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Left:
                    vDirection = new Vector2(0, -1);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Up:
                    vDirection = new Vector2(1, 0);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Down:
                    vDirection = new Vector2(-1, 0);
                    delta = new Vector2(-20, 18);
                    break;
            }
            Vector2 weaponLoc = Vector2.Add(delta, centerPosition);
            switch (currentWeapon)
            {
                case LinkWeapon.WoodenSword:
                    //weapon = new WoodenSword()..
                    break;
                case LinkWeapon.WhiteSword:

                    break;
                case LinkWeapon.MagicalSword:

                    break;
                case LinkWeapon.MagicalRod:

                    break;
            }
        }

        //Direction or weapon enum number??
        public void CycleWeapon(int dir){

            //TODO 
            //currentWeapon = (currentWeapon + dir + 3) % 3;
            //UpdateSprite();
        }

        public void UseItem(int item)
        {
            mode = LinkMode.Item;
            animationStage = 0;
            remainingDelay = delay;
            vDirection = new Vector2(0, 0);
            Vector2 itemDirection = vDirection;
            Vector2 delta = vDirection;
            linkSprite.SpriteLock = true;
            switch (direction)
            {
                case LinkDirection.Right:
                    itemDirection = new Vector2(0, 1);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Left:
                    itemDirection = new Vector2(0, -1);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Up:
                    itemDirection = new Vector2(1, 0);
                    delta = new Vector2(-20, 18);
                    break;
                case LinkDirection.Down:
                    itemDirection = new Vector2(-1, 0);
                    delta = new Vector2(-20, 18);
                    break;
            }
            Vector2 itemLoc = Vector2.Add(delta, centerPosition);
            switch (item)
            {
                case 1:
                    //projectiles.add(new Arrow...)
                    break;
                case 2:
                    break;

            }
        }

        //Number to enum?
        public void ChangeItem()
        {
            // TODO: Not Implemented this Sprint
        }

        public void TakeDamage(int damage)
        {

        }
        public Vector2 Direction
        {
            get => this.vDirection;
        }

        public float Speed
        {
            get => this.speed;
            set => this.speed = value;
        }

        public Vector2 CenterPosition
        {
            get => this.centerPosition;
            set
            {
                this.linkSprite.Center = value;
                this.centerPosition = value;
            }
        }
    }
}
