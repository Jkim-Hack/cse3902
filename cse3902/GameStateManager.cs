namespace cse3902
{
    public class GameStateManager
    {
        public enum PauseState
        {
            Unpaused,
            Paused,
            MenuDisplayed,
            MenuPaused
        }
        private PauseState pausedState;

        private static GameStateManager gameStateManagerInstance = new GameStateManager();
        public static GameStateManager Instance
        {
            get => gameStateManagerInstance;
        }
        private GameStateManager()
        {
            pausedState = PauseState.Unpaused;
        }

        public PauseState PausedState
        {
            get => pausedState;
            set => pausedState = value;
        }
    }
}
