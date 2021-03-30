using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace cse3902.Sounds
{
    public class SoundFactory
    {
        public SoundEffect getHeart { get; set; }
        public SoundEffect enemyHit { get; set; }
        public SoundEffect enemyDie { get; set; }
        public SoundEffect linkHit { get; set; }
        public SoundEffect linkDie { get; set; }

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
            enemyHit = content.Load<SoundEffect>("LOZ_Get_Heart");
        }

        public static void PlaySound(SoundEffect sound)
        {
            float volume = 1;
            float pitch = 0.0f;
            float pan = 0.0f;

            sound.Play(volume, pitch, pan);
        }
    }
}
