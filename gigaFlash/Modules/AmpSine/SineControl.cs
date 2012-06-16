using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;
using System.Windows.Forms;

namespace gigaFlash.Modules
{
    public class SineControl : IAmpSineView
    {
        #region IAmpSineView Members

        public event gigaFlash.Delegates.VoidDelegate TwinkleFired;

        public bool TwinkleButtonEnabled
        {
            get
            {
                return false; 
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IThreadedView Members

        public event gigaFlash.Delegates.TypedDelegate<Color> StartFired;

        public event gigaFlash.Delegates.VoidDelegate StopFired;

        public void Start()
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                EventUtils.FireTypedEvent(StartFired, cd.Color);
            }
        }

        public void Stop()
        {
            EventUtils.FireEvent(StopFired); 
        }


        public bool RunButtonEnabled
        {
            get
            {
                return false; 
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IModuleView Members

        public void Show() { }

        public event gigaFlash.Delegates.VoidDelegate Disposing;

        #endregion

        #region IThreadedView Members


        public event TypedDelegate<int> SpeedChanged;

        #endregion

        #region IAmpSineView Members


        public event TypedDelegate<Color> Color1Changed;

        public event TypedDelegate<Color> Color2Changed;

        #endregion

        #region IAmpSineView Members


        public Color Color1
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public Color Color2
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IModuleView Members

        public void ShowDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
