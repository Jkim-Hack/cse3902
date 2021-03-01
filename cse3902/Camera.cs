using Microsoft.Xna.Framework;
using System;

// this class should probably go with the rooms when they are merged

namespace cse3902
{
    public class Camera
    {
        private Game1 game;
        private Vector2 windowBounds;

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
            windowBounds = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);

            topLeftCoordinate = new Vector2(0, 0);
            dimensionScale = new Vector2(1,1);

            transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale,0));

            cameraIsMoving = false;
            smoothMovementDirection = new Vector2(0, 0);
            smoothMovementUpdateCyclesRemaining = 0;
            smoothMovementDestination = new Vector2(0, 0);
        }

        public void MoveCamera(Vector2 topLeft, int cameraWidth, int cameraHeight)
        {
            if (!cameraIsMoving) MoveCamera(topLeft, new Vector2(cameraWidth,cameraHeight));
        }

        public void MoveCamera(Vector2 topLeft, Vector2 dimensions)
        {
            if (!cameraIsMoving)
            {
                topLeftCoordinate = topLeft;
                dimensionScale = windowBounds / dimensions;

                transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale, 0));
                transformationMatrix.Translation = new Vector3(-topLeft * dimensionScale, 0);
            }
        }

        public void MoveCamera(Vector2 translation)
        {
            if (!cameraIsMoving) MoveCameraSmoothOverride(translation);
        }

        private void MoveCameraSmoothOverride(Vector2 translation)
        {
            topLeftCoordinate += translation;
            transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale, 0);
        }

        public void MoveCameraUp(int pixels)
        {
            if (!cameraIsMoving) MoveCamera(new Vector2(0, -pixels));
        }

        public void MoveCameraDown(int pixels)
        {
            if (!cameraIsMoving) MoveCamera(new Vector2(0, pixels));
        }

        public void MoveCameraLeft(int pixels)
        {
            if (!cameraIsMoving) MoveCamera(new Vector2(-pixels, 0));
        }

        public void MoveCameraRight(int pixels)
        {
            if (!cameraIsMoving) MoveCamera(new Vector2(pixels, 0));
        }

        public Matrix GetTransformationMatrix()
        {
            return transformationMatrix;
        }

        public void SmoothMoveCamera(Vector2 translation, int numberUpdateCyclesToComplete)
        {
            if (!cameraIsMoving)
            {
                smoothMovementUpdateCyclesRemaining = numberUpdateCyclesToComplete;
                smoothMovementDestination = topLeftCoordinate + translation;

                if (numberUpdateCyclesToComplete < 1) numberUpdateCyclesToComplete = 1;
                smoothMovementDirection = translation / ((float) numberUpdateCyclesToComplete);

                cameraIsMoving = true;
            }
        }

        public void SmoothMoveCameraUp(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(totalPixels * new Vector2(0, -1), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraDown(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(totalPixels * new Vector2(0, 1), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraLeft(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(totalPixels * new Vector2(-1, 0), numberUpdateCyclesToComplete);
        }
        public void SmoothMoveCameraRight(int totalPixels, int numberUpdateCyclesToComplete)
        {
            SmoothMoveCamera(totalPixels * new Vector2(1, 0), numberUpdateCyclesToComplete);
        }

        public void Update()
        {
            if (cameraIsMoving)
            {
                MoveCameraSmoothOverride(smoothMovementDirection);
                smoothMovementUpdateCyclesRemaining--;
                if (smoothMovementUpdateCyclesRemaining <= 0)
                {
                    cameraIsMoving = false;
                    topLeftCoordinate = smoothMovementDestination;
                }
            }
        }

        public bool GetCameraMoving()
        {
            return cameraIsMoving;
        }

        public void Reset()
        {
            topLeftCoordinate = new Vector2(0, 0);
            dimensionScale = new Vector2(1, 1);

            transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale, 0));

            cameraIsMoving = false;
            smoothMovementDirection = new Vector2(0, 0);
            smoothMovementUpdateCyclesRemaining = 0;
            smoothMovementDestination = new Vector2(0, 0);
        }
    }
}
