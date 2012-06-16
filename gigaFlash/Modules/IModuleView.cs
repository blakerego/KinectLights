using System;
using gigaFlash.Delegates;
namespace gigaFlash
{
	public interface IModuleView
	{
		/// <summary>
        /// Queues the UI form or website to be shown. 
        /// </summary>
        void Show();

        /// <summary>
        /// The Dispose Event is being fired. 
        /// </summary>
        event VoidDelegate Disposing;
	}
}

