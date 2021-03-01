﻿using System;
using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Projectiles
{
    public class ProjectileHandler
    {
        private Texture2D arrow;
        private Texture2D bomb;
        private Texture2D boomerang;
        private Texture2D fireball;
        private Texture2D swordItems;
        private Texture2D swordWeapons;

        public List<IProjectile> projectiles { get; set; }

        private static ProjectileHandler instance = new ProjectileHandler();

        public static ProjectileHandler Instance
        {
            get
            {
                return instance;
            }
        }

        private ProjectileHandler()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            arrow = content.Load<Texture2D>("arrow");
            bomb = content.Load<Texture2D>("bombnew");
            boomerang = content.Load<Texture2D>("boomerang");
            fireball = content.Load<Texture2D>("fireball");
            swordItems = content.Load<Texture2D>("SwordItem");
            swordWeapons = content.Load<Texture2D>("SwordAnimation");

            projectiles = new List<IProjectile>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                IProjectile projectile = projectiles[i];
                projectile.Update(gameTime);
                if (projectile.AnimationComplete)
                {
                    projectiles.Remove(projectile);
                    i--;
                }
            }
        }

        public void Draw()
        {
            foreach (IProjectile projectile in projectiles)
            {
                projectile.Draw();
            }
        }

        public void Reset()
        {
            projectiles = new List<IProjectile>();
        }

        public IProjectile CreateArrowItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            IProjectile newProj = new ArrowItem(spriteBatch, arrow, startingPos, dir);
            projectiles.Add(newProj);
            return newProj;
        }

        public IProjectile CreateBombItem(SpriteBatch spriteBatch, Vector2 startingPos)
        {
            IProjectile newProj = new BombItem(spriteBatch, bomb, startingPos);
            projectiles.Add(newProj);
            return newProj;
        }

        public IProjectile CreateBoomerangItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            IProjectile newProj = new BoomerangItem(spriteBatch, boomerang, startingPos, dir);
            projectiles.Add(newProj);
            return newProj;
        }
        public IProjectile CreateFireballObject(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            IProjectile newProj = new FireballSprite(spriteBatch, fireball, startingPos, dir);
            projectiles.Add(newProj);
            return newProj;
        }

        public IProjectile CreateSwordItem(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir)
        {
            IProjectile newProj = new SwordItem(spriteBatch, swordItems, startingPos, dir);
            projectiles.Add(newProj);
            return newProj;
        }

        public IProjectile CreateSwordWeapon(SpriteBatch spriteBatch, Vector2 startingPos, Vector2 dir, int swordType)
        {
            IProjectile newProj = new SwordWeapon(spriteBatch, swordWeapons, startingPos, dir, swordType);
            projectiles.Add(newProj);
            return newProj;
        }


    }
}