using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Threading;

namespace gigaFlash.Modules
{
    public class AmpSinePresenter : LightModulePresenterBase
    {
        #region Constructor 
        public AmpSinePresenter(IAmpSineView pView, ILightState pState)
            : base(pView, pState)
        {
            Initialize(pView); 
        }

        public AmpSinePresenter(IAmpSineView pView, ILightState pState, LightGroup pGroup)
            : base(pView, pState)
        {
            mLightGroup = pGroup;
            Initialize(pView); 
        }

        protected void Initialize(IAmpSineView pView)
        {
            mView = pView;
            if (mLightGroup != null)
            {
                foreach (LightViewPresenter lvp in mLightGroup.Lights)
                {
                    //this one updates the UI as well as the lights in the room.
                    mLights.Add(lvp);
                }
            }
            else
            {
                foreach (Light l in mLightState.Lights)
                {
                    mLights.Add(l); 
                }
            }
            mView.StartFired += new gigaFlash.Delegates.TypedDelegate<Color>(OnStartFired);
            mView.StopFired += new gigaFlash.Delegates.VoidDelegate(OnStopFired);
            mView.TwinkleFired += new gigaFlash.Delegates.VoidDelegate(OnTwinkleFired);
            mView.SpeedChanged += new gigaFlash.Delegates.TypedDelegate<int>(OnSpeedChanged);
            InitializeThread();

            mTwinkleWorker = new BackgroundWorker();
            mTwinkleWorker.WorkerSupportsCancellation = true;
            mTwinkleWorker.DoWork += new DoWorkEventHandler(OnTwinkleThreadFired);
            mTwinkleWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnSineFinished);

            OnStartFired(mView.Color2); 
        }

        protected void InitializeThread()
        {
            mSineWorker = new BackgroundWorker();
            mSineWorker.WorkerSupportsCancellation = true;
            mSineWorker.DoWork += new DoWorkEventHandler(OnSineThreadFired);
            mSineWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnSineFinished);
        }
        #endregion 

        #region Public Methods
        public static double PowerSine(double time, double phase)
        {
            double amp = 100;
            double freq = 100;
            double stepsPerCycle = 100;
            double sine = amp / 100 * Math.Sin(Math.PI * freq / 100 * time / stepsPerCycle + phase);
            return Math.Round(sine * sine, 4); 
        }
        #endregion 

        #region Handlers
        public virtual void OnStopFired()
        {
            mContinueSine = false; 
        }

        protected virtual void OnStartFired(Color pColor2)
        {
            //mColor2 = pColor2; 
            mSineWorker.RunWorkerAsync();
            mContinueSine = true; 
        }

        void OnTwinkleFired()
        {
            mTwinkleWorker.RunWorkerAsync(); 
            mContinueSine = true;
        }

        void OnSpeedChanged(int value)
        {
            mSpeed = value;
        }


        protected virtual void OnSineFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            mLightState.Clear(); 
        }

        protected virtual void OnSineThreadFired(object sender, DoWorkEventArgs e)
        {
            double time = 0; 
            while (mContinueSine)
            {
                double sineVal = AmpSinePresenter.PowerSine(time, 0);
                int red = (int)((mView.Color2.R - mView.Color1.R) * sineVal + mView.Color1.R);
                int green = (int)((mView.Color2.G - mView.Color1.G) * sineVal + mView.Color1.G);
                int blue = (int)((mView.Color2.B - mView.Color1.B) * sineVal + mView.Color1.B);
                foreach (Light light in mLightState.Lights)
                {
                    light.Red = red;
                    light.Green = green;
                    light.Blue = blue;
                }
                mLightState.Update();
                System.Threading.Thread.Sleep(100 - mSpeed);
                time++; 
            }
        }

        protected virtual void OnTwinkleThreadFired(object sender, DoWorkEventArgs e)
        {
            Random r = new Random(); 
            double time = 0;

            List<double> phaseMap = new List<double>();
            List<Color> colorMap = new List<Color>(); 
            foreach (ILight l in mLights)
            {
                phaseMap.Add(r.Next(2 * 314));
                colorMap.Add(ColorUtils.GetRandomColor()); 
            }

            while (mContinueSine)
            {
                
                int index = 0; 
                foreach (ILight light in mLights)
                {
                    double scale = PowerSine(time, phaseMap[index] / 100);
 
                    light.Color = Color.FromArgb(
                    Convert.ToInt16(colorMap[index].R * scale),
                    Convert.ToInt16(colorMap[index].G * scale),
                    Convert.ToInt16(colorMap[index].B * scale)
                    );

                    index++; 
                }
                mLightState.Update();
                System.Threading.Thread.Sleep(50);
                time++;
            }

        }

        #endregion 

        #region Members / Properties
        protected int mSpeed = 50; 

        protected IAmpSineView mView;

        protected BackgroundWorker mSineWorker;

        protected BackgroundWorker mTwinkleWorker; 

        protected bool mContinueSine;

        protected LightGroup mLightGroup;

        List<ILight> mLights = new List<ILight>();
        #endregion 
    }
}
