using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NBaseMaster
{
    public class Message
    {
        public static void Show確認(string message)
        {
            MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Showエラー(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Show問合せ(string message)
        {
            DialogResult res = MessageBox.Show(message, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
