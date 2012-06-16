using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public class ThunderPresenterFactory : LightModuleFactoryBase<ThunderPresenter>
    {
        public override ThunderPresenter Create(LightState pState)
        {
            IThunderView view = ModuleCatalog.Instance.GetView<IThunderView>(ModuleOptions.Thunder);
            return new ThunderPresenter(view, pState); 
        }
    }
}
