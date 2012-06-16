using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gigaFlash
{
    /// <summary>
    /// To be used for groups of lights. 
    /// </summary>
    public interface ILightState
    {
        /// <summary>
        /// Updates the state to a given color. 
        /// </summary>
        /// <param name="color"></param>
        void Update(Color color);

        /// <summary>
        /// Updates the state to its current internal value. 
        /// </summary>
        void Update();

        /// <summary>
        /// Turns off all lights within the group. 
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns all lights associated with this group. 
        /// </summary>
        List<Light> Lights { get; set; }
    }
}
