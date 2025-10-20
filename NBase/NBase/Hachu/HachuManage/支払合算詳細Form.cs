using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hachu.BLC;
using Hachu.Utils;
using NBaseData.BLC;
using NBaseData.DAC;
using NBaseData.DS;

namespace Hachu.HachuManage
{
    public partial class 支払合算詳細Form : Form
    {
        /// <summary>
        /// 対象の合算ヘッダ情報
        /// </summary>
        private OdShrGassanHead odShrGassanHead = null;
        
        /// <summary>
        /// 対象の合算項目情報
        /// </summary>
        private List<OdShrGassanItem> odShrGassanItems = null;

        /// <summary>
        /// 一覧
        /// </summary>
        private ItemTreeListView支払合算詳細 TreeList = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        #region public 支払合算詳細Form(OdShrGassanHead info)
        public 支払合算詳細Form(OdShrGassanHead info)
        {
            odShrGassanHead = info;
            InitializeComponent();
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
        private void 支払合算詳細Form_Load(object sender, EventArgs e)
        {
            Formに情報をセットする();
        }



        //======================================================================================
        //
        // 処理メソッド
        //
        //======================================================================================

        /// <summary>
        /// Formに情報をセットする
        /// </summary>
        #region private void Formに情報をセットする()
        private void Formに情報をセットする()
        {
            //=========================================
            // ＤＢから必要な情報を取得する
            //=========================================
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                odShrGassanItems = serviceClient.OdShrGassanItem_GetRecords(NBaseCommon.Common.LoginUser, odShrGassanHead.OdShrGassanHeadID);
            }

            //=========================================
            // 対象合算情報の内容を画面にセットする
            //=========================================
            #region Windowタイトル
            this.Text = NBaseCommon.Common.WindowTitle("番号不明", "合算詳細", ServiceReferences.WcfServiceWrapper.ConnectedServerID);
            #endregion

            textBox手配依頼種別.Text = odShrGassanHead.ThiIraiSbtName;
            textBox備考.Text = odShrGassanHead.Bikou;
            decimal amount = 0;
            foreach (OdShrGassanItem gassanItem in odShrGassanItems)
            {
                //amount += gassanItem.Amount;
                amount += (gassanItem.Amount + gassanItem.Carriage);
            }
            textBox合計金額.Text = NBaseCommon.Common.金額出力2(amount);

            一覧初期化();
            TreeList.AddNodes(odShrGassanItems);

            if (odShrGassanHead.Status == (int)OdShrGassanHead.StatusEnum.支払未作成)
            {
                button支払依頼.Enabled = true;
                button合算解除.Enabled = true;
            }
            else
            {
                button支払依頼.Enabled = false;
                button合算解除.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// 一覧（TreeListView）を初期化する
        /// </summary>
        #region private void 一覧初期化()
        private void 一覧初期化()
        {
            object[,] columns = new object[,] {
                                           {"手配内容", 200, null, null},
                                           {"科目", 100, null, null},
                                           {"発注日", 90, null, null},
                                           {"発注番号", 100, null, null},
                                           {"業者名", 200, null, null},
                                       　　{"完了日", 90, null, null},
                                           {"金額", 90, null, HorizontalAlignment.Right}
                                         };
            TreeList = new ItemTreeListView支払合算詳細(treeListView支払合算詳細);
            TreeList.SetColumns(-2, columns);
       }
        #endregion


        private void button合算解除_Click(object sender, EventArgs e)
        {
            if (odShrGassanHead.Status != (int)OdShrGassanHead.StatusEnum.支払未作成)
            {
                MessageBox.Show("既に支払が作成されているため解除できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("合算を解除します。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            // 合算解除
            bool ret = false;
            using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
            {
                odShrGassanHead.CancelFlag = NBaseCommon.Common.CancelFlag_キャンセル;
                ret = serviceClient.BLC_支払合算解除(NBaseCommon.Common.LoginUser, odShrGassanHead);
            }
            if (ret)
            {
                MessageBox.Show("合算を解除しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 閉じる
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("合算解除に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button支払依頼_Click(object sender, EventArgs e)
        {
            if (odShrGassanHead.Status != (int)OdShrGassanHead.StatusEnum.支払未作成)
            {
                MessageBox.Show("既に支払が作成されています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("支払依頼をします。よろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            // 支払依頼作成
            OdShr odShr = new OdShr();
            odShr.OdShrID = Hachu.Common.CommonDefine.新規ID(false);
            OdJry odJry = new OdJry();
            odJry.OdJryID = odShrGassanHead.OdJryID;
            try
            {

                bool ret = false;
                using (ServiceReferences.NBaseService.ServiceClient serviceClient = ServiceReferences.WcfServiceWrapper.GetInstance().GetServiceClient())
                {
                    ret = serviceClient.BLC_支払合算_支払依頼作成(
                                                            NBaseCommon.Common.LoginUser,
                                                            odShr,
                                                            odShrGassanHead,
                                                            odShrGassanItems);
                    odShr = serviceClient.OdShr_GetRecord(NBaseCommon.Common.LoginUser, odShr.OdShrID);
                    odJry = serviceClient.OdJry_GetRecord(NBaseCommon.Common.LoginUser, odJry.OdJryID);
                    odShrGassanHead = serviceClient.OdShrGassanHead_GetRecord(NBaseCommon.Common.LoginUser, odShrGassanHead.OdShrGassanHeadID);
                }
                if (ret == false)
                {
                    MessageBox.Show("支払の作成に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("支払の作成に失敗しました。\n致命的なエラーです。\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //フォーム起動
            支払Form form = new 支払Form((int)BaseForm.WINDOW_STYLE.通常, odShr, odJry);
            //form.ShowDialog();
        }

        private void button閉じる_Click(object sender, EventArgs e)
        {
            // 閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
