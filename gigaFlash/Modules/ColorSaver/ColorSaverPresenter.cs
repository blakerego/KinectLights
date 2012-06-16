using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;

namespace gigaFlash.Modules.ColorSaver
{
    public class ColorSaverPresenter
    {
        #region Constructor 
        public ColorSaverPresenter(IColorSaverView pView, LightState pState)
        {
            mView.ColorSaveRequested += new gigaFlash.Delegates.TypedDelegate<System.Drawing.Color>(HandleColorSaveRequest);
        }
        #endregion

        #region Handlers
        protected virtual void HandleColorSaveRequest(Color value)
        {
            /*
            XmlSerializer serializer = new XmlSerializer(typeof(Light)); 
            TextWriter stream = new StreamWriter(Path.
            serializer.Serialize(
             */ 
        }
        #endregion 

        #region Members / Properties
        IColorSaverView mView;
        #endregion 
    }
}
