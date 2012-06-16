using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public interface IViewFactory<T>
    {
        /// <summary>
        /// Returns a view of the given type. 
        /// </summary>
        T Create();
    }
}
