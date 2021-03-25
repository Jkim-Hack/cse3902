using System.Collections.Generic;
using cse3902.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections;

namespace cse3902.Rooms
{
    public class RoomProjectiles
    {
        public IList projectiles;

        private static RoomProjectiles instance = new RoomProjectiles();

        public static RoomProjectiles Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomProjectiles()
        {
            projectiles = new List<IProjectile>();
        }

        public void AddProjectile(IProjectile projectile)
        {
            projectiles.Add(projectile);
        }

        public void RemoveProjectile(IProjectile projectile)
        {
            projectile.Collided = true;
            //projectiles.Remove(projectile);
            //(projectiles as List<IItem>).RemoveAll(x => x.Center == projectile.Center);
        }

        public void Update(GameTime gameTime)
        {
            for(int i =0; i < projectiles.Count; i++)
            {
                IProjectile projectile = projectiles[i] as IProjectile;
                if (projectile.Update(gameTime) < 0)
                {
                    projectiles.Remove(projectile);
                    i--;
                }
                else if (projectile.AnimationComplete)
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

        public void LoadNewRoom(ref List<IProjectile> oldList, List<IProjectile> newList)
        {
            oldList = new List<IProjectile>();

            List<IProjectile> castedProjectiles = projectiles as List<IProjectile>;

            for (int i = 0; i < projectiles.Count; i++)
            {
                oldList.Add(castedProjectiles[i]);
            }

            projectiles = new List<IProjectile>();

            for (int i = 0; i < newList.Count; i++)
            {
                projectiles.Add(newList[i]);
            }
        }

        public ref IList ListRef
        {
            get => ref projectiles;
        }

    }
}
