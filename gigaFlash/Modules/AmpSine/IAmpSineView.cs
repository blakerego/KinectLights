using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Delegates;
using System.Drawing;

namespace gigaFlash.Modules
{
    public interface IAmpSineView : IThreadedView
    {
        event VoidDelegate TwinkleFired;

        Color Color1 { get; set; }

        Color Color2 { get; set; } 

        bool TwinkleButtonEnabled { get; set; }
    }
}
