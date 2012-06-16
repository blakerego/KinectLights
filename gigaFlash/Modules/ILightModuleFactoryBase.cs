using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public abstract class LightModuleFactoryBase<T> 
        where T : LightModulePresenterBase
    {
        public abstract T Create(LightState pState);

        public LightModulePresenterBase CreateBase(ModuleOptions pModuleType, LightState pState)
        {
            return Create(pState); 
        }
    }
}
