using Microsoft.Xna.Framework;
using cse3902.Constants;

namespace cse3902
{
    public class Camera
    {
        private Game1 game;
        private Vector2 gameplayBounds;
        private Vector2 gameplayOffset;

        private Vector2 topLeftCoordinate;
        private Vector2 dimensionScale;

        private Matrix transformationMatrix;

        private bool cameraIsMoving;
        private Vector2 smoothMovementDirection;
        private int smoothMovementUpdateCyclesRemaining;
        private Vector2 smoothMovementDestination;

        private bool hudDisplayed;
        private Vector2 gameplayOffsetDirection;

        public Camera(Game1 game)
        {
            this.game = game;
            Vector2 windowBounds = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            //adjustment for menu bar
            gameplayBounds = windowBounds + new Vector2(0, -DimensionConstants.HudHeight);
            gameplayOffset = new Vector2(0, DimensionConstants.HudHeight);

            topLeftCoordinate = new Vector2(0, 0);

            cameraIsMoving = false;
            smoothMovementDirection = new Vector2(0, 0);
            smoothMovementUpdateCyclesRemaining = 0;
            smoothMovementDestination = new Vector2(0, 0);

            MoveCamera(topLeftCoordinate, windowBounds);

            hudDisplayed = false;
            gameplayOffsetDirection = new Vector2();
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

        public void MoveCamera(Vector2 translation)
        {
            if (!cameraIsMoving) MoveCameraSmoothOverride(translation);
        }

        private void MoveCameraSmoothOverride(Vector2 translation)
        {
            topLeftCoordinate += translation;
            transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale + gameplayOffset, 0);
        }

        public void SmoothMoveCamera(Vector2 topLeft, int numberUpdateCyclesToComplete)
        {
            if (!cameraIsMoving)
            {
                smoothMovementUpdateCyclesRemaining = numberUpdateCyclesToComplete;
                smoothMovementDestination = topLeft;

                if (numberUpdateCyclesToComplete < 1) numberUpdateCyclesToComplete = 1;
                smoothMovementDirection = (topLeft - topLeftCoordinate) / numberUpdateCyclesToComplete;

                cameraIsMoving = true;
            }
        }

        public void ToggleHudDisplayed(int numberUpdateCyclesToComplete)
        {
            if (!cameraIsMoving)
            {
                SmoothMoveCamera(topLeftCoordinate, numberUpdateCyclesToComplete);
                if (numberUpdateCyclesToComplete < 1) numberUpdateCyclesToComplete = 1;
                gameplayOffsetDirection = new Vector2(0, DimensionConstants.WindowHeight - DimensionConstants.HudHeight) / numberUpdateCyclesToComplete;

                if (hudDisplayed) gameplayOffsetDirection *= -1;

                hudDisplayed = !hudDisplayed;
            }
        }

        public void Update()
        {
            if (cameraIsMoving)
            {
                if (smoothMovementUpdateCyclesRemaining <= 1)
                {
                    gameplayOffset += gameplayOffsetDirection;
                    topLeftCoordinate = smoothMovementDestination;
                    transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale + gameplayOffset, 0);
                    cameraIsMoving = false;
                    gameplayOffsetDirection = new Vector2();
                }
                else
                {
                    gameplayOffset += gameplayOffsetDirection;
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

            hudDisplayed = false;
            gameplayOffsetDirection = new Vector2();
        }
    }
}
