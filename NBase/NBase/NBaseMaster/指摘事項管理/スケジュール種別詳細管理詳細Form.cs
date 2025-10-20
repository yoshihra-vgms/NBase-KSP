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

namespace NBaseMaster.指摘事項管理
{
    public partial class スケジュール種別詳細管理詳細Form : Form
    {
        private MsScheduleKindDetail scheduleKindDetail;
        private List<MsScheduleKind> kinds;
		
		//データを変更したかどうか？
		private bool ChangeFlag = false;


        public スケジュール種別詳細管理詳細Form(List<MsScheduleKind> kinds)
            : this(kinds, new MsScheduleKindDetail())
        {
        }


        public スケジュール種別詳細管理詳細Form(List<MsScheduleKind> kinds, MsScheduleKindDetail scheduleKindDetail)
        {
            this.kinds = kinds;
            this.scheduleKindDetail = scheduleKindDetail;

            InitializeComponent();
            Init();
        }


        private void Init()
        {
            InitComboBox_種別();

            if (!scheduleKindDetail.IsNew())
            {
                var c = kinds.Where(obj => obj.ScheduleKindID == scheduleKindDetail.ScheduleKindID).First();
                comboBox種別.SelectedItem = c;

                textBox名.Text = scheduleKindDetail.ScheduleKindDetailName;

                if (scheduleKindDetail.ColorR != -1 && scheduleKindDetail.ColorG != -1 && scheduleKindDetail.ColorB != -1)
                    pictureBox.BackColor = Color.FromArgb(scheduleKindDetail.ColorR, scheduleKindDetail.ColorG, scheduleKindDetail.ColorB);
            }
            else
            {
                button削除.Enabled = false;
            }

			//編集可否を初期化
			this.ChangeFlag = false;
        }

        private void InitComboBox_種別()
        {
            comboBox種別.Items.Add(string.Empty);

            foreach (MsScheduleKind o in kinds)
            {
                comboBox種別.Items.Add(o);
            }

            comboBox種別.SelectedIndex = 0;
        }

        private void button更新_Click(object sender, EventArgs e)
        {
            Save(false);
        }

		//引数：削除かどうか？
        private void Save(bool dele)
        {
            if (ValidateFields())
            {
                FillInstance();

                if (InsertOrUpdate())
                {
					if (dele == false)
					{
						MessageBox.Show(this, "登録しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show(this, "削除しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					
					DialogResult = DialogResult.OK;
                    Dispose();
                }
                else
                {
					if (dele == false)
					{
						MessageBox.Show(this, "登録に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						MessageBox.Show(this, "削除に失敗しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
                }
            }
        }


        private bool ValidateFields()
        {
            if (comboBox種別.SelectedIndex == 0)
            {
                comboBox種別.BackColor = Color.Pink;
                MessageBox.Show("種別を選択して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox種別.BackColor = Color.White;
                return false;
            }
            else if (textBox名.Text.Length == 0)
            {
                textBox名.BackColor = Color.Pink;
                MessageBox.Show("詳細名を入力して下さい", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox名.BackColor = Color.White;
                return false;
            }

            return true;
        }


        private void FillInstance()
        {
            scheduleKindDetail.ScheduleKindID = (comboBox種別.SelectedItem as MsScheduleKind).ScheduleKindID;
            scheduleKindDetail.ScheduleKindDetailName = textBox名.Text;

            scheduleKindDetail.ColorR = pictureBox.BackColor.R;
            scheduleKindDetail.ColorG = pictureBox.BackColor.G;
            scheduleKindDetail.ColorB = pictureBox.BackColor.B;
        }


        private bool InsertOrUpdate()
        {
            bool result = false;

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                result = serviceClient.MsScheduleKindDetail_InsertOrUpdate(NBaseCommon.Common.LoginUser, scheduleKindDetail);
            }

            return result;
        }


        private void button削除_Click(object sender, EventArgs e)
        {
            //このデータは利用しているため削除できません

            //削除前に使用しているかのチェックをする
            bool ret = this.CheckDeleteUsing(this.scheduleKindDetail);

            if (ret == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show(this, "削除してよろしいですか？",
                                            "",
                                            MessageBoxButtons.OKCancel,
                                            MessageBoxIcon.Question) == DialogResult.OK)
            {
                scheduleKindDetail.DeleteFlag = true;
                Save(true);
            }
        }


        private void button閉じる_Click(object sender, EventArgs e)
        {
			//編集中に閉じようとした。
			if (this.ChangeFlag == true)
			{
				DialogResult ret = MessageBox.Show(this, "データが編集されていますが、閉じますか？",
											"",
											MessageBoxButtons.OKCancel,
											MessageBoxIcon.Question);

				if (ret == DialogResult.Cancel)
				{
					return;
				}
			}

            DialogResult = DialogResult.Cancel;
            Dispose();
        }

		//データが変更されたとき
		private void ChangeDataText(object sender, EventArgs e)
		{
			this.ChangeFlag = true;
		}


        /// <summary>
        /// 対象のMsScheduleKindDetailデータが使用されているかを調べる
        /// 引数：チェックするデータ
        /// 返り値：削除可能→true、使用されている→false
        /// </summary>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsScheduleKindDetail data)
        {
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
            }

            return true;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pbox = sender as PictureBox;
                colorDialog1.Color = pbox.BackColor;

                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    pbox.BackColor = colorDialog1.Color;
                }
            }
        }
    }
}
