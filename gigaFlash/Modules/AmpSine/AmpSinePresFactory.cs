using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public class AmpSinePresFactory : LightModuleFactoryBase<AmpSinePresenter>
    {
        public override AmpSinePresenter Create(LightState pState)
        {
            IAmpSineView view = ModuleCatalog.Instance.GetView<IAmpSineView>(ModuleOptions.AmpSine);
            return new AmpSinePresenter(view, pState); 
        }
    }
}
