// GameManager.TouchInput

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace GameManager
{
    internal class TouchInput
    {
        public static TouchCollection newTouchState;
        public static TouchCollection oldTouchState;
        public static Vector2 oldPosition;

        public static bool JustLeftClicked()
        {
            return TouchInput.newTouchState.Count > 0
                      && TouchInput.oldTouchState.Count == 0;
        }

        public static bool JustLeftReleased()
        {
            return TouchInput.newTouchState.Count == 0 &&
                   TouchInput.oldTouchState.Count > 0;
        }

        public static bool LeftClicked()
        {
            return TouchInput.newTouchState.Count == 1;
        }

        public static bool JustRightClicked()
        {
            return  TouchInput.newTouchState.Count == 2
                      && TouchInput.oldTouchState.Count == 0;
        }

        public static bool JustRightReleased()
        {
            return TouchInput.newTouchState.Count == 0 &&  
                   TouchInput.oldTouchState.Count > 0;
        }

        public static bool RightClicked()
        {
            return TouchInput.newTouchState.Count == 2;
        }

        public static bool Down()
        {
           bool Result = false;
          Vector2 oldPos = oldPosition;
          Vector2 newPos = GetPosition();
            if (newPos.Y > oldPos.Y)
            {
                Result = true;
            }
            return Result;
        }

        public static bool Up()
        {
            bool Result = false;
            Vector2 oldPos = oldPosition;
            Vector2 newPos = GetPosition();
            if (newPos.Y < oldPos.Y)
            {
               Result = true;
            }
            return Result;
        }

        public static bool Right()
        {
            bool Result = false;
            Vector2 oldPos = oldPosition;
            Vector2 newPos = GetPosition();
            if (newPos.X > oldPos.X)
            {
                Result = true;
            }
            return Result;
        }

        public static bool SwipeRight()
        {
            bool Result = false;

            
            Vector2 oldPos = oldPosition;
            Vector2 newPos = GetPosition();
            if (TouchInput.newTouchState.Count == 2 && newPos.X > oldPos.X)
            {
                Result = true;
            }
            return Result;
        }

        public static bool SwipeLeft()
        {
            bool Result = false;

            Vector2 oldPos = oldPosition;
            Vector2 newPos = GetPosition();
            if (TouchInput.newTouchState.Count == 2 && newPos.X < oldPos.X)
            {
                Result = true;
            }
            return Result;
        }

        public static bool Left()
        {
            bool Result = false;
            Vector2 oldPos = oldPosition;
            Vector2 newPos = GetPosition();
            if (newPos.X < oldPos.X)
            {
               Result = true;
            }
            return Result;
        }

        public static Vector2 GetPosition()
        {
            double x = 0;
            double y = 0;

            if (TouchInput.newTouchState.Count > 0)
            {
                TouchCollection state = TouchInput.newTouchState;// TouchPanel.GetState();
                
                   x = (double)state[0].Position.X;
                
                   y = (double)state[0].Position.Y;
                
                //oldPosition.X = (float)x;
                //oldPosition.Y = (float)y;
            }
            //oldPosition.X = (float)x;
            //oldPosition.Y = (float)y;

            return Vector2.Divide(new Vector2((float)x, (float)y), /*Game1.Instance.Screen.Scale*/1);
        }
    }
}
