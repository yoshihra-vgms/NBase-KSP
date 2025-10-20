using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBaseHonsen
{
    interface IDataSyncObserver
    {
        void SyncStart();
        
        void SyncFinish();

        void Online();

        void Offline();

        void Message(string message);
        void Message2(string message);
        void Message3(string message);
    }
}
