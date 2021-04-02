using cse3902.Sounds;

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
                    SoundFactory.Instance.backgroundMusic.Pause();
                    break;
                case PauseState.Paused:
                    pausedState = PauseState.Unpaused;
                    SoundFactory.Instance.backgroundMusic.Play();
                    break;
                case PauseState.ItemPickup:
                    pausedState = PauseState.ItemPickupPaused;
                    SoundFactory.Instance.fanfare.Pause();
                    break;
                case PauseState.ItemPickupPaused:
                    pausedState = PauseState.ItemPickup;
                    SoundFactory.Instance.fanfare.Play();
                    break;
                default:
                    break;
            }
        }

        public void LinkPickupItem(int numberUpdateCyclesToComplete)
        {
            pausedState = PauseState.ItemPickup;
            itemPickupCycles = numberUpdateCyclesToComplete;
            SoundFactory.Instance.backgroundMusic.Stop();
            SoundFactory.Instance.fanfare.Play();
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
                if (itemPickupCycles <= 0)
                {
                    pausedState = PauseState.Unpaused;
                    SoundFactory.Instance.backgroundMusic.Play();
                }
            }
        }

        public Camera Camera
        {
            set => camera = value;
        }
    }
}
