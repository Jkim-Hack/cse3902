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
        public SoundEffect swordSlash { get; set; }
        public SoundEffect arrowBoomerang { get; set; }
        public SoundEffect bombBlow { get; set; }
        public SoundEffect lowHealth { get; set; }
        public SoundEffect getItem { get; set; }
        public SoundEffect getRupee { get; set; }
        public SoundEffect text { get; set; }
        public SoundEffect keyAppear { get; set; }
        public SoundEffect doorUnlock { get; set; }
        public SoundEffect stairs { get; set; }
        public SoundEffect bossScream { get; set; }
        public SoundEffect bossHurt { get; set; }
        public SoundEffect secret { get; set; }

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
            // enemy hurt was unavailable, check later and update sound file
            enemyHit = content.Load<SoundEffect>("LOZ_Enemy_Die");
            enemyDie = content.Load<SoundEffect>("LOZ_Enemy_Die");
            linkHit = content.Load<SoundEffect>("LOZ_Link_Hurt");
            linkDie = content.Load<SoundEffect>("LOZ_Link_Die");
            swordSlash = content.Load<SoundEffect>("LOZ_Sword_Slash");
            arrowBoomerang = content.Load<SoundEffect>("LOZ_Arrow_Boomerang");
            bombBlow = content.Load<SoundEffect>("LOZ_Bomb_Blow");
            lowHealth = content.Load<SoundEffect>("LOZ_LowHealth");
            getItem = content.Load<SoundEffect>("LOZ_Get_Item");
            // change to rupee sound
            getRupee = content.Load<SoundEffect>("LOZ_Get_Item");
            text = content.Load<SoundEffect>("LOZ_Text");
            keyAppear = content.Load<SoundEffect>("LOZ_Key_Appear");
            doorUnlock = content.Load<SoundEffect>("LOZ_Door_Unlock");
            stairs = content.Load<SoundEffect>("LOZ_Stairs");
            bossScream = content.Load<SoundEffect>("LOZ_Boss_Scream1");
            // change to boss hurt
            bossHurt = content.Load<SoundEffect>("LOZ_Boss_Scream1");
            //secret = content.Load<SoundEffect>("LOZ_Secret");
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
