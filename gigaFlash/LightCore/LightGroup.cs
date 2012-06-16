using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using gigaFlash.Modules;

namespace gigaFlash
{
    public class LightGroup
    {
        #region Constructor
        public LightGroup(List<LightViewPresenter> pLightViews, ILightState pState)
        {
            mLights = pLightViews;
            mState = pState; 
        }
        #endregion 

        #region Public API
        public void Update(Color color)
        {
            foreach (LightViewPresenter light in mLights)
            {
                light.Color = color; 
            }
            Update(); 
        }

        public void Update()
        {
            mState.Update();
        }

        public void Clear()
        {
            foreach (LightViewPresenter light in mLights)
            {
                light.Color = Color.Black; 
            }
            Update(); 
        }

        public List<LightViewPresenter> Lights
        {
            get
            {
                return mLights; 
            }
            set
            {
                mLights = value;
            }
        }

        public void FadeTo(Color pColor, int pSeconds)
        {
            int maxIterations = pSeconds * 1000;

            List<int> redDiff = new List<int>();
            List<int> greenDiff = new List<int>(); 
            List<int> blueDiff = new List<int>();

            List<int> transformationMap = new List<int>(); 

            foreach (LightViewPresenter light in mLights)
            {
                /*
                redDiff.Add((Int16.Parse(pColor.R) - light.Red) / maxIterations);
                greenDiff.Add((Int16.Parse(pColor.G) - light.Green) / maxIterations);
                blueDiff.Add((Int16.Parse(pColor.B) - light.Blue) / maxIterations); 
                 */ 
            }

            bool finished = false;

            while (!finished)
            {
                foreach (LightViewPresenter light in mLights)
                {
                    int index = mLights.IndexOf(light); 
                    light.Red = light.Red - redDiff[index];
                    light.Green = light.Green - greenDiff[index];
                    light.Blue = light.Blue - blueDiff[index]; 
                }
            }
        }
        #endregion 

        #region Members / Fields
        protected List<LightViewPresenter> mLights = new List<LightViewPresenter>();

        protected ILightState mState; 
        #endregion 
    }
}
