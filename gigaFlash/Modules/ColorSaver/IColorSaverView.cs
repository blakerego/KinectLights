using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using gigaFlash.Delegates;

namespace gigaFlash
{
    public interface IColorSaverView
    {
        event TypedDelegate<Color> ColorSaveRequested;

    }
}
