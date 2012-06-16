using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gigaFlash.Delegates
{
    public delegate void VoidDelegate();

    public delegate void TypedDelegate<T>(T value);
    
    public delegate void DualTypedDelegate<T1, T2>(T1 val1, T2 val2);
    
    public delegate void TripleTypedDelegate<T1, T2, T3>(T1 val1, T2 val2, T3 val3);

    public static class EventUtils
    {
        public static void FireEvent(VoidDelegate pEvent)
        {
            VoidDelegate eh = pEvent;
            if (eh != null)
            {
                eh();
            }
        }

        public static void FireTypedEvent<TEventParam>(TypedDelegate<TEventParam> pEvent, TEventParam pParam)
        {
            TypedDelegate<TEventParam> eh = pEvent;
            if (eh != null)
            {
                eh(pParam);
            }
        }

        public static void FireDualTypedEvent<TParam1, TParam2>(DualTypedDelegate<TParam1, TParam2> pEvent,
            TParam1 pParam1, TParam2 pParam2)
        {
            DualTypedDelegate<TParam1, TParam2> eh = pEvent;
            if (eh != null)
            {
                eh(pParam1, pParam2);
            }
        }

        public static void FireTripleTypedEvent<TParam1, TParam2, TParam3>
            (
            TripleTypedDelegate<TParam1, TParam2, TParam3>
            pEvent,
            TParam1 pParam1,
            TParam2 pParam2,
            TParam3 pParam3
            )
        {
            TripleTypedDelegate<TParam1, TParam2, TParam3> eh = pEvent;
            if (eh != null)
            {
                eh(pParam1, pParam2, pParam3);
            }
        }

    }
}
