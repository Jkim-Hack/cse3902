using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902
{
    public class Camera
    {
        private CameraPart gameplayCamera;
        private CameraPart hudCamera;

        private Vector2 hudTopLeftCoord;

        public Camera(Vector2 menuTopLeftCoordinate)
        {
            gameplayCamera = new CameraPart(DimensionConstants.WindowDimensions + new Vector2(0, -DimensionConstants.HudHeight), new Vector2(0, DimensionConstants.HudHeight));
            hudCamera = new CameraPart(DimensionConstants.WindowDimensions, new Vector2(0, -DimensionConstants.GameplayHeight));

            hudTopLeftCoord = menuTopLeftCoordinate;
            hudCamera.MoveCamera(hudTopLeftCoord, DimensionConstants.WindowDimensions);
        }

        public void MoveCamera(Vector2 topLeft, Vector2 dimensions)
        {
            gameplayCamera.MoveCamera(topLeft, dimensions);
        }

        public void MoveCamera(Vector2 translation)
        {
            gameplayCamera.MoveCamera(translation);
        }

        public void SmoothMoveCamera(Vector2 topLeft, int numberUpdateCyclesToComplete)
        {
            gameplayCamera.SmoothMoveCamera(topLeft, numberUpdateCyclesToComplete);
        }

        public void ToggleHudDisplayed(int numberUpdateCyclesToComplete)
        {
            gameplayCamera.ToggleHudDisplayed(numberUpdateCyclesToComplete);
            hudCamera.ToggleHudDisplayed(numberUpdateCyclesToComplete);
        }

        public void Update()
        {
            gameplayCamera.Update();
            hudCamera.Update();
        }

        public Matrix GetGameplayTransformationMatrix()
        {
            return gameplayCamera.GetTransformationMatrix();
        }

        public Matrix GetHudTransformationMatrix()
        {
            return hudCamera.GetTransformationMatrix();
        }

        public bool IsCameraMoving()
        {
            return gameplayCamera.IsCameraMoving();
        }

        public void Reset()
        {
            gameplayCamera.Reset();
            hudCamera.Reset();

            hudCamera.MoveCamera(hudTopLeftCoord, DimensionConstants.WindowDimensions);
        }
    }
}
