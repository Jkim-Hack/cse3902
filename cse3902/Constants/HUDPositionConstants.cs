using System;
using Microsoft.Xna.Framework;

namespace cse3902.Constants
{
    public class HUDPositionConstants
    {
        // TODO: Figure out positioning here
        private const int HealthHUDWidth = 70;
        private const int HealthHUDHeight = 37;
	    public static Vector2 HealthHUDPosition = new Vector2(DimensionConstants.OriginalWindowDimensions.X - HealthHUDWidth, DimensionConstants.OriginalWindowDimensions.Y - HealthHUDHeight);
    }
}
