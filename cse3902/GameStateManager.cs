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
            ItemPickupPaused,
            Dying,
            WallMasterGrabbed
        }
        private PauseState pausedState;
        private Game1 game;

        private int remainingToggleCooldown;
        private const int toggleCooldown = 10;

        private const int cameraCycles = 60;
        private int updateCycles;

        private static GameStateManager gameStateManagerInstance = new GameStateManager();
        public static GameStateManager Instance
        {
            get => gameStateManagerInstance;
        }
        private GameStateManager()
        {
            pausedState = PauseState.Unpaused;
            remainingToggleCooldown = 0;
            updateCycles = 0;
        }

        public void ToggleMenuDisplayed()
        {
            if (!ValidToggle()) return;
            switch (pausedState)
            {
                case PauseState.Unpaused:
                    pausedState = PauseState.MenuOpening;
                    game.Camera.ToggleHudDisplayed(cameraCycles);
                    break;
                case PauseState.MenuDisplayed:
                    pausedState = PauseState.MenuClosing;
                    game.Camera.ToggleHudDisplayed(cameraCycles);
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
            updateCycles = numberUpdateCyclesToComplete;
            SoundFactory.Instance.backgroundMusic.Stop();
            SoundFactory.Instance.fanfare.Play();
        }

        public void LinkDies(int numberUpdateCyclesToComplete)
        {
            SoundFactory.Instance.backgroundMusic.Stop();
            updateCycles = numberUpdateCyclesToComplete;
            pausedState = PauseState.Dying;
        }

        public void LinkGrabbedByWallMaster(int numberUpdateCyclesToComplete)
        {
            SoundFactory.Instance.backgroundMusic.Stop();
            updateCycles = numberUpdateCyclesToComplete;
            pausedState = PauseState.WallMasterGrabbed;
            game.CollisionManager.Disabled = true;
        }

        private bool ValidToggle()
        {
            if (remainingToggleCooldown > 0 || pausedState == PauseState.MenuOpening || pausedState == PauseState.MenuClosing || game.Camera.IsCameraMoving()) return false;
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
        public bool IsDying()
        {
            return pausedState == PauseState.Dying;
        }
        public bool IsGrabbedByWallMaster()
        {
            return pausedState == PauseState.WallMasterGrabbed;
        }

        public void Update()
        {
            remainingToggleCooldown--;
            if (pausedState == PauseState.MenuClosing && !game.Camera.IsCameraMoving())
            {
                pausedState = PauseState.Unpaused;
            }
            else if (pausedState == PauseState.MenuOpening && !game.Camera.IsCameraMoving())
            {
                pausedState = PauseState.MenuDisplayed;
            } 
            else if (IsPickingUpItem() || IsDying() || IsGrabbedByWallMaster())
            {
                updateCycles--;
                if (updateCycles <= 0)
                {
                    pausedState = PauseState.Unpaused;
                    SoundFactory.Instance.backgroundMusic.Play();
                    game.CollisionManager.Disabled = false;
                    if (IsDying() || IsGrabbedByWallMaster()) game.RoomHandler.Reset();
                }
            }
        }

        public void Reset()
        {
            pausedState = PauseState.Unpaused;
            remainingToggleCooldown = 0;
            updateCycles = 0;
        }

        public Game1 Game
        {
            set => this.game = value;
        }
    }
}
