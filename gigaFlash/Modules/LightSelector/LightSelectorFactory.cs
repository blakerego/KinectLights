using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public class LightSelectorFactory : LightModuleFactoryBase<LightSelectorPresenter>
    {
        public override LightSelectorPresenter Create(LightState pState)
        {
            ILightSelectorView view =  
                ModuleCatalog.Instance.GetView<ILightSelectorView>(ModuleOptions.LightSelector);
            return new LightSelectorPresenter(view, pState); 
        }
    }
}
