/* Interface for particle emitters that are dependent on the position/state of an entity/projectile */
namespace cse3902.Interfaces
{
    public interface IDependentParticleEmmiter : IParticleEmmiter
    {
        public bool Kill { set; }
    }
}
