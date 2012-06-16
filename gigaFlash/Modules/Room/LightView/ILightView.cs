using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;

namespace gigaFlash.Modules
{
	/// <summary>
	/// Consumers of this interface will be making a UI representation of
	/// the a given light in a Room. 
	/// </summary>
	public interface ILightView : ILight
	{
		/// <summary>
		/// Gets or sets whether the current light is selected
		/// </summary>
		bool Selected { get; set; }

		/// <summary>
		/// Gets or sets the light's name. 
		/// </summary>
		String LabelName { get; set; }

		/// <summary>
		/// Announces that the color has been set. 
		/// </summary>
		event TypedDelegate<ILightView> ColorSet;  
	}
}
