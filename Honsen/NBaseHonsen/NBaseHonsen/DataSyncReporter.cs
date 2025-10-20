using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseHonsen
{
    class DataSyncReporter
    {
        private static DataSyncReporter INSTANCE = new DataSyncReporter();
        
        private DataSyncReporter()
        {
        }

        internal static DataSyncReporter instance()
        {
            return INSTANCE;
        }
        
        internal void NotifySyncStart()
        {
            FormCollection col = Application.OpenForms;
            
            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).SyncStart();
                }
            }
        }

        internal void NotifySyncFinish()
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).SyncFinish();
                }
            }
        }

        internal void NotifyModuleUpdate()
        {
            Form form = Form.ActiveForm;
            
            if(form != null)
            {
                try
                {
                    form.Invoke(new MethodInvoker(
                        delegate()
                        {
                            MessageBox.Show("プログラムの最新版が見つかりました。更新するために再起動します。",
                             "プログラム更新通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    ));
                }
                catch (InvalidOperationException e)
                {
                }
            }
        }

        internal void NotifySyncError(string message)
        {
            Form form = Form.ActiveForm;

            if (form != null)
            {
                try
                {
                    form.Invoke(new MethodInvoker(
                        delegate()
                        {
                            MessageBox.Show("データ同期に失敗しました。ネットワークの接続を確認してください。\n\n[詳細] " + message,
                             "同期エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    ));
                }
                catch (InvalidOperationException e)
                {
                }
            }
        }

        internal void NotifyOnline()
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).Online();
                }
            }
        }

        internal void NotifyOffline()
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).Offline();
                }
            }
        }

        internal void NotifyMessage(string message)
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).Message(message);
                }
            }
        }

        internal void NotifyMessage2(string message)
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).Message2(message);
                }
            }
        }

        internal void NotifyMessage3(string message)
        {
            FormCollection col = Application.OpenForms;

            for (int i = 0; i < col.Count; i++)
            {
                if (col[i] is IDataSyncObserver)
                {
                    ((IDataSyncObserver)col[i]).Message3(message);
                }
            }
        }
    }
}
