using System;

namespace cse3902.Constants
{
    public class ParticleConstants
    {
        public const bool UseParticleEffects = true;

        public static Random rand = new Random();

        /* Arrow emitter */
        public const int ArrowParticleInitialAmount = 100;
        public const int ArrowParticleAddAmount = 25;

        public const int ArrowParticleSize = 5;

        public const int ArrowParticleLifetimeMin = 5;
        public const int ArrowParticleLifetimeMax = 35;

        public const float ArrowParticleVelocityScale = 0.3f;

        public const int ArrowParticleColorMin = 150;
        public const int ArrowParticleColorMax = 256;

        /* Sword emitter */
        public const int SwordParticleCircleAmount = 200;
        public const int SwordParticleAddAmount = 50;

        public const int SwordParticleSize = 3;

        public const int SwordParticleLifetimeMin = 25;
        public const int SwordParticleLifetimeMax = 50;

        public const float SwordParticleVelocityScale = 0.4f;

        public const float SwordParticleOpacity = 0.5f;

        /* Bomb emitter */
        public const int BombParticleCenterAmount = 200;
        public const int BombParticleCenterAddAmount = 50;
        public const int BombParticleRingAmount = 100;

        public const int BombParticleCenterSize = 15;
        public const int BombParticleRingSize = 15;

        public const int BombParticleCenterLifetimeMin = 10;
        public const int BombParticleCenterLifetimeMax = 20;
        public const int BombParticleRingLifetimeMin = 30;
        public const int BombParticleRingLifetimeMax = 40;

        public const float BombParticleCenterVelocityScale = 1.5f;
        public const float BombParticleRingVelocityScale = 1f;

        public const int BombParticleCenterColorMax = 128;

        /* Fireball emitter */
        public const int FireballParticleCenterAmount = 20;
        public const int FireballParticleTrailAmount = 10;

        public const int FireballParticleAddDelay = 10; /* milliseconds */

        public const int FireballParticleCenterSize = 10;
        public const int FireballParticleTrailSize = 10;

        public const int FireballParticleCenterLifetime = 50;
        public const int FireballParticleTrailLifetimeMin = 10;
        public const int FireballParticleTrailLifetimeMax = 15;

        public const float FireballParticleCenterVelocityScale = 0.1f;
        public const float FireballParticleTrailVelocityScale = 1;

        public const float FireballParticleAngleRange = (float)Math.PI / 2.0f;

        public const int FireballParticleCenterColorMax = 64;
        public const int FireballParticleTrailColorMax = 128;
    }
}
