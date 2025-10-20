using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using NBaseData.DAC;
using ServiceReferences.NBaseService;

namespace NBaseMaster.基地管理
{
    public partial class 基地管理詳細Form : Form
    {
        public MsKichi msKichi = null;
        private List<MsCargo> cargoList;

        //データを編集したかどうか？
        private bool ChangeFlag = false;


        public 基地管理詳細Form()
        {
            InitializeComponent();
        }

        private void 基地管理詳細Form_Load(object sender, EventArgs e)
        {
            List<MsBasho> bashos;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                bashos = serviceClient.MsBasho_GetRecords(NBaseCommon.Common.LoginUser);
            }
            foreach (MsBasho basho in bashos)
            {
                comboBox_Basho.Items.Add(basho);
                comboBox_Basho.AutoCompleteCustomSource.Add(basho.BashoName);
            }



            // 既存のテンプレートの変更
            Template1 test = new Template1();
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                cargoList = serviceClient.MsCargo_GetRecords(NBaseCommon.Common.LoginUser);
            }
            ComboBoxCell comboCell_Cargo;
            comboCell_Cargo = test.Row.Cells["comboBoxCell_Cargo"] as ComboBoxCell;

            foreach (MsCargo c in cargoList)
            {
                comboCell_Cargo.Items.Add(c);
            }

            // MultiRowの設定
            gcMultiRow1.Template = test;







            if (msKichi == null)
            {
                Delete_button.Enabled = false;
            }
            else
            {
                KichiNo_textBox.Text = msKichi.KichiNo;
                KichiName_textBox.Text = msKichi.KichiName;



                {
                    var basho = bashos.Where(o => o.MsBashoId == msKichi.MsBashoID).FirstOrDefault();
                    if (basho != null)
                    {
                        comboBox_Basho.SelectedItem = basho;
                    }
                }
                {
                    textBox_ForNyukouToChakusan.Text = msKichi.ForNyukoToChakusan.ToString("0000");
                    textBox_AvailableForChakusanFrom.Text = 時間Format(msKichi.AvailableForChakusanFrom);
                    textBox_AvailableForChakusanTo.Text = 時間Format(msKichi.AvailableForChakusanTo);


                    textBox_ForChakusanToNiyaku.Text = msKichi.ForChakusanToNiyaku.ToString("0000");
                    textBox_AvailableForNiyakuFrom.Text = 時間Format(msKichi.AvailableForNiyakuFrom);
                    textBox_AvailableForNiyakuTo.Text = 時間Format(msKichi.AvailableForNiyakuTo);

                    textBox_ForNiyakuToRisan.Text = msKichi.ForNiyakuToRisan.ToString("0000");
                    textBox_AvailableForRisanFrom.Text = 時間Format(msKichi.AvailableForRisanFrom);
                    textBox_AvailableForRisanTo.Text = 時間Format(msKichi.AvailableForRisanTo);

                    textBox_ForRisanToShukou.Text = msKichi.ForRisanToShukou.ToString("0000");
                    textBox_AvailableForShukouFrom.Text = 時間Format(msKichi.AvailableForShukouFrom);
                    textBox_AvailableForShukouTo.Text = 時間Format(msKichi.AvailableForShukouTo);
                }
                if (string.IsNullOrEmpty(msKichi.Cargos) == false)
                {
                    var cargoVals = msKichi.Cargos.Split(',');

                    foreach (var cargoVal in cargoVals)
                    {
                        var vals = cargoVal.Split(':');

                        var index = cargoList.FindIndex(o => o.MsCargoID.ToString() == vals[0]);
                        if (index > 0)
                        {
                            gcMultiRow1.Rows.Add();
                            gcMultiRow1.SetValue(gcMultiRow1.Rows.Count - 1, "comboBoxCell_Cargo", index);
                        }

                        if (vals[1].Length > 0)
                            gcMultiRow1.SetValue(gcMultiRow1.Rows.Count - 1, "textBoxCell_tumi", vals[1]);

                        if (vals[2].Length > 0)
                            gcMultiRow1.SetValue(gcMultiRow1.Rows.Count - 1, "textBoxCell_age", vals[2]);
                    }

                }

            }
            
           this.ChangeFlag = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_button_Click(object sender, EventArgs e)
        {
            if (Validation() == false)
            {
                return;
            }

            bool is新規作成;
            if (msKichi == null)
            {
                msKichi = new MsKichi();
                msKichi.MsKichiId = Guid.NewGuid().ToString();
                msKichi.RenewDate = DateTime.Now;
                msKichi.RenewUserID = NBaseCommon.Common.LoginUser.MsUserID;
                msKichi.UserKey = "1";

                is新規作成 = true;
            }
            else
            {
                is新規作成 = false;
            }

            msKichi.KichiNo = KichiNo_textBox.Text;
            msKichi.KichiName = KichiName_textBox.Text;



            if (comboBox_Basho.SelectedItem is MsBasho)
            {
                msKichi.MsBashoID = (comboBox_Basho.SelectedItem as MsBasho).MsBashoId;
            }
            else
            {
                msKichi.MsBashoID = null;
            }

            msKichi.ForNyukoToChakusan = Get時間(textBox_ForNyukouToChakusan.Text);

            msKichi.AvailableForChakusanFrom = Get時間(textBox_AvailableForChakusanFrom.Text);
            msKichi.AvailableForChakusanTo = Get時間(textBox_AvailableForChakusanTo.Text);

            msKichi.ForChakusanToNiyaku = Get時間(textBox_ForNyukouToChakusan.Text);

            msKichi.AvailableForNiyakuFrom = Get時間(textBox_AvailableForNiyakuFrom.Text);
            msKichi.AvailableForNiyakuTo = Get時間(textBox_AvailableForNiyakuTo.Text);

            msKichi.ForNiyakuToRisan = Get時間(textBox_ForNiyakuToRisan.Text);

            msKichi.AvailableForRisanFrom = Get時間(textBox_AvailableForRisanFrom.Text);
            msKichi.AvailableForRisanTo = Get時間(textBox_AvailableForRisanTo.Text);


            msKichi.ForRisanToShukou = Get時間(textBox_ForRisanToShukou.Text);

            msKichi.AvailableForShukouFrom = Get時間(textBox_AvailableForShukouFrom.Text);
            msKichi.AvailableForShukouTo = Get時間(textBox_AvailableForShukouTo.Text);



            string cargos = null;
            foreach (var row in gcMultiRow1.Rows)
            {
                var cell = row.Cells["comboBoxCell_Cargo"];
                var index = cell.Value;
                if (index != null)
                {
                    if ((int)index >= 0)
                    {
                        var c = cargoList[(int)index];
                        cargos += $",{c.MsCargoID}";
                    }

                    cell = row.Cells["textBoxCell_tumi"];
                    var val = cell.Value;
                    if (string.IsNullOrEmpty((string)val) == false)
                    {
                        int res = 0;
                        int.TryParse((string)val, out res);
                        cargos += $":{res}";
                    }
                    else
                    {
                        cargos += $":";
                    }
                    cell = row.Cells["textBoxCell_age"];
                    val = cell.Value;
                    if (string.IsNullOrEmpty((string)val) == false)
                    {
                        int res = 0;
                        int.TryParse((string)val, out res);
                        cargos += $":{res}";
                    }
                    else
                    {
                        cargos += $":";
                    }
                }
            }
            if (string.IsNullOrEmpty(cargos) == false)
                cargos = cargos.Substring(1);
            msKichi.Cargos = cargos;



            if (is新規作成 == true)
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    MsKichi sameKichiNo = serviceClient.MsKichi_GetRecordByKichiNo(NBaseCommon.Common.LoginUser, msKichi.KichiNo);
                    if (sameKichiNo != null)
                    {
                        msKichi = null;
                        MessageBox.Show("基地Noが重複しています", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ret = serviceClient.MsKichi_InsertRecord(NBaseCommon.Common.LoginUser, msKichi);
                }

                if (ret == true)
                {
                    MessageBox.Show("更新しました", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bool ret;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKichi_UpdateRecord(NBaseCommon.Common.LoginUser, msKichi);
                }
                if (ret == true)
                {
                    MessageBox.Show("更新しました", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("更新に失敗しました", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private bool Validation()
        {
            if (KichiNo_textBox.Text.Length != 4)
            {
                MessageBox.Show("基地Noは4桁を入力して下さい", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (KichiName_textBox.Text == "")
            {
                MessageBox.Show("基地名を入力して下さい", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int outVal = 0;
            if (string.IsNullOrEmpty(textBox_ForNyukouToChakusan.Text) == false && int.TryParse(textBox_ForNyukouToChakusan.Text, out outVal) == false)
            {
                MessageBox.Show("入港～着桟は数値で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (時間Validation(textBox_AvailableForChakusanFrom.Text) == false)
            {
                MessageBox.Show("着桟可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (時間Validation(textBox_AvailableForChakusanTo.Text) == false)
            {
                MessageBox.Show("着桟可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (string.IsNullOrEmpty(textBox_ForChakusanToNiyaku.Text) == false && int.TryParse(textBox_ForChakusanToNiyaku.Text, out outVal) == false)
            {
                MessageBox.Show("着桟～荷役開始は数値で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (時間Validation(textBox_AvailableForNiyakuFrom.Text) == false)
            {
                MessageBox.Show("荷役開始可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (時間Validation(textBox_AvailableForNiyakuTo.Text) == false)
            {
                MessageBox.Show("荷役開始可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (string.IsNullOrEmpty(textBox_ForNiyakuToRisan.Text) == false && int.TryParse(textBox_ForNiyakuToRisan.Text, out outVal) == false)
            {
                MessageBox.Show("荷役終了～離桟は数値で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (時間Validation(textBox_AvailableForRisanFrom.Text) == false)
            {
                MessageBox.Show("離桟可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (時間Validation(textBox_AvailableForRisanTo.Text) == false)
            {
                MessageBox.Show("離桟可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrEmpty(textBox_ForRisanToShukou.Text) == false && int.TryParse(textBox_ForRisanToShukou.Text, out outVal) == false)
            {
                MessageBox.Show("離桟～出港は数値で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (時間Validation(textBox_AvailableForShukouFrom.Text) == false)
            {
                MessageBox.Show("出港可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (時間Validation(textBox_AvailableForShukouTo.Text) == false)
            {
                MessageBox.Show("出港可能時刻は0000〜2359の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



            foreach (var row in gcMultiRow1.Rows)
            {
                var cell = row.Cells["textBoxCell_tumi"];
                var val = cell.Value;
                if (string.IsNullOrEmpty((string)val) == false)
                {
                    int res = 0;
                    var isNum = int.TryParse((string)val, out res);
                    if (isNum == false)
                    {
                        MessageBox.Show("積み時間は0000〜9999の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if(res < 0 || res > 9999)
                    {
                        MessageBox.Show("積み時間は0000〜9999の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                     
                }
                cell = row.Cells["textBoxCell_age"];
                val = cell.Value;
                if (string.IsNullOrEmpty((string)val) == false)
                {
                    int res = 0;
                    var isNum = int.TryParse((string)val, out res);
                    if (isNum == false)
                    {
                        MessageBox.Show("揚げ時間は0000〜9999の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else if (res < 0 || res > 9999)
                    {
                        MessageBox.Show("揚げ時間は0000〜9999の範囲で入力してください", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }


            return true;
        }

        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_button_Click(object sender, EventArgs e)
        {
            //削除が可能かを調べる
            bool result = this.CheckDeleteUsing(this.msKichi);

            if (result == false)
            {
                MessageBox.Show(this, "このデータは利用しているため削除できません", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (MessageBox.Show("削除しますか？", "基地管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool ret;
                msKichi.DeleteFlag = 1;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.MsKichi_UpdateRecord(NBaseCommon.Common.LoginUser, msKichi);
                }
                if (ret == true)
                {
                    MessageBox.Show("削除しました", "基地管理", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
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
            Close();
        }

        //データを編集した時
        private void DataChange(object sender, EventArgs e)
        {
            this.ChangeFlag = true;
        }


        /// <summary>
        /// 指定データが使用しているかを調べる
        /// 引数：確認するMsKichiデータ
        /// 返り値：true→未使用削除可、false→使用削除不可
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeleteUsing(MsKichi data)
        {
            //MsKichiは
            //PT_KANIDOUSEI_INFO
            //DJ_DOUSEI
            //MS_BERTH
            //の三つのテーブルにリンクしている。(ER図参照)

            //故にこの三つのテーブルのデータをチェックする。

            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                #region PT_KANIDOUSEI_INFO

                List<PtKanidouseiInfo> kaniinfolist = new List<PtKanidouseiInfo>();

                //関連データの取得
                kaniinfolist = serviceClient.PtKanidouseiInfo_GetRecordsByMsKichiID(NBaseCommon.Common.LoginUser, data.MsKichiId);

                if (kaniinfolist.Count > 0)
                {
                    return false;
                }

                #endregion


                #region DJ_DOUSEI

                List<DjDousei> douseilist = new List<DjDousei>();

                //関連データの取得
                douseilist = serviceClient.DjDousei_GetRecordsByMsKichiID(NBaseCommon.Common.LoginUser, data.MsKichiId);

                if (douseilist.Count > 0)
                {
                    return false;
                }
                

                #endregion


                #region MS_BERTH
                List<MsBerth> berlist = new List<MsBerth>();

                berlist = serviceClient.MsBerth_GetRecordByMsKichiID(NBaseCommon.Common.LoginUser, data.MsKichiId);

                if (berlist.Count > 0)
                {
                    return false;
                }

                #endregion

            }

            return true;
        }



        private decimal Get時間(string text)
        {
            decimal ret = 0;
            if (string.IsNullOrEmpty(text) == false)
            {
                decimal.TryParse(text, out ret);
            }
            return ret;
        }

        private string 時間Format(decimal hhmm)
        {
            if (hhmm == 0)
            {
                return null;
            }
            return (hhmm.ToString("0000"));
        }

        private bool 時間Validation(string timeStr)
        {
            if(string.IsNullOrEmpty(timeStr) == false)
            {
                if (timeStr.Length != 4)
                {
                    return false;
                }
                if (Regex.IsMatch(timeStr, "\\d{4}") == false)
                {
                    return false;
                }
                if (TimeCheck(timeStr) == false)
                {
                    return false;
                }
            }
            return true;
        }
        private bool TimeCheck(string TIME)
        {
            try
            {
                int h = int.Parse(TIME.Substring(0, 2));
                if (h >= 24)
                    return false;

                int m = int.Parse(TIME.Substring(2, 2));
                if (m >= 60)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            gcMultiRow1.Rows.Add();
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];
            int r = e.RowIndex;

            //削除
            if (currentCell.Name == "buttonCell_Remove")
            {
                gcMultiRow1.Rows.RemoveAt(r);
            }
        }

        private void gcMultiRow1_CellEditedFormattedValueChanged(object sender, CellEditedFormattedValueChangedEventArgs e)
        {
            Cell currentCell = gcMultiRow1.Rows[e.RowIndex].Cells[e.CellIndex];
            int r = e.RowIndex;

            if (currentCell.Name == "comboBoxCell_Cargo")
            {
                ComboBoxCell combo = currentCell as ComboBoxCell;

                var val = currentCell.EditedFormattedValue;
                MsCargo cargo = cargoList[(int)val];

                foreach(var row in gcMultiRow1.Rows)
                {
                    if (row.Index == r)
                        continue;

                    var cell = row.Cells["comboBoxCell_Cargo"];
                    var index = cell.EditedFormattedValue;
                    if ((int)index > 0)
                    {
                        var c = cargoList[(int)index];
                        if (cargo.MsCargoID == c.MsCargoID)
                        {
                            MessageBox.Show($"[{cargo.CargoName}]は既に選択されています。");

                            
                        }
                    }
                }
            }
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            GcMultiRow gcMultiRow = (GcMultiRow)sender;
            if (gcMultiRow.CurrentCell is TextBoxCell)
            {
                if (e.FormattedValue != null)
                {
                    const string numbers = "1234567890";
                    string value = e.FormattedValue.ToString();

                    for (int i = 0; i < value.Length; i++)
                    {
                        if (numbers.IndexOf(value[i]) < 0)
                        {
                            gcMultiRow.CurrentCell.ErrorText = "数値以外の文字が含まれています";
                            return;
                        }
                    }
                    gcMultiRow.CurrentCell.ErrorText = string.Empty;
                }
                else
                {
                    gcMultiRow.CurrentCell.ErrorText = string.Empty;
                }
            }
        }
    }
}
