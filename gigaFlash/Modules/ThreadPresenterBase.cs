using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Drawing;

namespace gigaFlash.Modules
{
    public abstract class ThreadPresenterBase : LightModulePresenterBase
    {
        #region Constructor 
        public ThreadPresenterBase(IThreadedView pView, LightState pState)
            : base(pView, pState)
        {
            mThreadedView = pView;
            mThreadedView.StartFired += new gigaFlash.Delegates.TypedDelegate<Color>(OnMainThreadStart);
            mThreadedView.StopFired += new gigaFlash.Delegates.VoidDelegate(OnStopClicked);
            mThreadedView.Disposing += new gigaFlash.Delegates.VoidDelegate(OnDisposing);

            mMainThread = new BackgroundWorker();
            mMainThread.WorkerSupportsCancellation = true;
            mMainThread.DoWork += new DoWorkEventHandler(RunContinuously);
            mMainThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnMainThreadStopped);

        }
        #endregion 

        #region Handlers

        protected abstract void RunContinuously(object sender, DoWorkEventArgs e); 

        protected virtual void OnStopClicked()
        {
            mContinueThread = false;
        }

        protected void OnMainThreadStart(Color c)
        {
            ThreadColor = c; 
            mContinueThread = true;
            mThreadedView.RunButtonEnabled = false; 
            mMainThread.RunWorkerAsync();
        }

        protected virtual void OnMainThreadStopped(object sender, RunWorkerCompletedEventArgs e)
        {
            mThreadedView.RunButtonEnabled = true;
            mLightState.Clear(); 
        }

        protected virtual void OnDisposing()
        {
            mContinueThread = false;
            mLightState.Clear(); 
        }
        #endregion 

        #region Members / Properties
        protected IThreadedView mThreadedView;

        protected bool mContinueThread;

        BackgroundWorker mMainThread;

        protected Color ThreadColor = ColorUtils.GetRandomColor(); 
        #endregion 
    }
}
