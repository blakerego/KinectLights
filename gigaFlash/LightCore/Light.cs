using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gigaFlash
{
    public class Light : ILight
    {
        #region Constructor 
        public Light(int pRed, int pGreen, int pBlue)
        {
            mRed = pRed;
            mGreen = pGreen;
            mBlue = pBlue; 
        }
        #endregion 

        #region Members / Properties
        protected Color mColor;
        public Color Color
        {
            get { return mColor; }
            set
            {
                mColor = value;
                mRed = mColor.R;
                mGreen = mColor.G;
                mBlue = mColor.B; 
            }
        }

        protected int mRed;
        public int Red
        {
            get { return mRed; }
            set 
            { 
                mRed = value;
                mColor = Color.FromArgb(mRed, mGreen, mBlue); 
            } 
        }

        protected int mGreen;
        public int Green
        {
            get { return mGreen; }
            set 
            { 
                mGreen = value;
                mColor = Color.FromArgb(mRed, mGreen, mBlue); 
            } 
        }

        protected int mBlue;
        public int Blue
        {
            get { return mBlue; }
            set 
            { 
                mBlue = value;
                mColor = Color.FromArgb(mRed, mGreen, mBlue); 
            } 
        }
        #endregion 
    }
}
