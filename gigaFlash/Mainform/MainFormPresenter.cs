using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Modules;
using System.Windows.Forms;
using System.Drawing;
using gigaFlash.Room;
using gigaFlash.ConfigObjects;
using System.Xml.Serialization;
using System.IO;

namespace gigaFlash.Mainform
{
    public class MainFormPresenter
    {
        #region Constructor 
        public MainFormPresenter(IMainformView pView, LightState pState)
        {
            mView = pView;
            mState = pState;

            mRoomPresenter = new RoomPresenter(mView.RoomView, pState);
            mRoomPresenter.RoomSaveFired += new gigaFlash.Delegates.TypedDelegate<UserPrefObj>(OnRoomSaveFired);

            mView.PreferencesLoaded += new gigaFlash.Delegates.TypedDelegate<UserPrefObj>(LoadPreferences);
            mView.LightSelectorClicked += new gigaFlash.Delegates.VoidDelegate(OnLightSelectorClicked);
            mView.SnakeModuleClicked += new gigaFlash.Delegates.VoidDelegate(OnSnakeModuleClicked);
            mView.AmpSineClicked += new gigaFlash.Delegates.VoidDelegate(OnAmpSineClicked);
            mView.ThunderClicked += new gigaFlash.Delegates.VoidDelegate(OnThunderClicked);
            
            mView.MoveLightLeftEvent += new gigaFlash.Delegates.VoidDelegate(OnLightLeftEvent);
            mView.MoveLightRightEvent += new gigaFlash.Delegates.VoidDelegate(OnLightRightEvent);
            mView.ClickEventFired += new gigaFlash.Delegates.VoidDelegate(OnClickEvent);
        }


        #endregion 

        #region Handlers 
        protected virtual void OnLightSelectorClicked()
        {
            GetPresenter<LightSelectorPresenter, LightSelectorFactory>().ShowUI(); 
        }

        protected virtual void OnSnakeModuleClicked()
        {
            GetPresenter<SnakePresenter, SnakePresFactory>().ShowUI(); 
        }

        protected virtual void OnAmpSineClicked()
        {
            GetPresenter<AmpSinePresenter, AmpSinePresFactory>().ShowUI(); 
        }

        protected virtual void OnThunderClicked()
        {
            GetPresenter<ThunderPresenter, ThunderPresenterFactory>().ShowUI();  
        }

        void OnRoomSaveFired(UserPrefObj userprefobj)
        {
            userprefobj.Username = mView.CurrentUser;
            XmlSerializer serializer = new XmlSerializer(typeof(UserPrefObj));
            TextWriter stream = new StreamWriter(ConfigPath + mView.CurrentUser + ".xml");
            serializer.Serialize(stream, userprefobj);
            stream.Close();

        }

        protected int mLightIntensity = 100; 
        protected virtual void OnScrollEvent(int value)
        {
            //set previous light to black
            int lightIndex = mLightIntensity % mState.Lights.Count;
            mState.Lights[lightIndex].Color = Color.Black; 

            // set new light. 
            lightIndex = mLightIntensity++ % mState.Lights.Count; 
            mState.Lights[lightIndex].Color = Color.Red; 

        }

        void OnLightRightEvent()
        {
            if (mLightIntensity <= 100 - mSensitivity)
            {
                for (int i = 0; i < mSensitivity; i++)
                {
                    mLightIntensity++;
                }
            }
            UpdateRandomColor(); 
        }

        void OnLightLeftEvent()
        {
            if (mLightIntensity >= mSensitivity)
            {
                for (int i = 0; i < mSensitivity; i++)
                {
                    mLightIntensity--;
                }
            }
            UpdateRandomColor(); 
        }

        protected void UpdateRandomColor()
        {
            foreach (Light light in mState.Lights)
            {
                light.Color = Color.FromArgb(
                    mLightIntensity * mCurrentRandomColor.R / 100,
                    mLightIntensity * mCurrentRandomColor.G / 100,
                    mLightIntensity * mCurrentRandomColor.B / 100
                    );
            }
            mState.Update(); 
        }

        protected virtual void OnClickEvent()
        {
            mCurrentRandomColor = ColorUtils.GetRandomColor(); 

            //Scale up color to brightest ratio.
            foreach(Light light in mState.Lights) 
            {
                light.Color = mCurrentRandomColor; 
            }
            UpdateRandomColor(); 
        }

        #endregion 

        #region Public Methods
        public Pres GetPresenter<Pres, Factory>()
            where Pres : LightModulePresenterBase
            where Factory : LightModuleFactoryBase<Pres>
        {
            Factory factory = Activator.CreateInstance(typeof(Factory)) as Factory;
            return factory.Create(mState);
        }

        public void LoadPreferences(UserPrefObj pUserPrefObj)
        {
            mPrefObj = pUserPrefObj;
            mRoomPresenter.LoadPreferences(pUserPrefObj); 
        }
        #endregion 

        #region Members / Properties
        protected UserPrefObj mPrefObj; 

        protected IMainformView mView;

        protected LightState mState;

        protected Color mCurrentRandomColor;

        protected int mSensitivity = 5;

        protected RoomPresenter mRoomPresenter;

        //need to figure out how to get a relative path. 
        protected string ConfigPath
        {
            get { return UserPrefObj.ConfigPath; } 
        }
        #endregion
    }
}
