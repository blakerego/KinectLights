using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;

namespace gigaFlash.Modules
{
    public interface IThreadedView : IModuleView
    {
        /// <summary>
        /// The user has triggered the event to start.  
        /// </summary>
        event TypedDelegate<Color> StartFired;

        /// <summary>
        /// The user wants to stop. 
        /// </summary>
        event VoidDelegate StopFired;


        /// <summary>
        /// Allows the RunSnake button to be enabled. 
        /// </summary>
        bool RunButtonEnabled { get; set; }

        /// <summary>
        /// The speed of the threaded event has changed. 
        /// </summary>
        event TypedDelegate<int> SpeedChanged; 
    }
}
