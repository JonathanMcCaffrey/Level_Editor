using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace LevelEditor
{
#if XBOX
    //When compiled for the XBox, Mouse and MouseState are unknown.
    //Define dummy bodies for them, so a MouseState object
    //can be passed to the Update method of the camera.
    public class MouseState
    {
    }
    public static class Mouse
    {
        public static MouseState GetState()
        {
            return new MouseState();
        }
    }
#endif

    class QuakeCamera
    {
        Matrix viewMatrix;
        Matrix projectionMatrix;
        Viewport viewPort;

        float leftrightRot = -10;
        float updownRot = 0;
        const float rotationSpeed = 0.005f;
        Vector3 cameraPosition;

        public QuakeCamera(Viewport viewPort)
        {
            viewMatrix = FileManager.Get().ViewMatrix;
            projectionMatrix = FileManager.Get().ProjectionMatrix;

            cameraPosition = FileManager.Get().CameraPosition;
            leftrightRot = -17.3000031f;


            UpdateViewMatrix();

            //calls the constructor below with default startingPos and rotation values
        }

        public void Update()
        {
            MouseState currentMouseState = InputManager.Get().CurrentMouseState;
            KeyboardState keyState = InputManager.Get().CurrentKeyboardState;
            GamePadState gamePadState = InputManager.Get().CurrentGamePadState;

            InputManager input = InputManager.Get();

#if XBOX            
            leftrightRot -= rotationSpeed * gamePadState.ThumbSticks.Left.X * 5.0f;
            updownRot += rotationSpeed * gamePadState.ThumbSticks.Left.Y * 5.0f;

            UpdateViewMatrix();

            float moveUp = gamePadState.Triggers.Right - gamePadState.Triggers.Left;
            AddToCameraPosition(new Vector3(gamePadState.ThumbSticks.Right.X, moveUp, -gamePadState.ThumbSticks.Right.Y));
#else
            if (true)
            {
                float xDifference = 0;
                float yDifference = 0;

                if (input.KeyHeldDown(Keys.NumPad8))
                {
                    yDifference = -10;
                }
                if (input.KeyHeldDown(Keys.NumPad2))
                {
                    yDifference = 10;
                }
                if (input.KeyHeldDown(Keys.NumPad4))
                {
                    xDifference = -10;
                }
                if (input.KeyHeldDown(Keys.NumPad6))
                {
                    xDifference = 10;
                }

                leftrightRot -= rotationSpeed * xDifference;
                updownRot -= rotationSpeed * yDifference;
                UpdateViewMatrix();
            }

            float cameraSpeed = 50;

            if (keyState.IsKeyDown(Keys.Up))      //Forward
                AddToCameraPosition(new Vector3(0, cameraSpeed, 0));
            if (keyState.IsKeyDown(Keys.Down))    //Backward
                AddToCameraPosition(new Vector3(0, -cameraSpeed, 0));
            if (keyState.IsKeyDown(Keys.Right))   //Right
                AddToCameraPosition(new Vector3(cameraSpeed, 0, 0));
            if (keyState.IsKeyDown(Keys.Left))    //Left
                AddToCameraPosition(new Vector3(-cameraSpeed, 0, 0));
            if (keyState.IsKeyDown(Keys.Q))       //Up
                AddToCameraPosition(new Vector3(0, 1, 0));
            if (keyState.IsKeyDown(Keys.Z))       //Down
                AddToCameraPosition(new Vector3(0, -1, 0));
#endif
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            float moveSpeed = 0.5f;
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            cameraPosition += moveSpeed * rotatedVector;

            FileManager.Get().CameraPosition = cameraPosition;

            UpdateViewMatrix();
        }

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);
            Vector3 cameraFinalUpVector = cameraPosition + cameraRotatedUpVector;

            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraFinalTarget, cameraRotatedUpVector);

            FileManager.Get().ViewMatrix = viewMatrix;
        }

        public float UpDownRot
        {
            get { return updownRot; }
            set { updownRot = value; }
        }

        public float LeftRightRot
        {
            get { return leftrightRot; }
            set { leftrightRot = value; }
        }

        public Matrix ProjectionMatrix
        {
            get { return projectionMatrix; }
        }

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }
        public Vector3 Position
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                UpdateViewMatrix();
            }
        }
        public Vector3 TargetPosition
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
                Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
                Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;
                return cameraFinalTarget;
            }
        }
        public Vector3 Forward
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraForward = new Vector3(0, 0, -1);
                Vector3 cameraRotatedForward = Vector3.Transform(cameraForward, cameraRotation);
                return cameraRotatedForward;
            }
        }
        public Vector3 SideVector
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraOriginalSide = new Vector3(1, 0, 0);
                Vector3 cameraRotatedSide = Vector3.Transform(cameraOriginalSide, cameraRotation);
                return cameraRotatedSide;
            }
        }
        public Vector3 UpVector
        {
            get
            {
                Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);
                Vector3 cameraOriginalUp = new Vector3(0, 1, 0);
                Vector3 cameraRotatedUp = Vector3.Transform(cameraOriginalUp, cameraRotation);
                return cameraRotatedUp;
            }
        }
    }
}
