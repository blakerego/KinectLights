using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.ConfigObjects
{
    public enum LightMode
    {
        SingleColor
    }

    public class LightConfig
    {
        public int Index;

        public LightMode Mode; 
    }
}
