using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using NBaseUtil;
using SyncClient;
using NBaseData.DS;

namespace NBaseHonsen.Controls
{
    public partial class PortUserControl : UserControl
    {
        public event KaniDouseiControl2.AfterEditDelegate AfterEdit;

        public DateTime EventDate;
        public int MsVesselID;
        public string MsVesselName;

        public PtKanidouseiInfo PtKanidouseiInfo = new PtKanidouseiInfo();
        public bool IsSetInfo = false;
        public int Koma = 0;

        public string 簡易動静種別
        {
            get { return PtKanidouseiInfo.KanidouseiInfoShubetuName; }
            set { PtKanidouseiInfo.KanidouseiInfoShubetuName = value; }
        }

        public string 場所
        {
            get 
            {
                //if (基地 != null && 基地 != "")
                //{
                //    return PtKanidouseiInfo.BashoName + ":" + 基地;
                //}
                return PtKanidouseiInfo.BashoName; 
            }
            set { PtKanidouseiInfo.BashoName = value; }
        }

        public string 基地
        {
            get { return PtKanidouseiInfo.KitiName; }
            set { PtKanidouseiInfo.KitiName = value; }
        }

        public string 備考
        {
            get { return PtKanidouseiInfo.Bikou; }
        }

        public PortUserControl()
        {
            Font = UIConstants.DEFAULT_FONT;
            InitializeComponent();
        }


        private void PortUserControl_Load(object sender, EventArgs e)
        {
            SetValue();
        }

        public void SetValue()
        {
            PtKanidouseiInfo.Koma = Koma;

            if (PtKanidouseiInfo.DeleteFlag == NBaseCommon.Common.DeleteFlag_削除)
            {
                PortName_label.Visible = false;
                pictureBox1.Visible = false;
                return;
            }

            if (簡易動静種別 == "")
            {
                PortName_label.Visible = false;
                pictureBox1.Visible = false;
                return;
            }

            PortName_label.Text = 場所;
            if (簡易動静種別 == MsKanidouseiInfoShubetu.積み || 簡易動静種別 == MsKanidouseiInfoShubetu.揚げ)
            {
               if (PtKanidouseiInfo.MsCargoName != null && PtKanidouseiInfo.MsCargoName.Length > 0)
                {
                    PortName_label.Text += " " + PtKanidouseiInfo.MsCargoName;
                }
                if (PtKanidouseiInfo.Qtty > 0)
                {
                    PortName_label.Text += " " + PtKanidouseiInfo.Qtty.ToString(".000");
                }
            }

            PortName_label.Visible = true;

            //--------------------------------------
            //
            //--------------------------------------

            if (備考 != "")
            {
                PortName_label.Font = new Font(PortName_label.Font.FontFamily, PortName_label.Font.Size, FontStyle.Bold);
            }
            else
            {
                PortName_label.Font = new Font(PortName_label.Font.FontFamily, PortName_label.Font.Size);
            }

            if (簡易動静種別 == MsKanidouseiInfoShubetu.積み)
            {
                BackColor = NBaseCommon.Common.ColorTumi;
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.揚げ)
            {
                BackColor = NBaseCommon.Common.ColorAge;
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.待機)
            {
                BackColor = NBaseCommon.Common.ColorTaiki;
                pictureBox1.Visible = true;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.パージ)
            {
                BackColor = NBaseCommon.Common.ColorPurge;
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.避泊)
            {
                BackColor = NBaseCommon.Common.ColorHihaku;
                pictureBox1.Visible = false;
            }
            else if (簡易動静種別 == MsKanidouseiInfoShubetu.その他)
            {
                BackColor = NBaseCommon.Common.ColorEtc;
                pictureBox1.Visible = false;
            }
            else
            {
                BackColor = Color.Transparent;
            }

            if (PtKanidouseiInfo.DeleteFlag == 0 && PtKanidouseiInfo.HonsenCheckDate != DateTime.MinValue)
            {
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
            }
        }

        private void PortUserControl_DoubleClick(object sender, EventArgs e)
        {
            if (MsVesselID != SyncClient.同期Client.LOGIN_VESSEL.MsVesselID)
            {
                // 自船以外の場合、何もしない
                return;
            }

            PtKanidouseiInfo.VesselName = MsVesselName;
            PtKanidouseiInfo.EventDate = EventDate;
            PtKanidouseiInfo.MsVesselID = MsVesselID;
            PtKanidouseiInfo.Koma = Koma;

            if (PtKanidouseiInfo.PtKanidouseiInfoId == null || PtKanidouseiInfo.PtKanidouseiInfoId == "")
            {
                // 2013.05: 船での新規実績入力はなしとコメントを受けたので、コメントアウト
                //// 空をクリック
                //動静実績新規登録Form form = new 動静実績新規登録Form(PtKanidouseiInfo);
                //if (form.ShowDialog() == DialogResult.OK)
                //{
                //    PtKanidouseiInfo = form.KanidouseiInfo;
                //    AfterEdit(EventDate, MsVesselID, Koma, PtKanidouseiInfo);
                //}
            }
            else
            {
                DjDousei dousei = DjDousei.GetRecord(同期Client.LOGIN_USER, PtKanidouseiInfo.DjDouseiID);
                if (dousei.MsKanidouseiInfoShubetuID != MsKanidouseiInfoShubetu.積みID
                    && dousei.MsKanidouseiInfoShubetuID != MsKanidouseiInfoShubetu.揚げID)
                {
                    MessageBox.Show("積み/揚げ 以外は実績登録はできません", "簡易動静", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                動静確認();

                // 予定をクリック
                動静実績登録Form form = new 動静実績登録Form(PtKanidouseiInfo);
                form.AfterEdit += new KaniDouseiControl2.AfterEditDelegate(this.AfterEdit);
                form.ShowDialog();
                // ダイアログ内でAfterEditをCallしているのでここではCallしなくてよい
                //AfterEdit(EventDate, MsVesselID, Koma, PtKanidouseiInfo);
            }
        }

        private void PortUserControl_MouseEnter(object sender, EventArgs e)
        {
            // 2013.05: 船での新規実績入力はなしとコメントを受けたので、自船かつデータあるところだけカーソルを変える
            //if (MsVesselID == 同期Client.LOGIN_VESSEL.MsVesselID)
            if (MsVesselID == 同期Client.LOGIN_VESSEL.MsVesselID &&
                !(PtKanidouseiInfo.PtKanidouseiInfoId == null || PtKanidouseiInfo.PtKanidouseiInfoId == ""))
            {
                Cursor = Cursors.Hand;
            }
        }

        private void PortUserControl_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void PortUserControl_Click(object sender, EventArgs e)
        {
            if (MsVesselID != SyncClient.同期Client.LOGIN_VESSEL.MsVesselID)
            {
                // 自船以外の場合、何もしない
                return;
            }

            動静確認();
        }

        private void 動静確認()
        {
            if (簡易動静種別 == MsKanidouseiInfoShubetu.積み || 簡易動静種別 == MsKanidouseiInfoShubetu.揚げ)
            {
                if (PtKanidouseiInfo.HonsenCheckDate == DateTime.MinValue)
                {
                    pictureBox2.Visible = true;

                    using (ORMapping.DBConnect cone = new ORMapping.DBConnect())
                    {
                        cone.BeginTransaction();

                        PtKanidouseiInfo.HonsenCheckDate = DateTime.Now;
                        SyncTableSaver.InsertOrUpdate(PtKanidouseiInfo, 同期Client.LOGIN_USER, StatusUtils.通信状況.未同期, cone);
                        
                        cone.Commit();
                    }
                }
            }
        }
    }
}
