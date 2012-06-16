using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gigaFlash.Modules
{
	public class LightViewPresenter : ILight
	{
		#region Constructor
		public LightViewPresenter(ILightView pView, Light pModel)
		{
			mView = pView;
			mModel = pModel; 
		}
		#endregion 

		#region Public API
        public Color Color
        {
            get { return mModel.Color; }
            set 
			{ 
				mModel.Color = value; 
				mView.Color = value; 
			}
        }

        public int Red
        {
            get { return mModel.Red; }
            set { mModel.Red = value; } 
        }

        public int Green
        {
            get { return mModel.Green; }
            set { mModel.Green = value; } 
        }

        public int Blue
        {
            get { return mModel.Blue; }
            set { mModel.Blue = value; } 
        }
		#endregion

		#region Members / Properties
		protected ILightView mView;

		protected Light mModel;

		protected SineControl mSineControl; 
		#endregion
	}
}
