using Microsoft.Xna.Framework;

namespace cse3902
{
    public class Camera
    {
        private Game1 game;
        private Vector2 gameplayBounds;
        private Vector2 gameplayOffset;

        // dimensions = (width,height)
        private Vector2 topLeftCoordinate;
        private Vector2 dimensionScale;

        private Matrix transformationMatrix;

        private bool cameraIsMoving;
        private Vector2 smoothMovementDirection;
        private int smoothMovementUpdateCyclesRemaining;
        private Vector2 smoothMovementDestination;

        public Camera(Game1 game)
        {
            this.game = game;
            Vector2 windowBounds = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            //adjustment for menu bar
            gameplayBounds = windowBounds + new Vector2(0, 0);
            gameplayOffset = new Vector2(0, 0);

            topLeftCoordinate = new Vector2(0, 0);

            cameraIsMoving = false;
            smoothMovementDirection = new Vector2(0, 0);
            smoothMovementUpdateCyclesRemaining = 0;
            smoothMovementDestination = new Vector2(0, 0);

            MoveCamera(topLeftCoordinate, windowBounds);
        }

        public void MoveCamera(Vector2 topLeft, Vector2 dimensions)
        {
            if (!cameraIsMoving)
            {
                topLeftCoordinate = topLeft;
                dimensionScale = gameplayBounds / dimensions;

                transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale, 0));
                transformationMatrix.Translation = new Vector3(-topLeft * dimensionScale + gameplayOffset, 0);
            }
        }

        public void MoveCamera(Vector2 topLeft, int cameraWidth, int cameraHeight)
        {
            MoveCamera(topLeft, new Vector2(cameraWidth, cameraHeight));
        }

        public void MoveCamera(Rectangle newCamera)
        {
            MoveCamera(newCamera.Location.ToVector2(), newCamera.Size.ToVector2());
        }

        public void MoveCamera(Vector2 translation)
        {
            if (!cameraIsMoving) MoveCameraSmoothOverride(translation);
        }

        private void MoveCameraSmoothOverride(Vector2 translation)
        {
            topLeftCoordinate += translation;
            transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale + gameplayOffset, 0);
        }

        public void MoveCameraUp(int pixels)
        {
            MoveCamera(new Vector2(0, -pixels));
        }

        public void MoveCameraDown(int pixels)
        {
            MoveCamera(new Vector2(0, pixels));
        }

        public void MoveCameraLeft(int pixels)
        {
            MoveCamera(new Vector2(-pixels, 0));
        }

        public void MoveCameraRight(int pixels)
        {
            MoveCamera(new Vector2(pixels, 0));
        }

        public void SmoothMoveCamera(Vector2 translation, int numberUpdateCyclesToComplete)
        {
            if (!cameraIsMoving)
            {
                smoothMovementUpdateCyclesRemaining = numberUpdateCyclesToComplete;
                smoothMovementDestination = topLeftCoordinate + translation;

                if (numberUpdateCyclesToComplete < 1) numberUpdateCyclesToComplete = 1;
                smoothMovementDirection = translation / ((float)numberUpdateCyclesToComplete);

                cameraIsMoving = true;
            }
        }

        public void SmoothMoveCameraUp(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(new Vector2(0, -totalPixels), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraDown(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(new Vector2(0, totalPixels), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraLeft(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(new Vector2(-totalPixels, 0), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraRight(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(new Vector2(totalPixels, 0), numberUpdateCyclesToComplete);
        }

        public void Update()
        {
            if (cameraIsMoving)
            {
                if (smoothMovementUpdateCyclesRemaining <= 1)
                {
                    topLeftCoordinate = smoothMovementDestination;
                    transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale + gameplayOffset, 0);
                    cameraIsMoving = false;
                }
                else
                {
                    MoveCameraSmoothOverride(smoothMovementDirection);
                    smoothMovementUpdateCyclesRemaining--;
                }
            }
        }

        public Matrix GetTransformationMatrix()
        {
            return transformationMatrix;
        }

        public bool GetCameraMoving()
        {
            return cameraIsMoving;
        }

        public void Reset()
        {
            topLeftCoordinate = new Vector2(0, 0);

            cameraIsMoving = false;
            smoothMovementDirection = new Vector2(0, 0);
            smoothMovementUpdateCyclesRemaining = 0;
            smoothMovementDestination = new Vector2(0, 0);

            MoveCamera(topLeftCoordinate, new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
        }
    }
}
