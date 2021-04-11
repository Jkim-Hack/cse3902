using System;

namespace cse3902.Constants
{
    public class ParticleConstants
    {
        public static Random rand = new Random();
        public const bool UseParticleEffects = true;

        /* Arrow emitter */
	    public const int ArrowParticleDensity = 1000;
        public const int ArrowParticleSize = 2;

        public const int ArrowParticleLifetimeMin = 10;
        public const int ArrowParticleLifetimeMax = 75;

        public const float ArrowParticleVelocityScale = 0.2f;

        public const int ArrowParticleColorMin = 60;
        public const int ArrowParticleColorMax = 230;
    }
}
