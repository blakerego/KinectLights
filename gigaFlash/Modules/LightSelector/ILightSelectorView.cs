using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;
using System.Windows.Forms;

namespace gigaFlash.Modules
{
    public interface ILightSelectorView : IModuleView
    {
        /// <summary>
        /// Event that fires when a new color is selected. 
        /// </summary>
        event TypedDelegate<Color> UpdateColorFired;

        /// <summary>
        /// Gets or sets the selected items from the light selector view. 
        /// </summary>
        List<Object> SelectedItems { get; set; }

        /// <summary>
        /// Gets the list of available lights in the view. 
        /// </summary>
        List<Object> Items { get; } 

        /// <summary>
        /// Gets or sets the color to be sent to the light state. 
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// Event that fires when the user triggers a clear. 
        /// </summary>
        event VoidDelegate ClearClicked; 
    }
}
