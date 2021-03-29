namespace cse3902
{
    public class GameStateManager
    {
        private enum PauseState
        {
            Unpaused,
            Paused,
            MenuDisplayed,
            MenuOpening,
            MenuClosing,
            ItemPickup,
            ItemPickupPaused
        }
        private PauseState pausedState;
        private Camera camera;

        private int remainingToggleCooldown;
        private const int toggleCooldown = 10;

        private const int cameraCycles = 60;
        private int itemPickupCycles;

        private static GameStateManager gameStateManagerInstance = new GameStateManager();
        public static GameStateManager Instance
        {
            get => gameStateManagerInstance;
        }
        private GameStateManager()
        {
            pausedState = PauseState.Unpaused;
            remainingToggleCooldown = 0;
            itemPickupCycles = 0;
        }

        public void ToggleMenuDisplayed()
        {
            if (!ValidToggle()) return;
            switch (pausedState)
            {
                case PauseState.Unpaused:
                    pausedState = PauseState.MenuOpening;
                    camera.ToggleHudDisplayed(cameraCycles);
                    break;
                case PauseState.MenuDisplayed:
                    pausedState = PauseState.MenuClosing;
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
                case PauseState.ItemPickup:
                    pausedState = PauseState.ItemPickupPaused;
                    break;
                case PauseState.ItemPickupPaused:
                    pausedState = PauseState.ItemPickup;
                    break;
                default:
                    break;
            }
        }

        public void LinkPickupItem(int numberUpdateCyclesToComplete)
        {
            pausedState = PauseState.ItemPickup;
            itemPickupCycles = numberUpdateCyclesToComplete;
        }

        private bool ValidToggle()
        {
            if (remainingToggleCooldown > 0 || pausedState == PauseState.MenuOpening || pausedState == PauseState.MenuClosing || camera.IsCameraMoving()) return false;
            remainingToggleCooldown = toggleCooldown;
            return true;
        }

        public bool IsUnpaused()
        {
            return pausedState == PauseState.Unpaused;
        }
        public bool InMenu(bool isTransitioning)
        {
            return pausedState == PauseState.MenuDisplayed || (isTransitioning && (pausedState == PauseState.MenuOpening || pausedState == PauseState.MenuClosing));
        }
        public bool IsPickingUpItem()
        {
            return pausedState == PauseState.ItemPickup;
        }

        public void Update()
        {
            remainingToggleCooldown--;
            if (pausedState == PauseState.MenuClosing && !camera.IsCameraMoving())
            {
                pausedState = PauseState.Unpaused;
            }
            else if (pausedState == PauseState.MenuOpening && !camera.IsCameraMoving())
            {
                pausedState = PauseState.MenuDisplayed;
            } 
            else if (IsPickingUpItem())
            {
                itemPickupCycles--;
                if (itemPickupCycles <= 0) pausedState = PauseState.Unpaused;
            }
        }

        public Camera Camera
        {
            set => camera = value;
        }
    }
}
