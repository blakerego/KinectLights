using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public abstract class LightModulePresenterBase
    {
        #region Constructor
        public LightModulePresenterBase(IModuleView pView, ILightState pLightState)
            : this(pLightState)
        {
            mModuleView = pView;
        }

        public LightModulePresenterBase(ILightState pLightState)
        {
            mLightState = pLightState;
        }
        #endregion

        #region Members / Properties
        protected ILightState mLightState;

        protected IModuleView mModuleView; 
        #endregion

        #region Public Methods
        public void ShowUI()
        {
            mModuleView.Show(); 
        }
        #endregion 
    }
}
