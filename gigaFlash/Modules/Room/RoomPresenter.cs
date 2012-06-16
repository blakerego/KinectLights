using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;
using gigaFlash.Modules;
using gigaFlash.ConfigObjects;

namespace gigaFlash.Room
{
    public class RoomPresenter : LightModulePresenterBase
	{
		#region Constructor
		public RoomPresenter(IRoom pView, LightState pState)
            : base(pView, pState)
        {
            mView = pView;
			mView.LightViewAdded += new TypedDelegate<ILightView>(OnLightViewAdded);
            mView.InitializeRoom(); 
            mView.LightUpdate += new DualTypedDelegate<int, Color>(OnLightUpdate);
            mView.ColorSaveFired += new TypedDelegate<gigaFlash.ConfigObjects.ColorConfig>(OnColorSaveFired);
            mView.SineEventFired += new TypedDelegate<List<int>>(OnSineFired);
            mView.StopEventFired += new TypedDelegate<List<int>>(OnStopEvent);
            mView.Disposing += new VoidDelegate(OnDisposing);
            mView.PostInitializiation();
		}

		#endregion 

		#region Public Methods
		public void OnLightUpdate(int val1, Color val2)
        {
            if (val1 >= 0)
            {
                mLightState.Lights[val1].Color = val2;
            }
            else
            {
                foreach (Light light in mLightState.Lights)
                {
                    light.Color = val2; 
                }
            }
            mLightState.Update(); 
        }

        public void LoadPreferences(UserPrefObj pUserPrefObj)
        {
            mPrefObj = pUserPrefObj;
            mView.LoadPreferences(pUserPrefObj);
        }
        #endregion 

        #region Handlers
		protected void OnLightViewAdded(ILightView value)
		{
			LightViewPresenter lvp = new LightViewPresenter(value, mLightState.Lights[sLightIndex]);
			mLightViewPresenters.Add(lvp); 
			sLightIndex++; 
		}

        protected void OnStopEvent(List<int> value)
        {
            LightGroup group = new LightGroup(
                GetLightViewPresentersFromIndices(value),
                mLightState);
            foreach (SineControl sineCtrl in mSineControls)
            {
                sineCtrl.Stop(); 
            }
            mLightState.Update(); 
        }
        
        protected void OnSineFired(List<int> value)
        {
            LightGroup group = new LightGroup(
                GetLightViewPresentersFromIndices(value), 
                mLightState);
            SineControl sineCtrl = new SineControl(); 
            AmpSinePresenter pres = new AmpSinePresenter(sineCtrl, 
                mLightState, 
                group);
            mSineControls.Add(sineCtrl); 

            sineCtrl.Start(); 
        }

        protected List<LightViewPresenter> GetLightViewPresentersFromIndices(List<int> value)
        {
            List<LightViewPresenter> lightGroup = new List<LightViewPresenter>();
            foreach (int index in value)
            {
                try
                {
                    lightGroup.Add(mLightViewPresenters[index]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    //Index does not exist in this light state. 
                }
            }
            return lightGroup; 
        }
        protected void OnColorSaveFired(gigaFlash.ConfigObjects.ColorConfig value)
        {
            UserPrefObj userprefobj;
            if (mPrefObj == null)
            {
                userprefobj = new UserPrefObj();
                String now = DateTime.Now.ToString();
                userprefobj.DateCreated = now;
                userprefobj.DateLastModfied = now;
                DateTime dt = DateTime.Parse(userprefobj.DateCreated);
                userprefobj.Colors.Add(value);
            }
            else
            {
                mPrefObj.Colors.Add(value);
                userprefobj = mPrefObj;
            }

            EventUtils.FireTypedEvent(RoomSaveFired, userprefobj);
        }

        protected virtual void OnDisposing()
        {
            foreach (Light l in mLightState.Lights)
            {
            }
        }
        #endregion 

        #region Members / Properties
        protected UserPrefObj mPrefObj; 

        protected IRoom mView;

        public event TypedDelegate<UserPrefObj> RoomSaveFired;

        protected List<SineControl> mSineControls = new List<SineControl>();

		protected List<LightViewPresenter> mLightViewPresenters = new List<LightViewPresenter>(); 
		
		protected static int sLightIndex = 0; 
        #endregion 
    }
}
