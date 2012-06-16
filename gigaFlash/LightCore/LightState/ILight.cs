using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace gigaFlash
{
    /// <summary>
    /// The basic interface for a Light. 
    /// </summary>
    public interface ILight
    {
        Color Color
        {
            get;
            set; 
        }
    }
}
