using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using gigaFlash.Delegates;
using gigaFlash.ConfigObjects;
using gigaFlash.Modules;

namespace gigaFlash
{
    public interface IRoom : IModuleView
    {
        /// <summary>
        /// Announces a color change to a specific light index. 
        /// 
        /// If int is less than 0, we will update all lights. 
        /// </summary>
        event DualTypedDelegate<int, Color> LightUpdate;

        /// <summary>
        /// Meant to be run after events are hooked up from the presenter.
        /// </summary>
        void PostInitializiation();

        /// <summary>
        /// Announces a color save event. 
        /// </summary>
        event TypedDelegate<ColorConfig> ColorSaveFired;

        /// <summary>
        /// Loads the preferences for the particular user. 
        /// </summary>
        /// <param name="pPrefObj"></param>
        void LoadPreferences(UserPrefObj pPrefObj);

        /// <summary>
        /// Event that is fired when a sine event is fired from a 
        /// particular light. 
        /// </summary>
        event TypedDelegate<List<int>> SineEventFired; 

        /// <summary>
        /// Stops any thread. 
        /// </summary>
        event TypedDelegate<List<int>> StopEventFired;

		/// <summary>
		/// Broadcasts a LightView that will need to be listened to. 
		/// </summary>
		event TypedDelegate<ILightView> LightViewAdded;

        /// <summary>
        /// The room is disposing. 
        /// </summary>
        event VoidDelegate Disposing;

        /// <summary>
        /// Presenter to run this after setting up listener. 
        /// </summary>
        void InitializeRoom();
    }
}
