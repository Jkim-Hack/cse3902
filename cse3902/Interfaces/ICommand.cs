namespace cse3902.Interfaces
{
    // Command
    public interface ICommand
    {
        public void Execute(int id);
        public void Unexecute();
    }
}
