using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Modules
{
    public class SnakePresFactory : LightModuleFactoryBase<SnakePresenter>
    {
        public override SnakePresenter Create(LightState pState)
        {
            ISnakeView view = ModuleCatalog.Instance.GetView<ISnakeView>(ModuleOptions.Snake);
            return new SnakePresenter(view, pState); 
        }
    }
}
