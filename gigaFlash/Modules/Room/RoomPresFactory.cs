using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gigaFlash.Room;
using gigaFlash.Modules;

namespace gigaFlash
{
    public class RoomPresFactory : LightModuleFactoryBase<RoomPresenter>
    {

        public override RoomPresenter Create(LightState pState)
        {
            IRoom view = ModuleCatalog.Instance.GetView<IRoom>(ModuleOptions.Room);
            return new RoomPresenter(view, pState); 

        }
    }
}
