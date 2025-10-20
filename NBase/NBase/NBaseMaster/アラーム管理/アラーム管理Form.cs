using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;

namespace NBaseMaster.アラーム管理
{
    public partial class アラーム管理Form : Form
    {
        private Dictionary<int, MsAlarmDate> alarmDateDic = new Dictionary<int, MsAlarmDate>();
        private Dictionary<int, MaskedTextBox> maskedTextBoxDic = new Dictionary<int, MaskedTextBox>();

        public アラーム管理Form()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            // 船員管理
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.免許_免状アラームID, maskedTextBox免許免状);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.送金受入アラームID, maskedTextBox送金受入);

            // 検査・証書管理
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.検査アラーム1アラームID, maskedTextBox検査アラーム１);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.検査アラーム2アラームID, maskedTextBox検査アラーム２);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.証書アラームID, maskedTextBox証書);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.内部審査アラームID, maskedTextBox内部審査);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.レビューアラームID, maskedTextBoxレビュー);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.審査日アラームID, maskedTextBox審査日);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.救命設備アラームID, maskedTextBox救命設備);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.荷役安全アラームID, maskedTextBox荷役安全設備);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.検船アラームID, maskedTextBox検船);
            
            // 発注管理
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.手配依頼アラームID, maskedTextBox手配依頼);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.見積中アラームID, maskedTextBox見積中);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.承認依頼アラームID, maskedTextBox承認依頼);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.承認済みアラームID, maskedTextBox承認済み);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.発注済みアラームID, maskedTextBox発注済み);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.受領済みアラームID, maskedTextBox受領済み);
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.支払作成済みアラームID, maskedTextBox支払作成済み);
            
            // 文書管理
            maskedTextBoxDic.Add((int)MsAlarmDate.MsAlarmDateIDNo.文書管理アラームID, maskedTextBox文書管理);


            // マスタデータをセット
            List<MsAlarmDate> alarmDates = null;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                alarmDates = serviceClient.MsAlarmDate_GetRecords(NBaseCommon.Common.LoginUser);
            }
            foreach (MsAlarmDate m in alarmDates)
            {
                alarmDateDic.Add(m.MsAlarmDateID, m);

                MaskedTextBox mtb = maskedTextBoxDic[m.MsAlarmDateID];
                mtb.Text = m.DayOffset.ToString();
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
                    MessageBox.Show(this, "更新しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
                    MessageBox.Show(this, "更新に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateFields()
        {
            string ErrMsg = "";
            foreach( int key in maskedTextBoxDic.Keys )
            {
                MaskedTextBox mtb = maskedTextBoxDic[key];
                if (!NumberUtils.Validate(mtb.Text) || mtb.Text.Length > 4)
                {
                    mtb.BackColor = Color.Pink;

                    MsAlarmDate m = alarmDateDic[key];
                    ErrMsg += "・" + m.MsAlarmDateName + "は半角数字4文字以下で入力して下さい\n";
                }
                else
                {
                    mtb.BackColor = Color.White;
                }
            }

            if (ErrMsg.Length > 0)
            {
                MessageBox.Show(ErrMsg, "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FillInstance()
        {
            foreach (int key in maskedTextBoxDic.Keys)
            {
                MaskedTextBox mtb = maskedTextBoxDic[key];
                MsAlarmDate m = alarmDateDic[key];

                m.DayOffset = int.Parse(mtb.Text);
            }
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            List<MsAlarmDate> alarmDates = new List<MsAlarmDate>();
            foreach (int Key in alarmDateDic.Keys)
            {
                alarmDates.Add(alarmDateDic[Key]);
            }

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsAlarmDate_UpdateRecords(NBaseCommon.Common.LoginUser, alarmDates);
            }

            return result;
        }
    }
}
