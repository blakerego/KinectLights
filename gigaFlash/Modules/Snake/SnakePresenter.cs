using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;
using System.ComponentModel; 

namespace gigaFlash.Modules
{
	/// <summary>
	/// The logic in this class allows the lights to move in a snake-like fashion around the room. 
	/// By this I mean :         
	/// ... -  (|) - (||) - (|||)  
	/// 
	/// At the snake head, the color will be at full intensity. The next in line will be at half intensity. 
	/// 
	/// </summary>
    public class SnakePresenter : ThreadPresenterBase
    {
        #region Constructor 
        public SnakePresenter(ISnakeView pView, LightState pState)
            : base(pView, pState)
        {
            mView = pView;
            mLightState = pState;
            mView.SpeedChanged += new TypedDelegate<int>(OnSpeedChanged);
        }

        #endregion 

		#region Handlers
        /// <summary>
        /// This is meant to be run on a background thread which starts when 
        /// the user clicks the snake button. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void RunContinuously(object sender, DoWorkEventArgs e)
        {
            Color c = ThreadColor;
            while (mContinueThread)
            {
                int snakeParts = mLightState.Lights.Count;
                foreach (Light turn in mLightState.Lights)
                {
                    // Each light get its turn to be the head of the snake for the given color. 
                    int snakeHead = mLightState.Lights.IndexOf(turn);

                    foreach (Light light in mLightState.Lights)
                    {
                        int currentIndex = mLightState.Lights.IndexOf(light);
                        int distanceFromHead = (snakeHead - currentIndex) % snakeParts;
                        if (distanceFromHead < 0)
                            distanceFromHead += snakeParts;

                        int divideValue = distanceFromHead + 1;

                        light.Red = (int)c.R / divideValue ^ 3;
                        light.Green = (int)c.G / divideValue ^ 3;
                        light.Blue = (int)c.B / divideValue ^ 3;

                    }

                    mLightState.Update();
                    System.Threading.Thread.Sleep(100 - mSpeed);
                }
            }
            mLightState.Clear();
        }

        void OnSpeedChanged(int value)
        {
            mSpeed = value; 
        }
		#endregion

        #region Members / Props
        ISnakeView mView;

        protected int mSpeed = 50; 
        #endregion 
    }
}
