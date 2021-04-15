namespace cse3902.Interfaces
{
    public interface IDependentParticleEmmiter : IParticleEmmiter
    {
        public bool Kill { set; }
    }
}
