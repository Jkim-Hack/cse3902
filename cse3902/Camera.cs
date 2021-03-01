using Microsoft.Xna.Framework;

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

        public Camera(Game1 game)
        {
            this.game = game;
            windowBounds = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);

            topLeftCoordinate = new Vector2(0, 0);
            dimensionScale = new Vector2(1,1);

            transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale,0));
        }

        public void MoveCamera(Vector2 topLeft, int cameraWidth, int cameraHeight)
        {
            MoveCamera(topLeft, new Vector2(cameraWidth,cameraHeight));
        }

        public void MoveCamera(Vector2 topLeft, Vector2 dimensions)
        {
            topLeftCoordinate = topLeft;
            dimensionScale = windowBounds / dimensions;

            transformationMatrix = Matrix.CreateScale(new Vector3(dimensionScale, 0));
            transformationMatrix.Translation = new Vector3(-topLeft*dimensionScale, 0);
        }

        public void MoveCamera(Vector2 translation)
        {
            topLeftCoordinate += translation;
            transformationMatrix.Translation = new Vector3(-topLeftCoordinate * dimensionScale, 0);
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

        public Matrix getTransformationMatrix()
        {
            return transformationMatrix;
        }
    }
}
