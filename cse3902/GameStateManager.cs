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
        private Camera camera;

        private int remainingToggleCooldown;
        private const int toggleCooldown = 10;

        private bool closingMenu;
        private bool openingMenu;

        private const int cameraCycles = 60;

        private static GameStateManager gameStateManagerInstance = new GameStateManager();
        public static GameStateManager Instance
        {
            get => gameStateManagerInstance;
        }
        private GameStateManager()
        {
            pausedState = PauseState.Unpaused;
            remainingToggleCooldown = 0;
            closingMenu = false;
            openingMenu = false;
        }

        public void ToggleMenuDisplayed()
        {
            if (!ValidToggle()) return;
            switch (pausedState)
            {
                case PauseState.Unpaused:
                    openingMenu = true;
                    pausedState = PauseState.MenuPaused;
                    camera.ToggleHudDisplayed(cameraCycles);
                    break;
                case PauseState.MenuDisplayed:
                    closingMenu = true;
                    camera.ToggleHudDisplayed(cameraCycles);
                    break;
                default:
                    break;
            }
        }

        public void TogglePaused()
        {
            if (!ValidToggle()) return;
            switch (pausedState)
            {
                case PauseState.Unpaused:
                    pausedState = PauseState.Paused;
                    break;
                case PauseState.Paused:
                    pausedState = PauseState.Unpaused;
                    break;
                case PauseState.MenuDisplayed:
                    pausedState = PauseState.MenuPaused;
                    break;
                case PauseState.MenuPaused:
                    pausedState = PauseState.MenuDisplayed;
                    break;
                default: //this should never happen
                    pausedState = PauseState.Paused;
                    break;
            }
        }

        private bool ValidToggle()
        {
            if (remainingToggleCooldown > 0 || closingMenu || openingMenu) return false;
            remainingToggleCooldown = toggleCooldown;
            return true;
        }

        public bool IsUnpaused()
        {
            return pausedState == PauseState.Unpaused;
        }
        public bool InMenu()
        {
            return pausedState == PauseState.MenuDisplayed;
        }

        public void Update()
        {
            remainingToggleCooldown--;
            if (closingMenu && !camera.IsCameraMoving())
            {
                closingMenu = false;
                pausedState = PauseState.Unpaused;
            }
            else if (openingMenu && !camera.IsCameraMoving())
            {
                openingMenu = false;
                pausedState = PauseState.MenuDisplayed;
            }
        }

        public Camera Camera
        {
            set => camera = value;
        }
    }
}
