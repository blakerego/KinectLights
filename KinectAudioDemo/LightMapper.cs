using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace KinectAudioDemo
{
    public class LightMapper
    {
        public static Color GetColorFromDistance(float distance)
        {
            if (distance == 0)
                return Color.Black; 
            else
            {
                int amplified = Math.Abs((int) (255 * distance)); 
                return Color.FromArgb(
                    (int)amplified % 255,
                    (int) ((amplified * 1.2) % 255),
                    (int) ((amplified * 1.3) % 255)
                    );
            }
        }
    }
}
