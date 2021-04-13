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
        public const int SwordParticleCircleAmount = 250;
        public const int SwordParticleAddAmount = 50;

        public const int SwordParticleSize = 3;

        public const int SwordParticleLifetimeMin = 25;
        public const int SwordParticleLifetimeMax = 50;

        public const float SwordParticleVelocityScale = 0.4f;
        public const int SwordParticleAmplitude = 5;

        public const float SwordParticleOpacity = 0.5f;
    }
}
