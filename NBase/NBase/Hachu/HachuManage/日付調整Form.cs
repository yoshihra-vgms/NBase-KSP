using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NBaseData.DAC;
using Hachu.Models;
using Hachu.Utils;
using Hachu.BLC;
using Hachu.Reports;
using NBaseCommon.Nyukyo;
using NBaseUtil;


namespace Hachu.HachuManage
{
    public partial class 日付調整Form : BaseForm
    {
        private ListInfo日付調整 info = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowStyle"></param>
        /// <param name="info"></param>
        #region public 日付調整Form(int windowStyle, ListInfo日付調整 info)
        public 日付調整Form(int windowStyle, ListInfo日付調整 info)
        {
            this.info = info;

            InitializeComponent();

            this.WindowStyle = windowStyle;
        }
        #endregion


        //======================================================================================
        //
        // コールバック
        //
        //======================================================================================

        /// <summary>
        /// フォームのロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void 日付調整Form_Load(object sender, EventArgs e)
        private void 日付調整Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }
        #endregion

        /// <summary>
        /// 「閉じる」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button閉じる_Click(object sender, EventArgs e)
        private void button閉じる_Click(object sender, EventArgs e)
        {
            // Formを閉じる
            //BaseFormClosing();
            Close();
        }
        #endregion

        /// <summary>
        /// 「更新」ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button更新_Click(object sender, EventArgs e)
        private void button更新_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("日付の更新をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (日付調整処理() == false)
            {
                return;
            }
            MessageBox.Show("日付の更新をしました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

            InfoUpdating(info);

            // Formを閉じる
            Close();
        }
        #endregion



        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// 
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            textBox船.Text = info.thi.VesselName;
            textBox手配依頼種.Text = info.thi.ThiIraiSbtName;
            textBox手配依頼詳細種別.Text = info.thi.ThiIraiShousaiName;
            textBox手配内容.Text = info.thi.Naiyou;

            dateTimePicker手配依頼日.Value = info.thi.ThiIraiDate;

            if (info.mm.MmDate != DateTime.MinValue)
            {
                dateTimePicker見積依頼日.Value = info.mm.MmDate;
            }
            else
            {
                dateTimePicker見積依頼日.Visible = false;
            }

            if (info.mk.MkDate != DateTime.MinValue)
            {
                dateTimePicker回答日.Value = info.mk.MkDate;
            }
            else
            {
                dateTimePicker回答日.Visible = false;
            }

            dateTimePicker発注日.Value = info.mk.HachuDate;

            //2021/08/28 //m.yoshihara
            if (info.mk.Nouki != DateTime.MinValue)
            {
                dateTimePicker納品日.Value = info.mk.Nouki;
            }

            if (info.jry != null && info.jry.JryDate != DateTime.MinValue)
            {
                dateTimePicker受領日.Value = info.jry.JryDate;
            }
            else
            {
                dateTimePicker受領日.Visible = false;
            }

            if (info.shr != null)
            {
                if (info.shr.ShrDate != DateTime.MinValue)
                    nullableDateTimePicker支払日.Value = info.shr.ShrDate;
                else
                    nullableDateTimePicker支払日.Value = null;
            }
            else
            {
                nullableDateTimePicker支払日.Visible = false;
            }
            
            //2021/08/28 追加　m.yoshihara
            if (info.shr != null)
            {
                if (info.shr.ShrIraiDate != DateTime.MinValue)
                    nullableDateTimePicker請求書日.Value = info.shr.ShrIraiDate;
                else
                    nullableDateTimePicker請求書日.Value = null;
            }
            else
            {
                nullableDateTimePicker請求書日.Visible = false;
            }




            // 2018.03.05 支払い済みでも編集可とする
            //// 支払済みの場合、編集不可
            label支払済み.Visible = false;
            //if (info.shr != null && info.shr.Status == info.shr.OdStatusValue.Values[(int)OdShr.STATUS.支払済].Value)
            //{
            //    dateTimePicker手配依頼日.Enabled = false;
            //    dateTimePicker見積依頼日.Enabled = false;
            //    dateTimePicker回答日.Enabled = false;
            //    dateTimePicker発注日.Enabled = false;
            //    dateTimePicker受領日.Enabled = false;
            //    dateTimePicker支払日.Enabled = false;

            //    label支払済み.Visible = true;

            //    button更新.Enabled = false;
            //}
        }
        #endregion

        /// <summary>
        /// 日付調整処理
        /// </summary>
        /// <returns></returns>
        #region private bool 日付調整処理()
        private bool 日付調整処理()
        {
            try
            {
                bool ret = false;

                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    DateTime dateValue = dateTimePicker手配依頼日.Value;
                    if (info.thi.ThiIraiDate != dateValue)
                    {
                        info.thi.ThiIraiDate = dateValue;

                        ret = serviceClient.OdThi_Update(NBaseCommon.Common.LoginUser, info.thi);

                        if (ret)
                        {
                            info.thi = serviceClient.OdThi_GetRecord(NBaseCommon.Common.LoginUser, info.thi.OdThiID);
                        }

                    }

                    if (dateTimePicker見積依頼日.Visible)
                    {
                        dateValue = dateTimePicker見積依頼日.Value;
                        if (info.mm.MmDate != dateValue)
                        {
                            info.mm.MmDate = dateValue;

                            ret = serviceClient.OdMm_Update(NBaseCommon.Common.LoginUser, info.mm);

                            if (ret)
                            {
                                info.mm = serviceClient.OdMm_GetRecord(NBaseCommon.Common.LoginUser, info.mm.OdMmID);
                            }
                        }
                    }

                    if (dateTimePicker回答日.Visible)
                    {
                        dateValue = dateTimePicker回答日.Value;
                    }
                    else
                    {
                        dateValue = DateTime.MinValue;
                    }
                    DateTime dateValue2 = dateTimePicker発注日.Value;

                    DateTime dateValue3 = dateTimePicker納品日.Value;//2021/08/28 追加 m.yoshihara
                    if ( info.mk.Nouki != dateValue3)
                    {
                        info.mk.Nouki = dateValue3;
                    }

                    if (info.mk.MkDate != dateValue || info.mk.HachuDate != dateValue2 || info.mk.Nouki != dateValue3)
                    {
                        info.mk.MkDate = dateValue;
                        info.mk.HachuDate = dateValue2;
                        info.mk.Nouki = dateValue3;

                        info.mk.Nendo = NBaseUtil.DateTimeUtils.年度開始日(info.mk.HachuDate).Year.ToString();

                        ret = serviceClient.OdMk_Update(NBaseCommon.Common.LoginUser, info.mk);

                        if (ret)
                        {
                            info.mk = serviceClient.OdMk_GetRecord(NBaseCommon.Common.LoginUser, info.mk.OdMkID);
                        }
                    }

                    if (dateTimePicker受領日.Visible)
                    {
                        dateValue = dateTimePicker受領日.Value;
                        if (info.jry.JryDate != dateValue)
                        {
                            info.jry.JryDate = dateValue;

                            ret = serviceClient.OdJry_Update(NBaseCommon.Common.LoginUser, info.jry);

                            if (ret)
                            {
                                info.jry = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, info.jry.OdJryID);
                            }
                        }
                    }

                    if (nullableDateTimePicker支払日.Visible)
                    {
                        dateValue = DateTime.MinValue;
                        dateValue2 = DateTime.MinValue; ;//2021/08/28 追加 m.yoshihara

                        if (nullableDateTimePicker支払日.Value != null)
                            dateValue = (DateTime)nullableDateTimePicker支払日.Value;
                        if (nullableDateTimePicker請求書日.Value != null)
                            dateValue2 = (DateTime)nullableDateTimePicker請求書日.Value;//2021/08/28 追加 m.yoshihara

                        int chk = 0;
                        if (info.shr.ShrDate != dateValue)
                        {
                            info.shr.ShrDate = dateValue;
                            chk = 1;
                        }
                        //2021/08/28 追加 m.yoshihara
                        if (info.shr.ShrIraiDate != dateValue2)
                        {
                            info.shr.ShrIraiDate = dateValue2;
                            chk = 1;
                        }
                        if (chk == 1)
                        {
                            ret = serviceClient.OdShr_Update(NBaseCommon.Common.LoginUser, info.shr);

                            if (ret)
                            {
                                info.shr = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, info.shr.OdShrID);
                            } 
                        }
                        
                    }

                    //if (ret == false)
                    //{
                    //    MessageBox.Show("日付の更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return false;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("日付の更新に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion

    }
}
