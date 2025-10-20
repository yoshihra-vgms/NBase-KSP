using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NBaseCommon
{
    /// <summary>
    /// Form�̋@�\�g���N���X
    /// </summary>
    public class ExForm : Form
    {
        public ExForm parentForm = null;

        public ExForm()
        {
        }

        public bool HideParentForm()
        {
            //ExForm form = hForm.parentForm;
            ExForm form = parentForm;

            while (form != null)
            {
                if (form.Visible == true)
                {
                    form.Visible = false;
                    form = null;
                }
                else
                {
                    form = form.parentForm;
                }
            }

            return true;
        }

        public bool ShowParentForm()
        {
            //ExForm form = hForm.parentForm;
            ExForm form = parentForm;

            while (form != null)
            {
                if (form.parentForm == null || form.parentForm.Visible == true)
                {
                    form.Visible = true;
                    form = null;
                }
                else
                {
                    form = form.parentForm;
                }
            }

            return true;
        }

        public bool ChangeParentFormSize(FormWindowState state)
        {
            //ExForm form = hForm.parentForm;
            ExForm form = parentForm;

            if (form != null)
            {
                form.WindowState = state;
                // 2012.04.09 IINO�ł��܂������Ȃ��̂�TEST�I�ɃR�[�h��ǉ����Ă݂����c
                //if(state != FormWindowState.Minimized)
                //    form.Activate();
            }

            return true;
        }

        /// <summary>
        /// ���̃t�H�[�����t�H�A�O���E���h�ɐݒ肷��B
        /// </summary>
        public void SetForeground()
        {
            try
            {
                SetForegroundWindow(this.Handle);
            }
            catch
            {
                // ���s���Ă��������Ȃ�
            }
        }

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr hWnd);

    }
}
