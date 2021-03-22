using System.Collections.Generic;
using cse3902.Interfaces;
using cse3902.Items;
using System.Collections;
using cse3902.SpriteFactory;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cse3902.Rooms
{
    public class CloudAnimation
    {
        public List<CloudAnimationSprite> cloudAnims;

        private static CloudAnimation instance = new CloudAnimation();

        public static CloudAnimation Instance
        {
            get
            {
                return instance;
            }
        }

        private CloudAnimation()
        {
            cloudAnims = new List<CloudAnimationSprite>();
        }

        public void Update(GameTime gameTime)
        {
            for (int i = cloudAnims.Count - 1; i >= 0; i --)
            {
                if (cloudAnims[i].Update(gameTime) < 0)
                {
                    cloudAnims.RemoveAt(i);
                }    
            }
        }

        public void Draw()
        {
            foreach (CloudAnimationSprite cloudAnim in cloudAnims)
            {
                cloudAnim.Draw();
            }
        }

        public void LoadNewRoom(List<IEntity> newList, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < newList.Count; i ++)
            {
                CloudAnimationSprite newAnim = (CloudAnimationSprite) ItemSpriteFactory.Instance.CreateCloudAnimation(spriteBatch ,newList[i].Center);
                cloudAnims.Add(newAnim);
            }
        }
    }
}
