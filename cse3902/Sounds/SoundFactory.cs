using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Sounds
{
    public class SoundFactory
    {
        public SoundEffect getHeart { get; set; }
        public SoundEffect brickSound { get; set; }
        public SoundEffect paddleBounceSound { get; set; }
        public SoundEffect wallBounceSound { get; set; }
        public SoundEffect missSound { get; set; }
        public SpriteFont labelFont { get; set; }

        private static SoundFactory instance = new SoundFactory();

        public static SoundFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SoundFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            getHeart = content.Load<SoundEffect>("LOZ_Get_Heart");
        }
    }
}
